using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

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
        if(instance == null)
        {
            instance = this;
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
        EffectSoundSlider();
        BGMSoundSlider();
    }

    private void InitSlider()
    {
        effectSlider.value = 0.5f;
        bgmSlider.value = 0.5f;
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
        effectAudio.volume = effectSlider.value;
    }

    public void BGMSoundSlider()
    {
        bgmAudio.volume = bgmSlider.value;
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
