using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameCompleted : MonoBehaviour
{
    public bool gameCompleted = false;
    
    // Current level in the game, starts at 0
    private int currentLevel;
    
    // Amount of levels in the game
    private const int maxLevel = 1;

    PlayerData data;

    private void OnEnable()
    {
        // Get next scene from save system
        data = GetComponent<SaveSystem>().LoadData();

        // increment the level counter
        if(data.highestLevel < maxLevel && data.currentLevel == data.highestLevel) {
            data.highestLevel += 1;
        }
            

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

        // Add stars to the star container

        // Get stars from previous level
        int starAmount = data.stars[currentLevel];
        float time = data.time[currentLevel];
        int hearts = data.hearts[currentLevel];

        VisualElement starsContainer = root.Q<VisualElement>("starContainer");

        // Clears starsContainer
        starsContainer.Clear();

        // Add stars
        for (int i = 0; i < starAmount; i++) {
            Texture2D star = Resources.Load<Texture2D>("star");
            Image starImage = new();
            starImage.style.backgroundImage = new StyleBackground(star);
            starImage.style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;
            starImage.style.height = 32;
            starImage.style.width = 32;
            starsContainer.Add(starImage);
        }

        // Fill up with grey stars
        if(starAmount < 3) {
            for (int i = 0; i < 3 - starAmount; i++) {
                Texture2D star = Resources.Load<Texture2D>("starGrey");
                Image starImage = new();
                starImage.style.backgroundImage = new StyleBackground(star);
                starImage.style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;
                starImage.style.height = 32;
                starImage.style.width = 32;
                starsContainer.Add(starImage);
            }
        }

        // Add Hearts
        for (int i = 0; i < hearts; i++) {
            Texture2D heart = Resources.Load<Texture2D>("heart");
            Image heartImage = new();
            heartImage.style.backgroundImage = new StyleBackground(heart);
            heartImage.style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;
            heartImage.style.height = 32;
            heartImage.style.width = 32;
            starsContainer.Add(heartImage);
        }

        // Fill up with grey hearts
        if(hearts < 3) {
            for (int i = 0; i < 3 - hearts; i++) {
                Texture2D heart = Resources.Load<Texture2D>("heartGrey");
                Image heartImage = new();
                heartImage.style.backgroundImage = new StyleBackground(heart);
                heartImage.style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;
                heartImage.style.height = 32;
                heartImage.style.width = 32;
                starsContainer.Add(heartImage);
            }
        }

        // Add time
        Label timeText = new();
        // Format time to mm:ss
        timeText.text = string.Format("{0:00}:{1:00}", Mathf.Floor(time / 60), Mathf.Floor(time % 60));
        timeText.style.unityTextAlign = TextAnchor.MiddleCenter;
        timeText.style.fontSize = 24;
        starsContainer.Add(timeText);

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
        
        // Increment current Level counter
        data.currentLevel += 1;
        GetComponent<SaveSystem>().Save(data);

        string nextSceneName = GetComponent<LevelController>().levels[currentLevel+1];
        SceneManager.LoadScene(nextSceneName);
        Resume();
        
    }

    private void Resume() {
        Time.timeScale = 1f;
        gameCompleted = false;
    }
}
