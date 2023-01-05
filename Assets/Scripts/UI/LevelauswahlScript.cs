using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelauswahlScript : MonoBehaviour
{

    [SerializeField] public bool isInGame = false;
    private Button mainMenu, backButton, sceneButton;
    private IMGUIContainer levelContainer;

    // Start is called before the first frame update
    void Start()
    {
        // Play music only if hasn't been started yet
        try {
            if(SceneManager.GetActiveScene().name == "Levelauswahl" && !FindObjectOfType<AudioManager>().IsPlaying("menuMusic"))
            {
                FindObjectOfType<AudioManager>().Play("menuMusic");
            }
        } catch (System.NullReferenceException e) {
            Debug.Log("No AudioManager found, probably in editor");
        }

        var root = GetComponent<UIDocument>().rootVisualElement;
        
        mainMenu = root.Q<Button>("mainMenu");
        backButton = root.Q<Button>("back");
        
        mainMenu.clicked += LoadMainMenu;
        backButton.clicked += Hide;

        // Things to do when not in game
        if(!isInGame)  {
            // Make Levelauswahl instantly visible
            root.Q<VisualElement>("Container").style.display = DisplayStyle.Flex;

            // hide mainMenu
            mainMenu.style.display = DisplayStyle.None;

            // change back button to go to main menu
            backButton.clicked += LoadMainMenu;

            // Load Data from SaveSystem
            GetComponent<SaveSystem>().LoadData();
        }

        

        // Dynamic Levels
        levelContainer = root.Q<IMGUIContainer>("levelContainer");
        
        PlayerData data = FindObjectOfType<SaveSystem>().LoadData();
        if(SceneManager.GetActiveScene().name == "Levelauswahl")
            GetComponent<LevelController>().ForceRefreshLevelList();
        else
            FindObjectOfType<LevelController>().ForceRefreshLevelList();
        string[] SceneList = SceneManager.GetActiveScene().name == "Levelauswahl" ?  GetComponent<LevelController>().levels : FindObjectOfType<LevelController>().levels;

        // Attach every Scene (from SceneList) to the Level Container as a button
        foreach (string currentScene in SceneList)
        {
            int currentLevelNumber = SceneManager.GetActiveScene().name == "Levelauswahl" ? GetComponent<LevelController>().GetLevelNumber(currentScene) : FindObjectOfType<LevelController>().GetLevelNumber(currentScene);

            VisualElement currentLevelContainer = new();

            Label levelText = new();
            levelText.text = currentScene;
            levelText.style.unityTextAlign = TextAnchor.MiddleCenter;
            levelText.style.fontSize = 24;

            currentLevelContainer.Add(levelText);

            Button levelButton = new();

            // Disable button if level is not unlocked
            if((data.highestLevel) >= currentLevelNumber || (data.highestLevel == -1 && currentLevelNumber == 0))
            {
                levelButton.clicked += () => LoadLevel(currentScene);
                
            } else {
                levelButton.style.opacity = 0.25f;
            }

            // Add Background image to button
            // The Image has the same name as currentScene
            Texture2D currentThumbnail = Resources.Load<Texture2D>(currentScene);
            levelButton.style.backgroundImage = new StyleBackground(currentThumbnail);
            levelButton.style.unityBackgroundScaleMode  = ScaleMode.ScaleAndCrop;
            levelButton.style.height = 216;
            levelButton.style.width = 384;

            currentLevelContainer.Add(levelButton);

            VisualElement statsContainer = new();
            statsContainer.style.flexDirection = FlexDirection.Row;
            statsContainer.style.justifyContent = Justify.SpaceBetween;

            VisualElement starsContainer = new();
            starsContainer.style.flexDirection = FlexDirection.Row;
            starsContainer.style.justifyContent = Justify.Center;
            
            int currentLevel = currentLevelNumber;
            // Get Stars and Time from SaveSystem
            int stars = data.stars[currentLevel];
            float time = data.time[currentLevel];
            
            // Add stars
            for (int i = 0; i < stars; i++) {
                Texture2D star = Resources.Load<Texture2D>("star");
                Image starImage = new();
                starImage.style.backgroundImage = new StyleBackground(star);
                starImage.style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;
                starImage.style.height = 32;
                starImage.style.width = 32;
                starsContainer.Add(starImage);
            }

            // Fill up with grey stars
            if(stars < 3) {
                for (int i = 0; i < 3 - stars; i++) {
                    Texture2D star = Resources.Load<Texture2D>("starGrey");
                    Image starImage = new();
                    starImage.style.backgroundImage = new StyleBackground(star);
                    starImage.style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;
                    starImage.style.height = 32;
                    starImage.style.width = 32;
                    starsContainer.Add(starImage);
                }
            }

            statsContainer.Add(starsContainer);

            // Add time
            Label timeText = new();
            // Format time to mm:ss
            timeText.text = string.Format("{0:00}:{1:00}", Mathf.Floor(time / 60), Mathf.Floor(time % 60));
            timeText.style.unityTextAlign = TextAnchor.MiddleCenter;
            timeText.style.fontSize = 24;
            statsContainer.Add(timeText);

            currentLevelContainer.Add(statsContainer);

            levelContainer.Add(currentLevelContainer);
        }
    }

    // Makes the Levelauswahl visible
    public void MakeVisible() {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<VisualElement>("Container").style.display = DisplayStyle.Flex;
        Time.timeScale = 0f;
    }

    public void Hide() {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<VisualElement>("Container").style.display = DisplayStyle.None;
        Time.timeScale = 1f;
    }

    public void LoadLevel(string levelName)
    {
        FindObjectOfType<AudioManager>().Stop("menuMusic");
        FindObjectOfType<AudioManager>().Play("click");

        // Figure out which levelNumber the levelName is
        int levelNumber = FindObjectOfType<LevelController>().GetLevelNumber(levelName);

        // Load Data from SaveSystem
        PlayerData data = FindObjectOfType<SaveSystem>().LoadData();

        // Set currentLevel to levelNumber
        data.currentLevel = levelNumber;

        // Save Data to SaveSystem
        FindObjectOfType<SaveSystem>().Save(data);

        SceneManager.LoadScene(levelName);
    }

    public void LoadMainMenu()
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene("mainMenu");
    }
}
