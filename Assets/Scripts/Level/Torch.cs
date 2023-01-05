using System;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private Animator _animator;
    
    [SerializeField]
    bool alwaysOn = false;

    private void Awake()
    {
        TorchController.OnTorchToggle += Toggle;

        if(alwaysOn)
            Toggle();
    }

    private void OnDestroy()
    {
        TorchController.OnTorchToggle -= Toggle;
    }

    public void Toggle()
    {
         _animator = gameObject.GetComponent<Animator>();
        Debug.Log("Toggling Torch");
        _animator.SetBool("isOn", !_animator.GetBool("isOn"));
    }
}
