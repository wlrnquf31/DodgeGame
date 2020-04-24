using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    private AudioSource myAudio;

    public AudioClip hitShieldSound;
    public AudioClip hitCharacterSound;
    public AudioClip pickUpSound;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void PlayHitShieldSound()
    {
        myAudio.PlayOneShot(hitShieldSound);
    }

    public void PlayHitCharacterSound()
    {
        myAudio.PlayOneShot(hitCharacterSound);
    }

    public void PlayPickUpSound()
    {
        myAudio.PlayOneShot(pickUpSound);
    }
}
