using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonsaysScript : MonoBehaviour

{
    int randomNumber;
    int targetRandomNumber;
    int targetButtonId;
    private List<int> randomCheckList;

    [SerializeField] PuzzleUiController PopupUI;

    [SerializeField] Button[] button;
    [SerializeField] Image[] image;
    [SerializeField] Sprite[] sprite;
    [SerializeField] Image target;
    [SerializeField] Text textResult;
    [SerializeField] Text textSuccessCounter;
    [SerializeField] Text textTimer;

    int buttonCount = 12;

    int successCounter = 0;

    public float maxTime = 10.0f;

    void OnEnable()
    {
        resetQuiz();
    }
     // Start is called before the first frame update
     void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        maxTime -= Time.deltaTime;
        textTimer.text = maxTime.ToString();
        if (maxTime <= 0.0f)
        {
            timerEnded();
        }
    }
    public static SimonsaysScript Instance;

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < button.Length; i++)
        {
            int closureIndex = i;
            button[i].onClick.AddListener(() => OnButtonClick(closureIndex));
        }
        successCounter = 0;
        textResult.text = "";
        textSuccessCounter.text = successCounter.ToString() + "/5";
        done();
    }
    void resetQuiz()
    {
        maxTime = 10.0f;
        textResult.text = "";
        successCounter = 0;
        textSuccessCounter.text = successCounter.ToString() + "/5";
    }
    void timerEnded()
    {
        load();
    }

    void load()
    {
        maxTime= 10.0f;
        targetRandomNumber = Random.Range(0, buttonCount);
        target.sprite = sprite[targetRandomNumber];

        randomSpriteToImage();
    }

    void done()
    {
        if (successCounter < 5)
        {
            load();
        }
        else
        {
            PopupUI.PuzzleDone();
        }
    }

    public void OnButtonClick(int id)
    {
        

        if (targetButtonId == id)
        {
            if(successCounter == 4)
            {
                try {
                    FindObjectOfType<AudioManager>().Play("win");
                } catch(System.Exception e) {
                    Debug.Log(e);
                }
            }
            else
            {
                try {
                    FindObjectOfType<AudioManager>().Play("right");
                } catch(System.Exception e) {
                    Debug.Log(e);
                }
            }
            textResult.text = "Erfolgreich";
            textResult.color = Color.green;
            successCounter++;
            textSuccessCounter.text = successCounter.ToString() + "/5";
            done();
        }
        else
        {
            try {
                FindObjectOfType<AudioManager>().Play("wrong");
            } catch(System.Exception e) {
                Debug.Log(e);
            }
            successCounter = 0;
            textSuccessCounter.text = successCounter.ToString() + "/5";
            textResult.text = "falsch";
            textResult.color= Color.red;
            done();
        }
    }

    private void randomSpriteToImage()
    {
        randomCheckList = new List<int>();
        for (int i = 0; i < buttonCount; i++)
        {
            randomNumber = Random.Range(0, buttonCount);
            while (randomCheckList.Contains(randomNumber))
            {
                randomNumber = Random.Range(0, buttonCount);
            }
            if(randomNumber == targetRandomNumber)
            {
                targetButtonId = i;
            }
            image[i].sprite = sprite[randomNumber];
            randomCheckList.Add(randomNumber);
        }
    }

    public void Reset() {
        successCounter = 0;
        textSuccessCounter.text = successCounter.ToString() + "/5";
        load();
    }
}
