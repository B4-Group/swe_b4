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
        ResetTriggers();
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
        ResetTriggers();
        doorClose.source.Play();
        isOpen = false;
        _animator.SetTrigger(name: "CloseDoor");
    }

    // Used by InteractibleCircle in nomal doors
    public void ToggleDoor() {
        if(isOpen) {
            Close();
        } else {
            Open();
        }
    }

    public void ResetTriggers() {
        _animator.ResetTrigger(name: "CloseDoor");
        _animator.ResetTrigger(name: "OpenDoor");
    }

    private void OnDestroy() {
        doorOpen.source.Stop();
        doorClose.source.Stop();
    }
}
