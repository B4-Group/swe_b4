using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanelScript : MonoBehaviour
{
    [SerializeField] Text questionText;
    [SerializeField] Button checkResultButton;
    [SerializeField] InputField inputResult;
    [SerializeField] PuzzleUiController PopupUI;
    // Start is called before the first frame update

    int gameobjectAmount = 0;
    string[] gameobjectsToFind = { "MainTorch"};
    void Start()
    {
        checkResultButton.onClick.AddListener(() => checkResults());
        findGameObjects();
        questionText.text = "Wie viele " + gameobjectsToFind[0] + " gibt es in diesem Level?";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkResults()
    {
        int tempResult = int.Parse(inputResult.text);
        if(tempResult == gameobjectAmount)
        {
            PopupUI.PuzzleDone();
        }

    }


    void findGameObjects()
    {
        foreach (GameObject tmpGameobject in GameObject.FindGameObjectsWithTag(gameobjectsToFind[0]))
        {
            gameobjectAmount++;
        }
    }
}
