using System;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private Animator _animator;
    
    [SerializeField]
    bool alwaysOn = false;

    private void Awake()
    {
        if(alwaysOn)
            Toggle();
        else
            TorchController.OnTorchToggle += Toggle;
    }

    private void OnDestroy()
    {
        TorchController.OnTorchToggle -= Toggle;
    }

    public void Toggle()
    {
        _animator = gameObject.GetComponent<Animator>();
        _animator.SetBool("isOn", !_animator.GetBool("isOn"));
        FindObjectOfType<AudioManager>().Play("torch_lit");
    }
}
