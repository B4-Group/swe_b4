using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public string musicName;

    [HideInInspector]
    public string[] levels;

    void Start()
    {
        ForceRefreshLevelList();
        try {
            // Only play if current scene is in the levels array
            //if (System.Array.IndexOf(levels, SceneManager.GetActiveScene().name) != -1)
                Time.timeScale = 1f;
                FindObjectOfType<AudioManager>().Play(musicName);
                FindObjectOfType<Pausenmenu>().Resume();
        } catch (System.Exception e) {
            Debug.Log("No AudioManager found, probably in editor");
        }
        
    }

    public void ForceRefreshLevelList() {
        levels = new string[2] { "Tempel des Todes", "Ruine der Angst" };
    }

    // Returns the level number of the level with the given name
    // Returns -1 if the name is not found
    public int GetLevelNumber(string levelname) {
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
            int stars = FindObjectOfType<Stars>().GetStarsAmount();
            float time = FindObjectOfType<Timer>().getTimer();
            int hearts = FindObjectOfType<PlayerHealth>().GetHealth();

            //PlayerData data = new PlayerData(stars, time, hearts, currentLevel);
            PlayerData data = GetComponent<SaveSystem>().LoadData();
            // update the data with new values
            data.stars[currentLevel] = stars;
            data.time[currentLevel] = time;
            data.hearts[currentLevel] = hearts;

            // update highest level if necessary
            int highestLevel = data.highestLevel;
            if (currentLevel > highestLevel) {
                data.highestLevel = currentLevel;
            }

            GetComponent<SaveSystem>().Save(data);

        SceneManager.LoadScene("LevelDoneScene");
    }

    public void LoadLevelauswahl() {
        try {
            FindObjectOfType<AudioManager>().StopAll();
            FindObjectOfType<AudioManager>().Play("win");
        } catch (System.Exception e) {
            Debug.Log("No AudioManager found, probably in editor");
        }
        SceneManager.LoadScene("Levelauswahl");
    }

    public void StopLevelMusic() {
        try {
            FindObjectOfType<AudioManager>().Stop(musicName);
        } catch (System.Exception e) {
            Debug.Log("No AudioManager found, probably in editor");
        }
    }
}
