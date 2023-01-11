using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;

    private void Start()
    {
        try {
            // Play music only if hasn't been started yet
            if (!FindObjectOfType<AudioManager>().IsPlaying("menuMusic"))
            {
                FindObjectOfType<AudioManager>().Play("menuMusic");
            }
        } catch (System.NullReferenceException e) {
            Debug.Log("No AudioManager found, probably in editor");
        }
  

        FindObjectOfType<SliderEvents>().SetVisible(true);
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("play");
        exitButton = root.Q<Button>("exit");

        startButton.clicked += LoadLevelauswahl;
        exitButton.clicked += ExitGame;
    }

    public void LoadLevelauswahl()
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene("Levelauswahl");
    }

    public void ExitGame()
    {
        FindObjectOfType<AudioManager>().Stop("menuMusic");
        FindObjectOfType<AudioManager>().Play("click");
        Application.Quit();
    }
}
