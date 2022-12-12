using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameCompleted : MonoBehaviour
{
    public bool gameCompleted = false;
    private int currentLevel;
    private const int maxLevel = 2;

    private void OnEnable()
    {
        SetCurrentLevel();
        FindObjectOfType<AudioManager>().Stop("step");
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button NextLevel = root.Q<Button>("Nextlevel");
        Button Hauptmenu = root.Q<Button>("Hauptmenu");

        if ((currentLevel+1) == maxLevel) {
            NextLevel.style.display = DisplayStyle.None;
            Hauptmenu.clicked += () => mainmenu();
        } else {
            Hauptmenu.clicked += () => mainmenu();
            NextLevel.clicked += () => nextlevel();
        }
    }
    
    private void SetCurrentLevel()
    {
        PlayerData data = GetComponent<SaveSystem>().LoadData();
        currentLevel = data.currentLevel;
    }

    private void mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        gameCompleted = false;
        
    }
    private void nextlevel()
    {
        // Get next scene from save system
        PlayerData data = GetComponent<SaveSystem>().LoadData();
        data.currentLevel += 1;
        GetComponent<SaveSystem>().Save(data);
        SceneManager.LoadScene($"Level{data.currentLevel+1}");
        Debug.Log(data.currentLevel);
        Time.timeScale = 1f;
        gameCompleted = false;
        
    }

}
