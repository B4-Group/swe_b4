using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Star : MonoBehaviour, ICollectible
{
    public static event Action OnStarCollected;
    public void collect()
    {
        Debug.Log("Star collected");
        OnStarCollected?.Invoke();
        Destroy(gameObject);
    }


}
