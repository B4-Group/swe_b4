using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public string musicName;

    void Start()
    {
        try {
            FindObjectOfType<AudioManager>().Play(musicName);
        } catch (System.Exception e) {
            Debug.Log("No AudioManager found, probably in editor");
        }
        
    }

    // LevelDone Loads the LevelDone Popup
    // Until LevelDone is actually done, it will simply load Levelauswahl
    public void LevelDone()
    {
        try {
            FindObjectOfType<AudioManager>().StopAll();
            FindObjectOfType<AudioManager>().Play("win");
        } catch (System.Exception e) {
            Debug.Log("No AudioManager found, probably in editor");
        }
        Debug.Log("Level is done");
        SceneManager.LoadScene("LevelDoneScene");
    }

    public void StopLevelMusic() {
        try {
            FindObjectOfType<AudioManager>().Stop(musicName);
        } catch (System.Exception e) {
            Debug.Log("No AudioManager found, probably in editor");
        }
    }
}
