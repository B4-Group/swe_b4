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

        // Save data
            string currentScene = SceneManager.GetActiveScene().name;

            // Returns 5th character of string, which is the number of the level
            // Level1 = 0, Level2 = 1, etc.
            int currentLevel = int.Parse(currentScene.Substring(5))-1;
            Debug.Log("Current Level: " + currentLevel);

            int stars = FindObjectOfType<Stars>().GetStarsAmount();
            float time = FindObjectOfType<Timer>().getTimer();
            int hearts = FindObjectOfType<PlayerHealth>().GetHealth();

            Debug.Log("Stars: " + stars);
            Debug.Log("Time: " + time + "s");
            Debug.Log("Hearts: " + hearts + "/3");

            PlayerData data = new PlayerData(stars, time, hearts, currentLevel);
            Debug.Log("Checking player data: " + data.SaveToString());
            GetComponent<SaveSystem>().Save(data);
            Debug.Log("Checking response from disk: " + GetComponent<SaveSystem>().LoadData().SaveToString());

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
