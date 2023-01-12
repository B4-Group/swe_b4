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

    [SerializeField]
    PuzzleType puzzleType = PuzzleType.Information;

    [SerializeField]
    InformationPuzzleType informationPuzzleType = InformationPuzzleType.BlueTorch;

    [SerializeField]
    bool isDetermined = false;

    private bool isPuzzleDone = false;

    public static event Action OnPuzzleDone;

    void Awake() {
         // get a random puzzleNumber from PopupUi
        if(!isDetermined)
            puzzleType = puzzle.GetComponentInChildren<PuzzleUiController>().GetPuzzleType();
    }

    private void MarkPuzzleAsDone() {
        isPuzzleDone = true;
    }

    public void OpenPuzzel() {
        // if the puzzle is already done, do nothing
        if (isPuzzleDone) { return; }

        // Remove the listeners
        OnPuzzleDone -= gameObject.GetComponent<DoorController>().Open;
        OnPuzzleDone -= MarkPuzzleAsDone;

        // add a listener to the puzzleDone event and open the door when the puzzle is done
        OnPuzzleDone += gameObject.GetComponent<DoorController>().Open;
        OnPuzzleDone += MarkPuzzleAsDone;

        puzzle.GetComponentInChildren<PuzzleUiController>().StartPuzzle(puzzleType, OnPuzzleDone);

        if(puzzleType == PuzzleType.Information) puzzle.GetComponentInChildren<InformationPanelScript>().SetInformationPuzzleType(informationPuzzleType);
        
    }

    public void ResetPuzzle() {
        OnPuzzleDone -= gameObject.GetComponent<DoorController>().Close;
        OnPuzzleDone -= MarkPuzzleAsDone;
        isPuzzleDone = false;
    }

    // Remove the listener when destroyed	
    void OnDestroy() {
        OnPuzzleDone -= gameObject.GetComponent<DoorController>().Open;
        OnPuzzleDone -= MarkPuzzleAsDone;
    }
}
