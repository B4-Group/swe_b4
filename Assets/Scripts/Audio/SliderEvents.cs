using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SliderEvents : MonoBehaviour
{
    private Slider sliderMusic;
    private Slider sliderSfx;
    private Toggle toggleMusic;
    private Toggle toggleSfx;
    private Label labelMusic;
    private Label labelSfx;
    private AudioManager audiomanager;
    public bool visible;

    // Start is called before the first frame update
    void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
        var root = GetComponent<UIDocument>().rootVisualElement;

        sliderMusic = root.Q<Slider>("musicSlider");
        sliderSfx = root.Q<Slider>("sfxSlider");
        toggleMusic = root.Q<Toggle>("musicToggle");
        toggleSfx = root.Q<Toggle>("sfxToggle");

        labelMusic = root.Q<Label>("musicLabel");
        labelSfx = root.Q<Label>("sfxLabel");

        if(visible)
        {
            sliderMusic.visible = true;
            sliderSfx.visible = true;
            toggleMusic.visible = true;
            toggleSfx.visible = true;
            labelMusic.visible = true;
            labelSfx.visible = true;
        }
        else
        {
            sliderMusic.visible = false;
            sliderSfx.visible = false;
            toggleMusic.visible = false;
            toggleSfx.visible = false;
            labelMusic.visible = false;
            labelSfx.visible = false;
        }

        sliderMusic.value = audiomanager.musicMaster;
        sliderSfx.value = audiomanager.sfxMaster;
        toggleMusic.value = audiomanager.musicEnabled;
        toggleSfx.value = audiomanager.sfxEnabled;

        // Slider Music Listener
        sliderMusic.RegisterCallback<ChangeEvent<float>>((evt)=> 
        {
            audiomanager.musicMaster = sliderMusic.value;
            audiomanager.AdjustSoundVolume();
        });

        // Toggle Music Listener
        toggleMusic.RegisterCallback<ChangeEvent<bool>>((evt) =>
        {
            audiomanager.musicEnabled = toggleMusic.value;
            audiomanager.enableMusic(toggleMusic.value);
            audiomanager.Play("click");
        });

        // s. Slider Music Listener
        sliderSfx.RegisterCallback<ChangeEvent<float>>((evt) =>
        {
            audiomanager.sfxMaster = sliderSfx.value;
            audiomanager.AdjustSoundVolume();
        });

        // s. Toggle Music Listener
        toggleSfx.RegisterCallback<ChangeEvent<bool>>((evt) =>
        {
            audiomanager.Play("click");
            audiomanager.sfxEnabled = toggleSfx.value;
            audiomanager.Play("click");
        });
    }

    public void SetVisible(bool value)
    {
        visible = value;
        Start();
    }
}
