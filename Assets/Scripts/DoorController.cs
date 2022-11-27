using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator _animator;
    private bool isOpen = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    [ContextMenu(itemName:"OpenDoor")]
    public void Open()
    {
        isOpen = true;
        _animator.SetTrigger(name: "OpenDoor"); ;
    }

    [ContextMenu(itemName:"CloseDoor")]
    public void Close()
    {
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
