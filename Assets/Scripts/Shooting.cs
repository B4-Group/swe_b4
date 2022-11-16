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
 * Erkl�rung: Script muss bei Player vorhanden sein
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



    void Start()
    {
        isShooting = false;
    }

    // Update is called once per frame
    void Update() //schie�t durch Animation Aufruf
    {
        //if (Input.GetButtonDown("Fire1") && !isShooting)
        //{
        //    StartCoroutine(shoot());
        //}
    }

    public IEnumerator shoot()

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

        GameObject newBullet = Instantiate(bullet, Shootpoint.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * direction() * Time.fixedDeltaTime, 0f);

        //newBullet.transform.localScale = new Vector2(newBullet.transform.localScale.x * direction(), newBullet.transform.localScale.y);
        yield return new WaitForSeconds(shootTimer);
        isShooting = false;


    }




}