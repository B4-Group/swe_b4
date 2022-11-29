using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * using UnityEngine.UI;
 * Author: Nam
 * date: 15.11.2022
 * edit: 15.11.2022
 * 
 * Status: Edit
 * Einsatz: Hier soll das Puzzel Popup Menu aufgerufen werden
 *         OpenPuzzel() wird von Interactable aufgerufen
 */


public class PuzzelController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isOpenPuzzelMenu;

    public void OpenPuzzel()
    {
        if (!isOpenPuzzelMenu)
        {
            isOpenPuzzelMenu = true;
            Debug.Log("open chest (Puzzelcontroller");

            // Hier soll das Puzzel Popup Menu aufgerufen werden @Leon
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
