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

        // Save data
            string currentScene = SceneManager.GetActiveScene().name;

            // Returns 5th character of string, which is the number of the level
            // Level1 = 0, Level2 = 1, etc.
            int currentLevel = int.Parse(currentScene.Substring(5))-1;

            int stars = FindObjectOfType<Stars>().GetStarsAmount();
            float time = FindObjectOfType<Timer>().getTimer();
            int hearts = FindObjectOfType<PlayerHealth>().GetHealth();

            //PlayerData data = new PlayerData(stars, time, hearts, currentLevel);
            PlayerData data = GetComponent<SaveSystem>().LoadData();

            // update the data with new values
            data.stars[currentLevel] = stars;
            data.time[currentLevel] = time;
            data.hearts[currentLevel] = hearts;

            GetComponent<SaveSystem>().Save(data);

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
