using UnityEngine;
using UnityEngine.UI;
using PuzzleTypeNamespace;
using System.Collections.Generic;

public class InformationPanelScript : MonoBehaviour
{
    [SerializeField] Text questionText;
    [SerializeField] Button checkResultButton;
    [SerializeField] InputField inputResult;
    [SerializeField] PuzzleUiController PopupUI;
    InformationPuzzleType informationPuzzleType;
    public List<KeyValuePair<InformationPuzzleType, string>> informationPuzzleContent;

    int gameobjectAmount = 0;
    string[] gameobjectsToFind = { "BlueTorch" };
    void Start()
    {
        // key value map to get the question text for the information puzzle
        informationPuzzleContent = new List<KeyValuePair<InformationPuzzleType, string>>() {
            new KeyValuePair<InformationPuzzleType, string>(InformationPuzzleType.BlueTorch, "Wie viele blaue Fackeln gibt es in diesem Level?"),
            new KeyValuePair<InformationPuzzleType, string>(InformationPuzzleType.Vase, "Wie viele Vasen gibt es in diesem Level?"),
            new KeyValuePair<InformationPuzzleType, string>(InformationPuzzleType.Chest, "Wie viele Kisten gibt es in diesem Level?"),
        };

        // translator[InformationPuzzleType] will return the Unity Tag to look for the gameObjects
        // Temporary solution, will be replaced by a dictionary
        var translator = new string[3] { "BlueTorch", "Vase", "Chest" }; 

        foreach (GameObject tmpGameobject in GameObject.FindGameObjectsWithTag(translator[(int)informationPuzzleType]))
        {
            gameobjectAmount++;
        }

        checkResultButton.onClick.AddListener(() => checkResults());
        
        questionText.text = informationPuzzleContent[(int)informationPuzzleType].Value;
    }

    public void SetInformationPuzzleType(InformationPuzzleType puzzleType)
    {
        informationPuzzleType = puzzleType;
    }

    void checkResults()
    {
        int tempResult = int.Parse(inputResult.text);
        if(tempResult == gameobjectAmount)
        {
            try {
                FindObjectOfType<AudioManager>().Play("win");
            } catch(System.Exception e) {
                Debug.Log(e);
            }
            PopupUI.PuzzleDone();
        }
        else
        {
            try {
                FindObjectOfType<AudioManager>().Play("wrong");
            } catch(System.Exception e) {
                Debug.Log(e);
            }
        }
    }

    public void Reset() {
        inputResult.text = "";
    }
}
