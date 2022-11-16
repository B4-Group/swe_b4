using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelButton : VisualElement
{

    public Label LevelLabel;
    public string levelGuid = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public LevelButton(string levelName="No name provided")
    {
        LevelLabel.text = levelName;
        Add(LevelLabel);
        AddToClassList("#levelButton");
    }
}
