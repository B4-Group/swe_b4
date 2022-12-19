using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameCompleted : MonoBehaviour
{
    public bool gameCompleted = false;
    
    // Current level in the game, starts at 0
    private int currentLevel;
    
    // Amount of levels in the game
    private const int maxLevel = 2;

    private void OnEnable()
    {
        // Get next scene from save system
        PlayerData data = GetComponent<SaveSystem>().LoadData();
        // increment the level counter
        if (data.currentLevel < maxLevel)
            data.currentLevel += 1;
        // save the modified level counter back to disk
        GetComponent<SaveSystem>().Save(data);
        // set local property to the new level counter
        currentLevel = data.currentLevel;

        try {
            FindObjectOfType<AudioManager>().Stop("step");
        } catch (System.Exception e) {
            Debug.Log("No AudioManager found, probably in editor. " + e);
        }

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button NextLevel = root.Q<Button>("Nextlevel");
        Button Hauptmenu = root.Q<Button>("Hauptmenu");

        if ((currentLevel) == maxLevel) {
            NextLevel.style.display = DisplayStyle.None;
            Hauptmenu.clicked += () => mainmenu();
        } else {
            Hauptmenu.clicked += () => mainmenu();
            NextLevel.clicked += () => nextlevel();
        }

        // Get Score data from the saved system 
        int scoreLevel = currentLevel == maxLevel ? currentLevel - 1 : currentLevel;
        int stars = data.stars[scoreLevel];
        float time = data.time[scoreLevel];
        int hearts = data.hearts[scoreLevel];


        // Display the time take to complete the level
        Label firstMinute = root.Q<Label>("firstMinute");
        Label secondMinute = root.Q<Label>("secondMinute");
        Label firstSecond = root.Q<Label>("firstSecond");
        Label secondSecond = root.Q<Label>("secondSecond");

        float minute = Mathf.FloorToInt(time / 60);
        float second = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}", minute, second);

        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();

        // Display stars according the number of collected stars
        Button firstStar = root.Q<Button>("firstScoreStar");
        Button secondStar = root.Q<Button>("secondScoreStar");
        Button thirdStar = root.Q<Button>("thirdScoreStar");

        firstStar.style.visibility = Visibility.Hidden;
        secondStar.style.visibility = Visibility.Hidden;
        thirdStar.style.visibility = Visibility.Hidden;

        if (stars >= 1)
        {
            firstStar.style.visibility = Visibility.Visible;
        }
        if (stars >= 2)
        {
            secondStar.style.visibility = Visibility.Visible;
        }
        if (stars >= 3)
        {
            thirdStar.style.visibility = Visibility.Visible;
        }

        // Display hearts according the number of leftover hearts
        Button firstHeart = root.Q<Button>("firstScoreHeart");
        Button secondHeart = root.Q<Button>("secondScoreHeart");
        Button thirdHeart = root.Q<Button>("thirdScoreHeart");

        firstHeart.style.visibility = Visibility.Hidden;
        secondHeart.style.visibility = Visibility.Hidden;
        thirdHeart.style.visibility = Visibility.Hidden;

        if (hearts >= 1)
        {
            firstHeart.style.visibility = Visibility.Visible;
        }
        if (hearts >= 2)
        {
            secondHeart.style.visibility = Visibility.Visible;
        }
        if (hearts >= 3)
        {
            thirdHeart.style.visibility = Visibility.Visible;
        }

        Debug.Log("ScoreLevel: " + scoreLevel + "\nTime taken: " + currentTime + "\nStars: " + stars + "\nhearts: " + hearts);

    }

    private void mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
        Resume();
        
    }
    private void nextlevel()
    {
        if(currentLevel+1 > maxLevel) {
            Debug.Log("No more levels");
            return;
        }
        string nextSceneName = GetComponent<LevelController>().levels[currentLevel];
        SceneManager.LoadScene(nextSceneName);
        Resume();
        
    }

    private void Resume() {
        Time.timeScale = 1f;
        gameCompleted = false;
    }
}
