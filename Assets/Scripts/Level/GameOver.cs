using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOver : MonoBehaviour
{

    public static bool gameOver = false;

    private void DisableScreen()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q("GameOver").style.display = DisplayStyle.None;
    }

    private void Awake()
    {
        this.DisableScreen();
    }

    private void Update()
    {

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
        Debug.Log("GameOver");
        Time.timeScale = 0f;
        gameOver = true;
    }


}
