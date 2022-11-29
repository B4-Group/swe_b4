using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stars : MonoBehaviour
{
    public static Button firstStar;
    public static Button secondStar;
    public static Button thirdStar;

    private static int countStars = 0;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        firstStar = root.Q<Button>("firstStar");
        secondStar = root.Q<Button>("secondStar");
        thirdStar = root.Q<Button>("thirdStar");

        firstStar.style.visibility = Visibility.Hidden;
        secondStar.style.visibility = Visibility.Hidden;
        thirdStar.style.visibility = Visibility.Hidden;

    }

    private void OnEnable()
    {
        Debug.Log("Adding listener");
        Star.OnStarCollected += CollectedStars;
    }

    private void OnDisable()
    {
        Star.OnStarCollected -= CollectedStars;
    }

    public static void CollectedStars()
    {
        Debug.Log("Adding star");
        FindObjectOfType<AudioManager>().Play("star_collected");
        countStars += 1;
        Debug.Log($"Current star count: {countStars}");

        if (countStars <= 1)
        {
            firstStar.style.visibility = Visibility.Visible;
        }
        if (countStars == 2)
        {
            secondStar.style.visibility = Visibility.Visible;
        }
        if (countStars >= 3)
        {
            thirdStar.style.visibility = Visibility.Visible;
        }
    }

    public static void ResetStar()
    {
        firstStar.style.visibility = Visibility.Hidden;
        secondStar.style.visibility = Visibility.Hidden;
        thirdStar.style.visibility = Visibility.Hidden;
        countStars = 0;
    }
}
