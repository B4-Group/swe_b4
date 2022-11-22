using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthbarUI : MonoBehaviour
{
    public Transform TransformToFollow;


    private VisualElement m_Bar;
    private VisualElement[] Hearts;
    private void Start()
    {
        m_Bar = GetComponent<UIDocument>().rootVisualElement.Q("Container");
        Hearts = m_Bar.Children().ToArray();
    }

    // Update is called once per frame
    void Update()
    {

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
