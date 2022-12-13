using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public string musicName;

    // Array with Scene names of Levels to show
    // Will overwrite whatever is written in the inspector

    [HideInInspector]
    public string[] levels;

    void Start()
    {
        ForceRefreshLevelList();
        try {
            // Only play if current scene is in the levels array
            if (System.Array.IndexOf(levels, SceneManager.GetActiveScene().name) != -1)
            Debug.Log($"Playing {musicName}");
                FindObjectOfType<AudioManager>().Play(musicName);
        } catch (System.Exception e) {
            Debug.Log("No AudioManager found, probably in editor");
        }
        
    }

    public void ForceRefreshLevelList() {
        levels = new string[2] { "Tempel des Todes", "Ruine der Angst" };
        Debug.Log("LevelController levels: " + string.Join(", ", levels));
    }

    // Returns the level number of the level with the given name
    // Returns -1 if the name is not found
    public int GetLevelNumber(string levelname) {
        Debug.Log("Querying levels: " + string.Join(", ", levels) + " for " + levelname);
        return System.Array.IndexOf(levels, levelname);
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

            int currentLevel = GetLevelNumber(currentScene);
            Debug.Log("Current level number: " + currentLevel + " for name: " + currentScene);
            int stars = FindObjectOfType<Stars>().GetStarsAmount();
            float time = FindObjectOfType<Timer>().getTimer();
            int hearts = FindObjectOfType<PlayerHealth>().GetHealth();

            //PlayerData data = new PlayerData(stars, time, hearts, currentLevel);
            PlayerData data = GetComponent<SaveSystem>().LoadData();
            Debug.Log("Loaded current Level: " + currentLevel + " with " + string.Join(",", stars) + " stars, " + string.Join(",", time) + " time and " + string.Join(",", hearts) + " hearts");
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
