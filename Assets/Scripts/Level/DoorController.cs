using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator _animator;
    private bool isOpen = false;

    private Sound doorOpen, doorClose;

    private void Start()
    {
        doorOpen = FindObjectOfType<AudioManager>().GetSound("doorOpen");
        doorClose = FindObjectOfType<AudioManager>().GetSound("doorClose");
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    [ContextMenu(itemName:"OpenDoor")]
    public void Open()
    {
        doorOpen.source.Play();
        isOpen = true;
        _animator.SetTrigger(name: "OpenDoor"); ;
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
