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
    [SerializeField]
    GameObject puzzle;

    public void OpenPuzzel()
    {
        //isOpenPuzzelMenu = true;
        puzzle.GetComponentInChildren<PopupUi>().startPuzzle();
        Debug.Log("open chest (Puzzelcontroller");
        
    }
}
