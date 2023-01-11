using UnityEngine;
using UnityEngine.UIElements;
using PuzzleTypeNamespace;

public class HintDialogue : MonoBehaviour
{
    private bool init = false;

    private PuzzleType puzzleType;

    private GroupBox hintDialogue;
    private Label hintText;
    // button does not activate on click while eventsystem/stand alone input system exist/are active?
    // currently using a toggle function instead
    private Button okButton;

    private void Initialize()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        hintDialogue = root.Q<GroupBox>("HintDialogue");
        okButton = root.Q<Button>("HintOkButton");
        hintText = root.Q<Label>("HintText");


        if (hintDialogue == null || okButton == null || hintText == null)
            throw new System.Exception("Missing Hint Dialogue elements in UI Document (HintDialogue: " + (hintDialogue != null ? "ok" : "missing") + ", HintOkButton: " + (okButton != null ? "ok" : "missing") + ", HintText: " + (hintText != null ? "ok" : "missing") + ")");

        okButton.clicked += CloseHint;
    }

    // Finds Visual Elements belonging to Hint Dialogue 
    void Start()
    {
        if (!init) Initialize();
        //okButton.pickingMode = PickingMode.Position;
        //openHint("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. \nDuis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ");
    }

    public void SetPuzzleType(PuzzleType puzzleType)
    {
        this.puzzleType = puzzleType;
    }

    // Opens or closes a dialogue
    public void ToggleVisibility(string content = null)
    {
        if (hintDialogue.style.visibility == Visibility.Visible) CloseHint();
        else OpenHint(content);
    }

    // Open a Dialogue box with content 
    public void OpenHint(string content = null)
    {
        if (!init) Initialize();
        // open hint for current puzzle if no text is given
        if (content == null)
        {
            switch (puzzleType)
            {
                case PuzzleType.Calculator:
                    hintText.text = "Um dieses Puzzle zu l\u00F6sen, musst du den Wert des gesuchten Symbols angeben. Sieh dir dazu die angegebenen Gleichungen an.\nWenn du deine Eingabe gemacht hast, klicke auf Eingabe Pr\u00FCfen um fortzufahren.";
                    break;
                case PuzzleType.SimonSays:
                    hintText.text = "Um dieses Puzzle zu l\u00F6sen, musst du f\u00FCnf mal das richtige Symbole anklicken. Welches Symbol als n\u00E4chstes dran ist, wird dir auf der rechten Seite angezeigt.\nAchtung: du hast nur 10 Sekunden Zeit um ein Symbol auszuw\u00E4hlen!";
                    break;
                case PuzzleType.Information:
                    hintText.text = "Um dieses Puzzle zu l\u00F6sen musst du die korrekte Antwort auf die angezeigte Frage geben. Klicke auf Eingabe Pr\u00FCfen wenn du deine Antwort eingegeben hast.";
                    break;
                default:
                    return;
            }
        } else hintText.text = content;
        hintDialogue.style.visibility = Visibility.Visible;
    }

    // Closes hint dialogue, is automatically called by clicking "OK" button 
    public void CloseHint()
    {
        hintDialogue.style.visibility = Visibility.Hidden;
        hintText.text = "";
    }

}