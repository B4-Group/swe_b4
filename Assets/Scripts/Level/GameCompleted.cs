using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameCompleted : MonoBehaviour
{
    public bool gameCompleted = false;
    
    // Current level in the game, starts at 0
    private int currentLevel;
    
    // Amount of levels in the game
    private const int maxLevel = 2;

    private void OnEnable()
    {
        // Get next scene from save system
        PlayerData data = GetComponent<SaveSystem>().LoadData();
        // increment the level counter
        data.currentLevel += 1;
        // save the modified level counter back to disk
        GetComponent<SaveSystem>().Save(data);
        // set local property to the new level counter
        currentLevel = data.currentLevel;

        try {
            FindObjectOfType<AudioManager>().Stop("step");
        } catch (System.Exception e) {
            Debug.Log("No AudioManager found, probably in editor. " + e);
        }
        
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button NextLevel = root.Q<Button>("Nextlevel");
        Button Hauptmenu = root.Q<Button>("Hauptmenu");

        if ((currentLevel) == maxLevel) {
            NextLevel.style.display = DisplayStyle.None;
            Hauptmenu.clicked += () => mainmenu();
        } else {
            Hauptmenu.clicked += () => mainmenu();
            NextLevel.clicked += () => nextlevel();
        }
    }

    private void mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
        Resume();
        
    }
    private void nextlevel()
    {
        SceneManager.LoadScene($"Level{currentLevel+1}");
        Resume();
        
    }

    private void Resume() {
        Time.timeScale = 1f;
        gameCompleted = false;
    }
}
