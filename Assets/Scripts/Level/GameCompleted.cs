using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameCompleted : MonoBehaviour
{
    public static bool gameCompleted = false;

    private static int ActualLevel()
    {
        string actualScene = SceneManager.GetActiveScene().name;
        return (actualScene[actualScene.Length - 1]) - '0';
    }

    private int actualLevel = ActualLevel();
    private const int maxLevel = 2;


    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button NextLevel = root.Q<Button>("Nextlevel");
        Button Hauptmenu = root.Q<Button>("Hauptmenu");

        Debug.Log("Aktuelles Level:" + actualLevel.ToString());

        if (actualLevel == maxLevel)
        {
            NextLevel.style.display = DisplayStyle.None;
            Hauptmenu.clicked += () => mainmenu();
        }
        else
        {
            Hauptmenu.clicked += () => mainmenu();
            NextLevel.clicked += () => nextlevel();
        }
    }

    private void mainmenu()
    {
        if (SceneManager.GetAllScenes().Length <= 1)
        {
            Debug.Log("....Hauptmenü");
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1f;
            gameCompleted = false;
        }
    }
    private void nextlevel()
    {
        if (SceneManager.GetAllScenes().Length <= 1)
        {
            SceneManager.LoadScene("Lade Level:" + (actualLevel + 1));
            Debug.Log("Lade Level:" + (actualLevel + 1).ToString());
            Time.timeScale = 1f;
            gameCompleted = false;
        }
    }

}
