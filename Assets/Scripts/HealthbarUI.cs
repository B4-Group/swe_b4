using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthbarUI : MonoBehaviour
{
    public Transform TransformToFollow;

    public GameObject ant;
    private PlayerHealth health;

    private VisualElement m_Bar;
    private VisualElement[] Hearts;
    void Start()
    {
        //health = ant.GetComponent<PlayerHealth>();

        m_Bar = GetComponent<UIDocument>().rootVisualElement.Q("Container");
        Hearts = m_Bar.Children().ToArray();
        
        for (int i = 0; i < 3; i++)
        {
            Hearts[i].style.visibility = Visibility.Hidden;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (health.curHealth < 3)
        //{
        //    Hearts[0].style.visibility = Visibility.Visible;
        //}
        //if (health.curHealth < 2)
        //{
        //    Hearts[1].style.visibility = Visibility.Visible;
        //}
        //if (health.curHealth < 1)
        //{
        //    Hearts[2].style.visibility = Visibility.Visible;
        //}
    }

    public void AnimateBar(bool increaseHealth)
    {
        if (increaseHealth)
        {
            VisualElement nextHeart = Hearts.Where(x => !x.visible).FirstOrDefault();
            nextHeart.style.visibility = Visibility.Visible;
        }
        else
        {
            VisualElement nextHeart = Hearts.Where(x => x.visible).LastOrDefault();
            nextHeart.style.visibility = Visibility.Hidden;
        }
    }
}
