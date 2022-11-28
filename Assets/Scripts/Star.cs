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
        collectSound = FindObjectOfType<AudioManager>().getSound("starCollect");
    }

    public void collect()
    {
        Debug.Log("Star collected");
        collectSound.source.Play();
        OnStarCollected?.Invoke();
        Destroy(gameObject);
    }


}
