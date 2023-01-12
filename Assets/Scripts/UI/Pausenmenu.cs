using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Pausenmenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private IMGUIContainer pauseMenu;
    public Button resume;
    public Button restart;
    public Button levelAuswahl;
    public Button hauptMenu;
    public Button pauseButton;

    [SerializeField]
    private Rigidbody2D player;

    public GameObject zombiePrefab;
    public GameObject starPrefab;

    private Vector3 origPosition;
    private Vector3[] zombiePositions;
    private Vector3[] starPositions;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        pauseMenu = root.Q<IMGUIContainer>("pauseMenu");

        resume = root.Q<Button>("resume");
        restart = root.Q<Button>("restart");
        levelAuswahl = root.Q<Button>("levelAuswahl");
        hauptMenu = root.Q<Button>("hauptMenu");
        pauseButton = root.Q<Button>("pauseButton");

        resume.clicked += Resume;
        restart.clicked += NeueStarten;
        levelAuswahl.clicked += LoadLevelauswahl;
        hauptMenu.clicked += HauptMenu;
        pauseButton.clicked += Pause;

        pauseMenu.style.visibility = Visibility.Hidden;

        origPosition = player.transform.position;

        // Get Zombie positions by "Enemy" tag
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
        zombiePositions = new Vector3[zombies.Length];
        for (int i = 0; i < zombies.Length; i++)
        {
            zombiePositions[i] = zombies[i].transform.position;
        }

        // Get Star positions by "Star" tag
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
        starPositions = new Vector3[stars.Length];
        for (int i = 0; i < stars.Length; i++)
        {
            starPositions[i] = stars[i].transform.position;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }
    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("click");
        FindObjectOfType<SliderEvents>().SetVisible(false);
        FindObjectOfType<SliderEvents>().visible = false;
        pauseMenu.style.visibility = Visibility.Hidden;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        FindObjectOfType<AudioManager>().Play("pause");
        FindObjectOfType<SliderEvents>().SetVisible(true);
        pauseMenu.style.visibility = Visibility.Visible;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void NeueStarten()
    {
         
        player.transform.position = origPosition;

        PlayerHealth playerHealthController = FindObjectOfType<PlayerHealth>();
       

        playerHealthController.Reset();

        // Destroy Zombies
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject zombie in zombies)
        {
            Destroy(zombie);
        }
        // Instantiate Zombies at original positions
        foreach(Vector3 position in zombiePositions)
        {
            GameObject zombie = Instantiate(zombiePrefab, position, Quaternion.identity);
            zombie.tag = "Enemy";
            zombie.GetComponent<Pathfinding.AIDestinationSetter>().target = player.transform;
        }

        // Destroy Stars
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
        foreach (GameObject star in stars)
        {
            Destroy(star);
        }
        // Instantiate Stars at original positions
        foreach (Vector3 position in starPositions)
        {
            Instantiate(starPrefab, position, Quaternion.identity);
        }

        // Reset all doors
        DoorController[] doors = FindObjectsOfType<DoorController>();
        foreach (DoorController door in doors)
        {
            try{
                door.Close();
            }catch(System.Exception e){
                Debug.Log(e);
            }
        }
        // Reset puzzle doors
        PuzzelController[] puzzelDoors = FindObjectsOfType<PuzzelController>();
        foreach (PuzzelController puzzelDoor in puzzelDoors)
        {
            puzzelDoor.ResetPuzzle();
        }

        PuzzleUiController puzzleUiController = FindObjectOfType<PuzzleUiController>();
        puzzleUiController.Reset();

        Stars.ResetStar();
        Timer.ResetTimer();

        FindObjectOfType<AudioManager>().StopAll();
        string restartMusicName = FindObjectOfType<LevelController>().GetMusicName();
        FindObjectOfType<AudioManager>().Play(restartMusicName);

        Resume();
    }
    public void LoadLevelauswahl()
    {
        Resume();
        // Make levelauswahl visible
        FindObjectOfType<LevelauswahlScript>().MakeVisible();
    }
    public void HauptMenu()
    {
        FindObjectOfType<AudioManager>().StopAll();
        LevelController levelController = FindObjectOfType<LevelController>();
        SceneManager.LoadScene("MainMenu");
        Resume();
    }
}
