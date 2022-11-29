using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CalculatorScript : MonoBehaviour
{
    [SerializeField] Image[] image;
    [SerializeField] Sprite[] sprite;
    [SerializeField] Text textResult1;
    [SerializeField] Text textResult2;
    [SerializeField] Text InputResultText;
    [SerializeField] Image[] imageResult;
    [SerializeField] Button checkResultButton;
    [SerializeField] InputField[] inputResult;
    [SerializeField] PopupUi PopupUI;


    int randomNumber;
    private List<int> randomCheckList; 
    private List<Sprite> randomSpritesList;
    private List<Sprite> tmpSpritesList;

    int imageCount = 2;
    int maxNumber = 100;
    int Result1;
    int Result2;

    // Start is called before the first frame update
    void Start()
    {
        checkResultButton.onClick.AddListener(() => checkResults());
        load();
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void checkResults()
    {
        int tempResult1 = int.Parse(inputResult[0].text);
        int tempResult2 = int.Parse(inputResult[1].text);

        Debug.Log(tempResult1);
        Debug.Log(tempResult2);

        Debug.Log(Result1 / 2);
        Debug.Log((Result1/ 2) + Result2);
        

        if (tempResult1 == (Result1 / 2) && tempResult2 == ((Result1 / 2) + Result2))
        {
            PopupUI.Hide();
        }
        else
        {
            InputResultText.text = "Fehler";
        }
    }

    private void load()
    {
        Result1 = Random.Range(0, maxNumber);
        if(Result1 % 2 != 0)
        {
            Result1++;
        }

        Result2 = Result1 + Random.Range(0, maxNumber);

        Debug.Log(Result1);
        Debug.Log(Result2);

        textResult1.text = Result1.ToString();
        textResult2.text = Result2.ToString();

        randomSpritesList = createRandomSpriteList();
        image[0].sprite = randomSpritesList[0];
        image[1].sprite = randomSpritesList[0];
        image[2].sprite = randomSpritesList[1];
        image[3].sprite = randomSpritesList[0];

        imageResult[0].sprite = randomSpritesList[0];
        imageResult[1].sprite = randomSpritesList[1];
    }
    private List<Sprite> createRandomSpriteList()
    {
        tmpSpritesList = new List<Sprite>();
        randomCheckList = new List<int>();
        for (int i = 0; i < sprite.Length; i++)
        {
            randomNumber = Random.Range(0, sprite.Length);
            while (randomCheckList.Contains(randomNumber))
            {
                randomNumber = Random.Range(0, sprite.Length);
            }
            tmpSpritesList.Add(sprite[randomNumber]);
            randomCheckList.Add(randomNumber);
        }
        return tmpSpritesList;
    }
}
