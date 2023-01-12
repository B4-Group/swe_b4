using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * using UnityEngine.UI;
 * Author: Nam
 * date: 15.11.2022
 * edit: 19.11.2022
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
    private Sound impactSoundWall, impactSoundEnemy;
  
    void Start()
    {
       StartCoroutine(Timer()); //timer init
        impactSoundWall = FindObjectOfType<AudioManager>().GetSound("bulletWallImpact");
        impactSoundEnemy = FindObjectOfType<AudioManager>().GetSound("bulletEnemyImpact");
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
            if(collisionGameObject.GetComponent<EnemyHealth>()!= null)
            {
                impactSoundEnemy.source.Play();
                collisionGameObject.GetComponent<EnemyHealth>().TakeDamage(10); // damage uebergabe an den Enemy mit einem HealthController Script 
            } else
            {
                impactSoundWall.source.Play();
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