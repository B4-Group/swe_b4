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
 * Einsatz: Script muss bei Bullet / BulletPrefab vorhanden sein
 *          HealthController.TakeDamage wird von "Bullet.cs" aufgerufen
 *          
 */

public class Bullet : MonoBehaviour
{
   
    public float dietime;
    public GameObject diePEffect;
  
    void Start()
    {
       StartCoroutine(Timer()); //timer init
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{

    //        if (collision.gameObject.CompareTag("Enemy"))
    //        {
    //            Destroy(gameObject);
    //        }

       
    //}
    private void OnCollisionEnter2D(Collision2D collision) //Methode von Unity
    {
        GameObject collisionGameObject = collision.gameObject;
        if(collisionGameObject.name != "Player")
        {
            if(collisionGameObject.GetComponent<HealthController>()!= null)
            {
                collisionGameObject.GetComponent<HealthController>().TakeDamage(10); // damage uebergabe an den Enemy mit einem HealthController Script 
                print("hit![Bullet]"); //consol debug
            }
            Die();
        }
    }
    void Die()
    {
        if (diePEffect != null)
        {
            Instantiate(diePEffect, transform.position, Quaternion.identity); //VFX Effekte
        }
        Destroy(gameObject);
    }
    
    //Timer damit Bullet nach "dietime"(im editor einstellbar) verschwindet
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dietime);
        Destroy(gameObject);
    }
}