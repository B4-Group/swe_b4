using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOver : MonoBehaviour
{

    public bool gameOver = false;

    private void DisableScreen()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q("GameOver").style.display = DisplayStyle.None;
    }

    private void Awake()
    {
        this.DisableScreen();
    }

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        UnityEngine.UIElements.Button neustart = root.Q<UnityEngine.UIElements.Button>("Neustart");
        UnityEngine.UIElements.Button hauptmenu = root.Q<UnityEngine.UIElements.Button>("Hauptmenu");


        hauptmenu.clicked += () => mainmenu();
        neustart.clicked += () => restart();
    }

    private void mainmenu()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        Pausenmenu obj = FindObjectOfType<Pausenmenu>();
        obj.HauptMenu();

        gameOver = false;
    }
    private void restart()
    {
        Pausenmenu obj = FindObjectOfType<Pausenmenu>();
        obj.NeueStarten();
        this.DisableScreen();
        gameOver = false;
    }

    public void GameOverScreen()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q("GameOver").style.display = DisplayStyle.Flex;

                PlayerData data = FindObjectOfType<SaveSystem>().LoadData();
        string currentScene = SceneManager.GetActiveScene().name;

        int currentLevel = FindObjectOfType<LevelController>().GetLevelNumber(currentScene);
        int starAmount = FindObjectOfType<Stars>().GetStarsAmount();
        float time = FindObjectOfType<Timer>().getTimer();

        VisualElement starsContainer = root.Q<VisualElement>("starsContainer");

        // Add stars
        for (int i = 0; i < starAmount; i++) {
            Texture2D star = Resources.Load<Texture2D>("star");
            Image starImage = new();
            starImage.style.backgroundImage = new StyleBackground(star);
            starImage.style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;
            starImage.style.height = 50;
            starImage.style.width = 50;
            starsContainer.Add(starImage);
        }

        // Fill up with grey stars
        if(starAmount < 3) {
            for (int i = 0; i < 3 - starAmount; i++) {
                Texture2D star = Resources.Load<Texture2D>("starGrey");
                Image starImage = new();
                starImage.style.backgroundImage = new StyleBackground(star);
                starImage.style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;
                starImage.style.height = 50;
                starImage.style.width = 50;
                starsContainer.Add(starImage);
            }
        }

        VisualElement timeContainer = root.Q<VisualElement>("timeContainer");
        
        // Add time
        Label timeText = new();
        // Format time to mm:ss
        timeText.text = string.Format("{0:00}:{1:00}", Mathf.Floor(time / 60), Mathf.Floor(time % 60));
        timeText.style.unityTextAlign = TextAnchor.MiddleCenter;
        timeText.style.fontSize = 32;
        timeContainer.Add(timeText);

        Time.timeScale = 0f;
        gameOver = true;
    }


}
