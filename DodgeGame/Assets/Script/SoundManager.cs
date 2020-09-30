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

    public AudioSource effectAudio;
    public AudioSource bgmAudio;

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
        InitSound();
    }

    public void InitSound()
    {
        data = DataManager.instance.Load();

        InitEffect(data.effectVolume);
        InitBgm(data.bgmVolume);
    }

    private void InitBgm(float bgmValue)
    {
        GameObject child = new GameObject("BGM");
        child.transform.SetParent(transform);
        bgmAudio = child.AddComponent<AudioSource>();
        bgmAudio.volume = bgmValue;
        bgmAudio.loop = true;
        bgmAudio.clip = bgmSound;
        bgmAudio.Play();
    }

    private void InitEffect(float effectValue)
    {
        effectAudio = GetComponent<AudioSource>();
        effectAudio.volume = effectValue;
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
