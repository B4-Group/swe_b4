using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * using UnityEngine.UI;
 * Author: Nam
 * date: 29.11.2022
 * edit: 20.12.2022 by Nam
 * 
 * Status: Baustelle
 * Explanation: Script muss bei Player vorhanden sein
 *          Erstellt neues "Prefab" Objekt: Bullet
 *          Richtung und Geschwindigkeit werden hier bearbeitet
 *         
 *          
 */

public class EnemyShooting : MonoBehaviour
{
    //shootSpeed = velocity
    // shootTimer = delay
    public float shootSpeed, shootTimer = 5;

    private bool isShooting;

    public Transform Shootpoint;
    public GameObject bullet;
    public Animator animator;

    private Sound shootSound;

    void Start()
    {
        isShooting = false;
        shootSound = FindObjectOfType<AudioManager>().GetSound("shootBullet");
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        EnemyShoot();
    }
    void ShootTrigger()
    {
        animator.SetTrigger("throwing");
    }

    public IEnumerator EnemyShoot()

    {
        int direction()
        {
            if (transform.localScale.x < 0f)
            {
                return -1;
            }
            else
            {
                return +1;
            }

        }

        isShooting = true;
        //enemy shoot sound einbauen 29.11 @everyone
        //shootSound.source.Play();

        GameObject newBullet = Instantiate(bullet, Shootpoint.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * direction() * Time.fixedDeltaTime, 0f);

        //newBullet.transform.localScale = new Vector2(newBullet.transform.localScale.x * direction(), newBullet.transform.localScale.y);
        yield return new WaitForSeconds(shootTimer);
        isShooting = false;
        Debug.Log("Shooting[ENEMY]");
        //animator.SetTrigger("Shooting");

    }

}