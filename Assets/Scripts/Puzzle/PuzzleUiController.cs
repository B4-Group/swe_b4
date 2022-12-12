using System;
using UnityEngine;
using UnityEngine.UI;
using PuzzleTypeNamespace;

public class PuzzleUiController : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Button closeUiButton;
    [SerializeField] Button hintButton;
    [SerializeField] Text title;
    [SerializeField] GameObject simonSaysPanel;
    [SerializeField] GameObject calculatorPanel;
    [SerializeField] GameObject informationPanel;
    [SerializeField] HintDialogue hintDialogue;

    public static PuzzleUiController Instance;
    private Action OnPuzzleDone;

    void Awake()
    {
        Instance = this;
        closeUiButton.onClick.RemoveAllListeners();
        closeUiButton.onClick.AddListener(Hide);
        hintButton.onClick.RemoveAllListeners();
        hintButton.onClick.AddListener(() => { hintDialogue.ToggleVisibility(); });
    }

    // Returns a random puzzle number
    public PuzzleType GetPuzzleType()
    {
        return (PuzzleType)UnityEngine.Random.Range(0, System.Enum.GetNames(typeof(PuzzleType)).Length-1);
    }

    public void StartPuzzle(PuzzleType puzzleType, Action OnPuzzleDone)
    {
        this.OnPuzzleDone = OnPuzzleDone;
        canvas.SetActive(true);
        Instance = this;
        closeUiButton.onClick.RemoveAllListeners();
        closeUiButton.onClick.AddListener(Hide);

        hintButton.onClick.RemoveAllListeners();
        hintButton.onClick.AddListener(() => { hintDialogue.ToggleVisibility(); });

        SetQuiz(puzzleType);
    }

    public void Hide()
    {
        hintDialogue.CloseHint();
        canvas.SetActive(false);
    }

    [ContextMenu("PuzzleDone cheat")]
    public void PuzzleDone()
    {
        Hide();
        OnPuzzleDone.Invoke();
    }

    public void SetQuiz(PuzzleType puzzleType)
    {
        hintDialogue.SetPuzzleType(puzzleType);

        switch(puzzleType) {
            case PuzzleType.Calculator:
                title.text = "Hieroglyphen Rechner";
                calculatorPanel.SetActive(true);
                simonSaysPanel.SetActive(false);
                informationPanel.SetActive(false);
                break;
            case PuzzleType.SimonSays:
                title.text = "Simon Says";
                simonSaysPanel.SetActive(true);
                calculatorPanel.SetActive(false);
                informationPanel.SetActive(false);
                break;
            case PuzzleType.Information:
                title.text = "Informationsabfrage";
                informationPanel.SetActive(true);
                simonSaysPanel.SetActive(false);
                calculatorPanel.SetActive(false);
                break;
            default:
                throw new System.Exception("PuzzleType not found" + puzzleType);
        }
    }
}
