using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Pausenmenü : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private IMGUIContainer pauseMenu;
    public Button resume;
    public Button restart;
    public Button levelAuswahl;
    public Button hauptMenu;


    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        pauseMenu = root.Q<IMGUIContainer>("pauseMenu");

        resume = root.Q<Button>("resume");
        restart = root.Q<Button>("restart");
        levelAuswahl = root.Q<Button>("levelAuswahl");
        hauptMenu = root.Q<Button>("hauptMenu");

        resume.clicked += Resume;
        restart.clicked += NeueStarten;
        levelAuswahl.clicked += LoadLevelauswahl;
        hauptMenu.clicked += HauptMenu;

        Debug.Log(pauseMenu);
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
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;

    }
    public void NeueStarten()
    {
        Debug.Log("...NeuStarten");
    }
    public void LoadLevelauswahl()
    {
        Debug.Log("Loading levelauswahl");
        SceneManager.LoadScene("Levelauswahl");
    }
    public void HauptMenu()
    {
        Debug.Log("....Hauptmenü");
        SceneManager.LoadScene("MainMenu");
    }
}
