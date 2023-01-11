using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stars : MonoBehaviour
{
    public static Button firstStar;
    public static Button secondStar;
    public static Button thirdStar;

    public static int countStars = 0;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        firstStar = root.Q<Button>("firstStar");
        secondStar = root.Q<Button>("secondStar");
        thirdStar = root.Q<Button>("thirdStar");

        
        Texture2D starGrey = Resources.Load<Texture2D>("starGrey");

        firstStar.style.backgroundImage = new StyleBackground(starGrey);
        secondStar.style.backgroundImage = new StyleBackground(starGrey);
        thirdStar.style.backgroundImage = new StyleBackground(starGrey);

    }

    private void OnEnable()
    {
        Star.OnStarCollected += CollectedStars;
    }

    private void OnDisable()
    {
        Star.OnStarCollected -= CollectedStars;
    }

    public static void CollectedStars()
    {
        FindObjectOfType<AudioManager>().Play("star_collected");
        countStars += 1;

        Texture2D star = Resources.Load<Texture2D>("star");

        if (countStars >= 1)
        {
            firstStar.style.backgroundImage = new StyleBackground(star);
        }
        if (countStars >= 2)
        {
            secondStar.style.backgroundImage = new StyleBackground(star);
        }
        if (countStars >= 3)
        {
            thirdStar.style.backgroundImage = new StyleBackground(star);
        }
    }

    public static void ResetStar()
    {
        Texture2D starGrey = Resources.Load<Texture2D>("starGrey");
        firstStar.style.backgroundImage = new StyleBackground(starGrey);
        secondStar.style.backgroundImage = new StyleBackground(starGrey);
        thirdStar.style.backgroundImage = new StyleBackground(starGrey);
        countStars = 0;
    }

    public int GetStarsAmount() {
        return countStars;
    }
}
