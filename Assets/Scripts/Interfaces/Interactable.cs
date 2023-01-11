using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //wichtig
/**
 * using UnityEngine.UI;
 * Author: Nam
 * date: 15.11.2022
 * edit: 15.11.2022#
 * 
 * Status: fertig
 * Erkl�rung und Einsatz: Jedes Interactable Object soll dieses Objekt als unterklasse besitzen.
 *                      Diese Klasse erkennt, wenn der Player in und aus der "range" reinkommt.
 *                      In der Range kann der Spieler dann "InteractKey"(im Editor einstellbar) dr�cken.
 */

public class Interactable : MonoBehaviour
{
   
    public bool isInRange;
    public KeyCode interactKey; //einstellbar im Editor
    public UnityEvent interactAction;

    private void OnTriggerEnter2D(Collider2D collision) //in range
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //out range
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange) //sobald der Spieler in Range ist, wird die ganze zeit �berpr�ft, ob er interactKey dr�ckt
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke(); // fire Event
            }
        }
    }
    
}
