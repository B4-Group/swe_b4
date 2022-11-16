using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stars : MonoBehaviour
{
    public static Button firstStar;
    public static Button secondStar;
    public static Button thirdStar;

    private static int countStars;


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
        countStars = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void CollectedStars()
    {
        if (countStars == 0)
        {
            firstStar.style.visibility = Visibility.Visible;
            countStars++;

        }
        else if (countStars == 1)
        {
            secondStar.style.visibility = Visibility.Visible;
            countStars++;
        }
        else if (countStars == 2)
        {
            thirdStar.style.visibility = Visibility.Visible;
            countStars++;
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
