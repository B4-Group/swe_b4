using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public static float curHealth;
    public float maxHealth = 3;
    

    private Rigidbody2D rb;
    private Animator anim;


    private VisualElement m_Bar;
    private VisualElement[] m_Hearts;

    private VisualElement em_Bar;
    private VisualElement[] em_Hearts;


    private Sound dieSound, loseHealthSound;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;


        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

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

        dieSound = FindObjectOfType<AudioManager>().getSound("playerDying");
        loseHealthSound = FindObjectOfType<AudioManager>().getSound("playerLoseHealth");
    }

    // Update is called once per frame
    void Update()
    {
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
            m_Hearts[2].style.visibility = Visibility.Hidden;
            em_Hearts[2].style.visibility = Visibility.Visible;
            Death();
        }

        //Keyboard Input -> Will be removed later
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SendDamage();
        }
    }

    //Damages hisself with clicking on space
    public void SendDamage(int damageValue = 1)
    {
        curHealth -= damageValue;
        anim.SetFloat("TakeDamage", damageValue);
        loseHealthSound.source.Play();
        Debug.Log("player get dmg [Playerhealth]");
    }

    public void Death()
    {
        dieSound.source.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetBool("IsDead", true);
    }
}

