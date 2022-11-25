using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Pausenmenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private IMGUIContainer pauseMenu;
    public Button resume;
    public Button restart;
    public Button levelAuswahl;
    public Button hauptMenu;
    public Button pauseButton;


    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        pauseMenu = root.Q<IMGUIContainer>("pauseMenu");


        resume = root.Q<Button>("resume");
        restart = root.Q<Button>("restart");
        levelAuswahl = root.Q<Button>("levelAuswahl");
        hauptMenu = root.Q<Button>("hauptMenu");
        pauseButton = root.Q<Button>("pauseButton");

        resume.clicked += Resume;
        restart.clicked += NeueStarten;
        levelAuswahl.clicked += LoadLevelauswahl;
        hauptMenu.clicked += HauptMenu;
        pauseButton.clicked += Pause;

        pauseMenu.style.visibility = Visibility.Hidden;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }
    public void Resume()
    {
        pauseMenu.style.visibility = Visibility.Hidden;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenu.style.visibility = Visibility.Visible;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void NeueStarten()
    {
        Debug.Log("...NeuStarten");
        Stars.ResetStar();
        Timer.ResetTimer();
        Resume();
    }
    public void LoadLevelauswahl()
    {
        Debug.Log("Loading levelauswahl");
        SceneManager.LoadScene("Levelauswahl");
        Resume();
    }
    public void HauptMenu()
    {
        Debug.Log("....Hauptmenü");
        SceneManager.LoadScene("MainMenu");
        Resume();
    }
}
