using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelauswahlScript : MonoBehaviour
{

    [SerializeField] public bool isInGame = false;
    private Button mainMenu, backButton, sceneButton;
    private IMGUIContainer levelContainer;

    // Array with Scene names of Levels to show
    private string[] SceneList = { "Level1", "Level2" };

    // Start is called before the first frame update
    void Start()
    {
        // Play music only if hasn't been started yet
        if(!FindObjectOfType<AudioManager>().IsPlaying("menuMusic"))
        {
            FindObjectOfType<AudioManager>().Play("menuMusic");
        }

        var root = GetComponent<UIDocument>().rootVisualElement;
        
        mainMenu = root.Q<Button>("mainMenu");
        backButton = root.Q<Button>("back");

        // Things to do when not in game
        if(!isInGame)  {
            // Make Levelauswahl instantly visible
            root.Q<VisualElement>("Container").style.display = DisplayStyle.Flex;

            // hide backButton
            backButton.style.display = DisplayStyle.None;
        }

        mainMenu.clicked += LoadMainMenu;
        backButton.clicked += Hide;

        // Dynamic Levels
        levelContainer = root.Q<IMGUIContainer>("levelContainer");

        // Attach every Scene (from SceneList) to the Level Container as a button
        foreach (string currentScene in SceneList)
        {
            Button levelButton = new();
            levelButton.clicked += () => LoadLevel(currentScene);
            levelButton.text = currentScene;
            levelButton.EnableInClassList("levelButton", true);
            levelContainer.Add(levelButton);
        }
    }

    // Makes the Levelauswahl visible
    public void MakeVisible() {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<VisualElement>("Container").style.display = DisplayStyle.Flex;
        mainMenu.clicked += LoadMainMenu;
        backButton.clicked += Hide;
        Time.timeScale = 0f;
    }

    public void Hide() {
        Debug.Log("Hiding levelauswahl");
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<VisualElement>("Container").style.display = DisplayStyle.None;
        Time.timeScale = 1f;
    }

    public void LoadLevel(string levelName)
    {
        FindObjectOfType<AudioManager>().Stop("menuMusic");
        FindObjectOfType<AudioManager>().Play("click");
        Debug.Log("Loading level" + levelName);
        SceneManager.LoadScene(levelName);
    }

    public void LoadMainMenu()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Debug.Log("Loading main menu");
        SceneManager.LoadScene("mainMenu");
    }
}
