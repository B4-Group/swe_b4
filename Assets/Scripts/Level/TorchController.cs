using System;
using UnityEngine;
using UnityEngine.UIElements;

public class TorchController : MonoBehaviour
{
    public static event Action OnTorchToggle;
    public Button helpButton;

    private bool isOn = false;

    void Awake()
    {
        // get all torches ingame through tags
        var root = GetComponent<UIDocument>().rootVisualElement;

        helpButton = root.Q<Button>("helpButton");
        helpButton.clicked += Toggle;
    }

    void OnDestroy()
    {
        helpButton.clicked -= Toggle;
    }

    public void Toggle()
    {
        isOn = !isOn;
        OnTorchToggle?.Invoke();
    }

    public void ResetTorches() {
        if(isOn) {
            Toggle();
        }
    }
}
