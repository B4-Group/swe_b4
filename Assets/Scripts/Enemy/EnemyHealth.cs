using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/**
 * using UnityEngine.UI;
 * Author: Nam
 * date: 15.11.2022
 * edit: 15.11.2022
 * 
 * Status: Fertig
 * Einsatz: Script muss bei Enemy vorhanden sein
 *          TakeDamage wird von "Bullet.cs" aufgerufen
 *          
 */

public class EnemyHealth : MonoBehaviour
{

    public float startHealth;
    public float hp;
    public GameObject diePEffect; //VFX
    public Animator animator;

    private Sound enemyBreathingSound, enemyDieSound;

    public float interval = 3.0f;
    public float trackedTime = 0.0f;
    public bool dead = false;
    void Update()
    {
        trackedTime += Time.deltaTime;
        if(trackedTime > interval)
        {
            trackedTime = 0.0f;
            if (dead == true) { 
                Destroy(gameObject); Debug.Log("[EH] Dead Removed"); }
            Debug.Log("[EH] dead request");

        }
    }
        private void Start()
    {
        enemyBreathingSound = FindObjectOfType<AudioManager>().getSound("enemyBreathing");
        enemyBreathingSound.source.Play();

        enemyDieSound = FindObjectOfType<AudioManager>().getSound("enemyDying");
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0f)
        {
            Debug.Log("should die");
            Die();
        }
        
    }
    public void Die()
    {
        enemyBreathingSound.source.Stop();
        enemyDieSound.source.Play();
        animator.SetTrigger("dying");
        dead = true;

    }
    public void Die2()
    {
        Debug.Log("should die2");
        if (diePEffect != null)
        {
            Instantiate(diePEffect, transform.position, Quaternion.identity); //VFX
        }
        FindObjectOfType<AudioManager>().Play("mummy_death");
        Destroy(gameObject);
    }
}
