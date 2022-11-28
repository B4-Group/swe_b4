using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TorchController : MonoBehaviour
{
    public static event Action OnTorchToggle;
    public Button helpButton;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
