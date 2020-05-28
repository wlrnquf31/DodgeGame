using DataInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    GameData data = new GameData();

    public static SoundManager instance;

    private AudioSource effectAudio;
    private AudioSource bgmAudio;

    public Slider effectSlider;
    public Slider bgmSlider;

    public AudioClip hitShieldSound;
    public AudioClip hitCharacterSound;
    public AudioClip pickUpSound;
    public AudioClip bgmSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitSlider();
        InitEffect();
        InitBgm();
    }

    private void Update()
    {
        if(UiManager.instance.optionMenu.activeInHierarchy)
        {
            EffectSoundSlider();
            BGMSoundSlider();
        }
    }

    private void InitSlider()
    {
        data = DataManager.instance.Load();

        effectSlider.value = data.effectVolume;
        bgmSlider.value = data.bgmVolume;
    }

    private void InitBgm()
    {
        GameObject child = new GameObject("BGM");
        child.transform.SetParent(transform);
        bgmAudio = child.AddComponent<AudioSource>();
        bgmAudio.volume = bgmSlider.value;
        bgmAudio.loop = true;
        bgmAudio.clip = bgmSound;
        bgmAudio.Play();
    }

    private void InitEffect()
    {
        effectAudio = GetComponent<AudioSource>();
        effectAudio.volume = effectSlider.value;
    }

    public void BGMToggle()
    {
        if(bgmAudio.isPlaying)
        {
            bgmAudio.Pause();
        }
        else
        {
            bgmAudio.Play();
        }
        
    }

    public void EffectSoundSlider()
    {
        data = DataManager.instance.Load();

        effectAudio.volume = effectSlider.value;
        data.effectVolume = effectSlider.value;
        DataManager.instance.Save(data);
    }

    public void BGMSoundSlider()
    {
        data = DataManager.instance.Load();

        bgmAudio.volume = bgmSlider.value;
        data.bgmVolume = bgmSlider.value;
        DataManager.instance.Save(data);
    }

    public void PlayHitShieldSound()
    {
        effectAudio.PlayOneShot(hitShieldSound);
    }

    public void PlayHitCharacterSound()
    {
        effectAudio.PlayOneShot(hitCharacterSound);
    }

    public void PlayPickUpSound()
    {
        effectAudio.PlayOneShot(pickUpSound);
    }
}
