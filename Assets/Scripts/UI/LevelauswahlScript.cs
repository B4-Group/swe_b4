using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelauswahlScript : MonoBehaviour
{
    public Button mainMenu, backButton, sceneButton;
    private IMGUIContainer levelContainer;

    // Array with Scene names of Levels to show
    public string[] SceneList = { "Level1", "Level2" };

    public GameObject buttonPreset;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        mainMenu = root.Q<Button>("mainMenu");
        backButton = root.Q<Button>("back");

        // Handle default clicks
        mainMenu.clicked += LoadMainMenu;
        backButton.clicked += LoadMainMenu;

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

    public void LoadLevel(string levelName)
    {
        Debug.Log("Loading level" + levelName);
        SceneManager.LoadScene(levelName);
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading main menu");
        SceneManager.LoadScene("mainMenu");
    }
}
