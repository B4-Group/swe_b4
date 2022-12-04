using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public string musicName;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play(musicName);
    }

    // LevelDone Loads the LevelDone Popup
    // Until LevelDone is actually done, it will simply load Levelauswahl
    public void LevelDone()
    {
        FindObjectOfType<AudioManager>().StopAll();
        FindObjectOfType<AudioManager>().Play("win");
        Debug.Log("Level is done");
        SceneManager.LoadScene("LevelDoneScene");
    }

 
}
