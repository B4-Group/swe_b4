using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/**
 * using UnityEngine.UI;
 * Author: Nam
 * date: 29.11.2022
 * edit: 29.11.2022
 * 
 * Status: Fertig
 * Einsatz: Nur zum sterben die Entfernung
 *          Nur zum sterben die Entfernung
 *          
 */

public class EnemyDyingVFX : MonoBehaviour
{

    public float startHealth;
    public float hp;
    public GameObject diePEffect; //VFX
    public Animator animator;

    public float interval = 3.0f;
    public float trackedTime = 0.0f;
    void Update()
    {
        trackedTime += Time.deltaTime;
    }
    private void Start()
    {

    }

   
    public void Die2()
    {
        Debug.Log("should die2");
        if (diePEffect != null)
        {
            Instantiate(diePEffect, transform.position, Quaternion.identity); //VFX
        }
        //FindObjectOfType<AudioManager>().Play("mummy_death");
        // hier soll Die1 methode in EnemyHealth aufgerufen werden
        Destroy(gameObject);
    }
}
