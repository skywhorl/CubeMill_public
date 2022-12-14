using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundCtrl : MonoBehaviour
{
    public static SoundCtrl instance;
    public AudioSource BGM;
    public AudioSource SoundEffect;
    public Slider BGMSlider;
    public Slider SoundEffectSlider;
    int BGMVolum;
    int effectsVolum;
    public GameObject audioChange;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        audioChange.SetActive(false);
    }
    public void BGMControl()
    {
        float sound = BGMSlider.value;
        BGM.volume = sound;
        BGMVolum = Mathf.RoundToInt(sound * 10);
        BGMSlider.value = BGMVolum * 0.1f;
    }

    public void SoundEffectControl()
    {
        float sound = SoundEffectSlider.value;
        SoundEffect.volume = sound;
        effectsVolum = Mathf.RoundToInt(sound * 10);
        SoundEffectSlider.value = effectsVolum * 0.1f;
    }

    public void SoundEffectPlay(AudioClip effects)
    {
        SoundEffect.clip = effects;
        SoundEffect.Play();
    }

    public void BGMPlay(AudioClip bgms)
    {
        BGM.Stop();
        BGM.clip = bgms;
        BGM.Play();
    }
}

