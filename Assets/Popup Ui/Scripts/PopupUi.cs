using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUi : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Button closeUiButton;
    [SerializeField] Text title;
    [SerializeField] GameObject simonSaysPanel;
    [SerializeField] GameObject calculaterPanel;
    [SerializeField] GameObject informationPanel;

    public static event Action OnPuzzleDone;

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
        setQuiz(2);
    }

    public void startPuzzle()
    {
        canvas.SetActive(true);
        Instance = this;
        closeUiButton.onClick.RemoveAllListeners();
        closeUiButton.onClick.AddListener(Hide);
        setQuiz(0);
    }

    public void Hide()
    {
        canvas.SetActive(false);
    }

    public void PuzzleDone()
    {
        Hide();
        OnPuzzleDone.Invoke();
    }

    public void setQuiz(int i)
    {
        if(i == 0)
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
            informationPanel.SetActive(true);
            title.text = "Informationsabfrage";
        }
    }
}
