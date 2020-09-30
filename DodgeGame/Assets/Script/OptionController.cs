using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataInfo;
using UnityEngine.SceneManagement;

public class OptionController : MonoBehaviour
{
    GameData data = new GameData();

    public Slider effectSlider;
    public Slider bgmSlider;

    public GameObject joyStickToggle;

    private void Start()
    {
        InitSlider();

#if UNITY_ANDROID || UNITY_IOS
        if(joyStickToggle != null)
        {   
            joyStickToggle.SetActive(true);
        }
#endif
    }

    private void Update()
    {
        EffectSoundSlider();
        BGMSoundSlider();
    }

    private void InitSlider()
    {
        data = DataManager.instance.Load();

        effectSlider.value = data.effectVolume;
        bgmSlider.value = data.bgmVolume;
    }

    public void EffectSoundSlider()
    {
        data = DataManager.instance.Load();

        SoundManager.instance.effectAudio.volume = effectSlider.value;
        data.effectVolume = effectSlider.value;
        DataManager.instance.Save(data);
    }

    public void BGMSoundSlider()
    {
        data = DataManager.instance.Load();

        SoundManager.instance.bgmAudio.volume = bgmSlider.value;
        data.bgmVolume = bgmSlider.value;
        DataManager.instance.Save(data);
    }

    public void OnClickBack()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("OptionScene"));
    }
}
