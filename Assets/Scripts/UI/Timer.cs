using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public float timer;

    public Label firstMinute;
    public Label secondMinute;
    public Label separator;
    public Label firstSecond;
    public Label secondSecond;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
        var root = GetComponent<UIDocument>().rootVisualElement;

        firstMinute = root.Q<Label>("firstMinute");
        secondMinute = root.Q<Label>("secondMinute");
        firstSecond = root.Q<Label>("firstSecond");
        secondSecond = root.Q<Label>("secondSecond");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimerDisplay(timer);
    }

    void UpdateTimerDisplay(float time)
    {
        float minute = Mathf.FloorToInt(time / 60);
        float second = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}", minute, second);

        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }

    public void ResetTimer()
    {
        timer = 0;
    }
    public float getTimer()
    {
        return timer;
    }
}
