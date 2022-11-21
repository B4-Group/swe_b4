using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float curHealth;
    public float maxHealth;

    public float numOfHearts;
    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHearts;
    private float time = 0.4f;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        numOfHearts = maxHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            //Heartsbar: Num of Hearts is equal to curHealth
            for (int i = 0; i < hearts.Length; i++)
            {
                if (curHealth > numOfHearts)
                {
                    numOfHearts = curHealth;
                }

                if (i < curHealth)
                {
                    hearts[i].sprite = fullHearts;
                }
                else
                {
                    hearts[i].sprite = emptyHearts;
                }

                if (i < numOfHearts)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }


            float hurt = anim.GetFloat("TakeDamage");


            //Plays the Hurting-Animation
            if (anim.GetFloat("TakeDamage") < 0 && curHealth != 0)
            {
                anim.SetFloat("TakeDamage", hurt);
            }
            anim.SetFloat("TakeDamage", 0);

            //Set anim parameter "IsDead" on true if player has no life
            //Plays the Dying-Animation
            if (curHealth <= 0)
            {
                time -= Time.deltaTime;
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
    }

    public void Death()
    {
        GetComponent<Rigidbody2D>().simulated = true; 
        anim.SetBool("IsDead", true);

        if (time <= 0f)
        {
            Time.timeScale = 0f;
        }
        
    }

}

