using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public string musicName;

    void Start()
    {
        Debug.Log("Music name: " + musicName);
        if (musicName != null)
            PlayMusic();
        else
            Debug.Log("Couldnt play music " + musicName);
    }

    // Plays default Level Music
    public void PlayMusic()
    {
        Debug.Log("Playing music" + musicName);
        FindObjectOfType<AudioManager>().Play(musicName);
    }
    public void StopLevelMusic()
    {
        FindObjectOfType<AudioManager>().StopSound(musicName);
    }

    // LevelDone Loads the LevelDone Popup
    // Until LevelDone is actually done, it will simply load Levelauswahl
    public void LevelDone()
    {
        StopLevelMusic();
        FindObjectOfType<AudioManager>().Play("levelDoneSound");
        Debug.Log("Level is done");
        SceneManager.LoadScene("LevelDoneScene");
    }

 
}
