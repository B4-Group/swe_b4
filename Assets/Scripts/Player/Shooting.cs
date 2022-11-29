using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * using UnityEngine.UI;
 * Author: Nam
 * date: 15.11.2022
 * edit: 19.11.2022 by Manuel
 * 
 * Status: Fertig
 * Explanation: Script muss bei Player vorhanden sein
 *          Erstellt neues "Prefab" Objekt: Bullet
 *          Richtung und Geschwindigkeit werden hier bearbeitet
 *         
 *          
 */

public class Shooting : MonoBehaviour
{
    //shootSpeed = velocity
    // shootTimer = delay
    public float shootSpeed, shootTimer;

    private bool isShooting;

    public Transform Shootpoint;
    public GameObject bullet;
    public Animator animator;

    private Sound shootSound;

    void Start()
    {
        isShooting = false;
        shootSound = FindObjectOfType<AudioManager>().getSound("shootBullet");
    }

    // Update is called once per frame
    void Update() //schie�t durch Animation Aufruf
    {
        //if (Input.GetButtonDown("Fire1") && !isShooting && !Pausenmenu.GameIsPaused)
        //{
        //    FindObjectOfType<AudioManager>().Play("throw");
        //    StartCoroutine(Shoot());
        //}
    }

    public IEnumerator Shoot()

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
        shootSound.source.Play();
        GameObject newBullet = Instantiate(bullet, Shootpoint.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * direction() * Time.fixedDeltaTime, 0f);

        //newBullet.transform.localScale = new Vector2(newBullet.transform.localScale.x * direction(), newBullet.transform.localScale.y);
        yield return new WaitForSeconds(shootTimer);
        isShooting = false;

        //OnFire();

    }

    void OnFire() // nur animation
    {

        animator.SetTrigger("Shooting");

    }


}