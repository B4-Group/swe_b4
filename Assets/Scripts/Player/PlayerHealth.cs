using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public static int curHealth;
    public static int maxHealth = 3;
    

    private Rigidbody2D rb;
    public Animator anim;


    private VisualElement m_Bar;
    private static VisualElement[] m_Hearts;

    private VisualElement em_Bar;
    private static VisualElement[] em_Hearts;

    private bool death_sound_played;

    private float timer;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        timer = 3.0f;
        death_sound_played = false;
        curHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();

        //Hearts
        m_Bar = GetComponent<UIDocument>().rootVisualElement.Q("healthContainer");
        m_Hearts = m_Bar.Children().ToArray();

        em_Bar = GetComponent<UIDocument>().rootVisualElement.Q("emptyHealthContainer");
        em_Hearts = em_Bar.Children().ToArray();

        for (int loop = (int)maxHealth; loop > 0; loop--)
        {
            m_Hearts[loop - 1].style.visibility = Visibility.Visible;
            em_Hearts[loop - 1].style.visibility = Visibility.Hidden;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Return if game is in game over state
        if(FindObjectOfType<GameOver>().gameOver)
            return;
            
        float hurt = anim.GetFloat("TakeDamage");
        

        //Plays the Hurting-Animation
        if (anim.GetFloat("TakeDamage") < 0 && curHealth != 0)
        {
            anim.SetFloat("TakeDamage", hurt);
        }

        if (curHealth == 2)
        {
            m_Hearts[0].style.visibility = Visibility.Hidden;
            em_Hearts[0].style.visibility = Visibility.Visible;
        }

        if (curHealth == 1)
        {
            m_Hearts[1].style.visibility = Visibility.Hidden;
            em_Hearts[1].style.visibility = Visibility.Visible;
        }

        anim.SetFloat("TakeDamage", 0);

        //Set anim parameter "IsDead" on true if player has no life
        //Plays the Dying-Animation
        if (curHealth <= 0)
        {
            isDead = true;
            timer -= Time.deltaTime;

            if (!death_sound_played)
            {
                FindObjectOfType<AudioManager>().StopAll();
                FindObjectOfType<AudioManager>().Play("lose");
                death_sound_played = true;
            }
            m_Hearts[2].style.visibility = Visibility.Hidden;
            em_Hearts[2].style.visibility = Visibility.Visible;
            Death();
        }
    }

    // Note: This is NOT a debug function anymore!
    // This is actively used by other objects to damage the player
    public void SendDamage(int damageValue = 1)
    {
        FindObjectOfType<AudioManager>().Play("hurt");
        curHealth -= damageValue;
        anim.SetFloat("TakeDamage", damageValue);
    }

    public void Death()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetBool("IsDead", true);
        if (timer <= 0.0f)
        {
        GameOver obj= FindObjectOfType<GameOver>();
        obj.GameOverScreen();
        }
    }

    public void ResetHealth()
    {
        anim.SetBool("IsDead", false);
        rb.bodyType = RigidbodyType2D.Dynamic;
        anim.Play("Idle");
        curHealth = 3;
        for (int loop = (int)maxHealth; loop > 0; loop--)
        {
            m_Hearts[loop - 1].style.visibility = Visibility.Visible;
            em_Hearts[loop - 1].style.visibility = Visibility.Hidden;
        }
    }

    public int GetHealth()
    {
        return curHealth;
    }

    public Animator GetAnim()
    {
        return anim;
    }

    public void Reset(){
        timer = 3.0f;
        ResetHealth();
    }
}

