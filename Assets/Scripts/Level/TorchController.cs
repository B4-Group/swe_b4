using System;
using UnityEngine;
using UnityEngine.UIElements;

public class TorchController : MonoBehaviour
{
    public static event Action OnTorchToggle;
    public Button helpButton;

    void Awake()
    {
        // get all torches ingame through tags
        var root = GetComponent<UIDocument>().rootVisualElement;

        helpButton = root.Q<Button>("helpButton");
        helpButton.clicked += Toggle;
    }

    public void Toggle()
    {
        Debug.Log("Invoking Torch Toggle");
        OnTorchToggle?.Invoke();
    }
}
