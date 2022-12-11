using UnityEngine;
using PuzzleTypeNamespace;
using System;

/**
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
    [SerializeField] GameObject puzzle;
    PuzzleType puzzleType;

    private bool isPuzzleDone = false;

    public static event Action OnPuzzleDone;

    void Awake() {
        // get a random puzzleNumber from PopupUi
        puzzleType = puzzle.GetComponentInChildren<PuzzleUiController>().GetPuzzleType();
    }

    private void MarkPuzzleAsDone() {
        isPuzzleDone = true;
    }

    public void OpenPuzzel() {
        // if the puzzle is already done, do nothing
        if (isPuzzleDone) { return; }

        // add a listener to the puzzleDone event and open the door when the puzzle is done
        OnPuzzleDone += gameObject.GetComponent<DoorController>().Open;
        OnPuzzleDone += MarkPuzzleAsDone;

        //isOpenPuzzelMenu = true;
        puzzle.GetComponentInChildren<PuzzleUiController>().StartPuzzle(puzzleType, OnPuzzleDone);
        Debug.Log("open Puzzle:" + puzzleType);
        
    }

    // Remove the listener when destroyed	
    void OnDestroy() {
        OnPuzzleDone -= gameObject.GetComponent<DoorController>().Open;
        OnPuzzleDone -= MarkPuzzleAsDone;
    }
}
