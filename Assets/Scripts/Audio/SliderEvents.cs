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

        sliderMusic.value = audiomanager.GetMusicMaster();
        sliderSfx.value = audiomanager.GetSoundMaster();

        toggleMusic.value = audiomanager.musicMaster > 0 ? true : false;
        toggleSfx.value = audiomanager.sfxMaster > 0 ? true : false;

        // Slider Music Listener
        sliderMusic.RegisterCallback<ChangeEvent<float>>((evt)=> 
        {
            /*  4 Instruktionen: SliderMusic
             *      - prev setzen
             *      - musicMaster auf slider setzen
             *      - toggle entweder an oder aus setzen
             *      - volume anpassen
             */

            // prev = musicMaster
            audiomanager.prevMusicVolume = audiomanager.musicMaster;
            // musicMaster = slider
            audiomanager.musicMaster = sliderMusic.value;
            // toggle auf an oder aus setzen
            toggleMusic.value = audiomanager.musicMaster > 0 ? true : false;
            // volume wird aktualisiert
            audiomanager.AdjustSoundVolume();
        });

        // Toggle Music Listener
        toggleMusic.RegisterCallback<ChangeEvent<bool>>((evt) =>
        {
            // slider auf prev oder 0 setzen -> slider Listener wird aufgerufen
            sliderMusic.value = toggleMusic.value ? audiomanager.prevMusicVolume : 0;
            audiomanager.Play("click");
        });

        // s. Slider Music Listener
        sliderSfx.RegisterCallback<ChangeEvent<float>>((evt) =>
        {
            audiomanager.prevSfxVolume = audiomanager.sfxMaster;
            audiomanager.sfxMaster = sliderSfx.value;
            toggleSfx.value = audiomanager.sfxMaster > 0 ? true : false;
            audiomanager.AdjustSoundVolume();
        });

        // s. Toggle Music Listener
        toggleSfx.RegisterCallback<ChangeEvent<bool>>((evt) =>
        {
            sliderSfx.value = toggleSfx.value ? audiomanager.prevSfxVolume : 0;
            audiomanager.Play("click");
        });
    }

    public void SetVisible(bool value)
    {
        visible = value;
        Start();
    }
}
