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

public class HealthController : MonoBehaviour
{

    public float startHealth;
    public float hp;
    public GameObject diePEffect; //VFX


    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0f)
        {
            Die();
        }

    }
    void Die()
    {
        if (diePEffect != null)
        {
            Instantiate(diePEffect, transform.position, Quaternion.identity); //VFX
        }
        Destroy(gameObject);
    }
}
