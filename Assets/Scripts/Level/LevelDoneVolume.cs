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
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //out range
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
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
