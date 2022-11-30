using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * using UnityEngine.UI;
 * Author: Nam
 * date: 29.11.2022
 * edit: 29.11.2022
 * 
 * Status: Bearbeitung
 * Einsatz: Gegner schieﬂt copy von Bullet
 *          Gegner schieﬂt copy von Bullet
 *          
 */

public class EnemyBullet : MonoBehaviour
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
        if (collisionGameObject.name == "Player")
        {
            if (collisionGameObject.GetComponent<PlayerHealth>() != null)
            {
                impactSoundEnemy.source.Play();
                collisionGameObject.GetComponent<PlayerHealth>().SendDamage(1); // damage uebergabe an den Spieler
                print("hit![EnemyBullet]"); //consol debug
            }
            else
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

    //Timer damit EnemyBullet nach "dietime"(im editor einstellbar) verschwindet
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dietime);
        Destroy(gameObject);
    }
}