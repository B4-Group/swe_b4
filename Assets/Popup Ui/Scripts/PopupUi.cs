using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUi : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Button closeUiButton;
    [SerializeField] Button hintButton;
    [SerializeField] Text title;
    [SerializeField] GameObject simonSaysPanel;
    [SerializeField] GameObject calculaterPanel;
    [SerializeField] GameObject informationPanel;
    [SerializeField] HintDialogue hintDialogue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static PopupUi Instance;

    void Awake()
    {
        canvas.SetActive(true);
        Instance = this;
        closeUiButton.onClick.RemoveAllListeners();
        closeUiButton.onClick.AddListener(Hide);

        hintButton.onClick.RemoveAllListeners();
        hintButton.onClick.AddListener(() => { hintDialogue.toggleVisibility(); });
        setQuiz(0);
    }

    public void startPuzzle()
    {
        canvas.SetActive(true);
        Instance = this;
        closeUiButton.onClick.RemoveAllListeners();
        closeUiButton.onClick.AddListener(Hide);

        hintButton.onClick.RemoveAllListeners();
        hintButton.onClick.AddListener(() => { hintDialogue.toggleVisibility(); });
        setQuiz(0);
    }

    public void Hide()
    {
        hintDialogue.closeHint();
        canvas.SetActive(false);
    }

    public void setQuiz(int i)
    {
        hintDialogue.setPuzzleType(i);
        if (i == 0)
        {
            title.text = "Hieroglyphen Rechner";
            

        }
        else if (i == 1)
        {
            title.text = "Simon Says";
            simonSaysPanel.SetActive(true);
        }
        else if (i == 2)
        {
            title.text = "Informationsabfrage";
        }
    }
}
