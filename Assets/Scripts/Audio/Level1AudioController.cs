using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1AudioController : MonoBehaviour
{
    private Sound levelMusic;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Playing music");
        levelMusic = FindObjectOfType<AudioManager>().getSound("level1Music");
        levelMusic.source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
