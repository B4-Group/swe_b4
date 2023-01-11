using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Star : MonoBehaviour, ICollectible
{
    public static event Action OnStarCollected;
    private Sound collectSound;

    private void Start()
    {
        collectSound = FindObjectOfType<AudioManager>().GetSound("starCollect");
    }

    public void collect()
    {
        try {
            collectSound.source.Play();
        } catch(System.Exception e) {
            Debug.Log("If I got a penny for every audio error...");
        }
        
        OnStarCollected?.Invoke();
        Destroy(gameObject);
    }


}
