using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelDoneVolume : MonoBehaviour
{
    private bool isInRange = false;
    public UnityEvent interactAction;

    private void OnTriggerEnter2D(Collider2D collision) //in range
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player now in range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //out range
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player now not in range");
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (isInRange)
        {
            interactAction.Invoke();
        }
    }
}
