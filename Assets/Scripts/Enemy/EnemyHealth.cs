using System;
using System.Collections;
using System.Collections.Generic;
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
            StartCoroutine( Die());
        }

    }
    public IEnumerator Die()
    {
        enemyBreathingSound.source.Stop();
        enemyDieSound.source.Play();
        animator.SetTrigger("dying");
        yield return new WaitForSeconds(1);
        if (diePEffect != null)
        {
            Instantiate(diePEffect, transform.position, Quaternion.identity); //VFX
        }
        Destroy(gameObject);
    }
}
