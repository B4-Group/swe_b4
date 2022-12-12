using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator _animator;
    private bool isOpen = false;

    private Sound doorOpen, doorClose;

    private void Awake()
    {
        doorOpen = FindObjectOfType<AudioManager>().GetSound("doorOpen");
        doorClose = FindObjectOfType<AudioManager>().GetSound("doorClose");
        _animator = GetComponent<Animator>();
    }

    [ContextMenu(itemName:"OpenDoor")]
    public void Open()
    {
        try {
            doorOpen.source.Play();
        } catch (System.Exception e) {
            Debug.Log("I refuse to bother with this shit. Here is the error: " + e);
        }
        
        isOpen = true;
        _animator.SetTrigger(name: "OpenDoor");
    }

    [ContextMenu(itemName:"CloseDoor")]
    public void Close()
    {
        doorClose.source.Play();
        isOpen = false;
        _animator.SetTrigger(name: "CloseDoor");
    }

    public void ToggleDoor()
    {
        if(isOpen)
        {
            Close();
        } else
        {
            Open();
        }
    }
}
