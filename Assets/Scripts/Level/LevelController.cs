using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelController : MonoBehaviour
{
    public Sound LevelMusic { get; set; }
    public Sound LevelDoneSound;
    
    public string musicName;

    void Start()
    {
        Debug.Log("Music name: " + musicName);
        if(musicName != null)
            PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Plays default Level Music
    public virtual void PlayMusic()
    {
        Debug.Log("Playing music");
        LevelMusic = FindObjectOfType<AudioManager>().getSound(musicName);
        LevelMusic.source.Play();
    }

    // LevelDone Loads the LevelDone Popup
    // Until LevelDone is actually done, it will simply load Levelauswahl
    public void LevelDone()
    {
        LevelMusic.source.Stop();
        LevelDoneSound = FindObjectOfType<AudioManager>().getSound("levelDoneSound");
        LevelDoneSound.source.Play();
        Debug.Log("Level is done");
        SceneManager.LoadScene("LevelDoneScene");
    }
}
