using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static float curHealth;
    public float maxHealth;

    private HealthbarUI m_HealthbarUI;


    private Rigidbody2D rb;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;

        m_HealthbarUI = GetComponent<HealthbarUI>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Heartsbar: Num of Hearts is equal to curHealth
     


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
        Debug.Log("player get dmg [Playerhealth]");
    }

    public void Death()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetBool("IsDead", true);
    }

    public void DamageShip()
    {
        if (curHealth > 0)
        {
            curHealth--;

            m_HealthbarUI.AnimateBar(false);
        }
        if (curHealth == 0)
        {
            Invoke("SinkShip", 1);
        }
    }
    public void HealShip()
    {   
        if (curHealth < 4)
        {
            curHealth++;
            m_HealthbarUI.AnimateBar(true);
        }
    }

}

