using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;

    private void Start()
    {
        // Play music only if hasn't been started yet
        if (!FindObjectOfType<AudioManager>().IsPlaying("menuMusic"))
        {
            FindObjectOfType<AudioManager>().Play("menuMusic");
        }

        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("play");
        exitButton = root.Q<Button>("exit");

        startButton.clicked += LoadLevelauswahl;
        exitButton.clicked += ExitGame;
    }

    public void LoadLevelauswahl()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Debug.Log("Loading levelauswahl");
        SceneManager.LoadScene("Levelauswahl");
    }

    public void ExitGame()
    {
        FindObjectOfType<AudioManager>().Stop("menuMusic");
        FindObjectOfType<AudioManager>().Play("click");
        Debug.Log("Quit game");
        Application.Quit();
    }
}
