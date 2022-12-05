using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private Animator _animator;
    public static event Action OnTorchTurnOn;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        TorchController.OnTorchToggle += Toggle;
    }

    public void TurnOn()
    {
        Debug.Log("Turn on Torch");
        _animator.SetBool("isOn", true);
    }

    public void TurnOff()
    {
        Debug.Log("Turn off Torch");
        _animator.SetBool("isOn", false);
    }

    public void Toggle()
    {
        Debug.Log("Toggling Torch");
        _animator.SetBool("isOn", !_animator.GetBool("isOn"));
        if(_animator.GetBool("isOn"))
        {
            FindObjectOfType<AudioManager>().Play("torch_lit");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("torch_extinguish");
        }
    }
}
