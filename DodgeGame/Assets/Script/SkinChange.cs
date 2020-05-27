using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DataInfo;
using UnityEngine.UI;

public class SkinChange : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> skins;

    [System.NonSerialized]
    public Sprite skin;

    private GameObject curSelectSkin;

    private GameData data = new GameData();

    private void Start()
    {
        SkinInit();
        ShowCharacterBoard();
    }

    public void ClickSkin()
    {
        curSelectSkin = EventSystem.current.currentSelectedGameObject;
    }

    public void ChangeSkin()
    {
        skin = curSelectSkin.GetComponent<Sprite>();
        ShowCharacterBoard();
    }

    private void ShowCharacterBoard()
    {
        gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = skin;
        gameObject.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().sizeDelta = skin.rect.size;
    }

    public void SkinInit()
    {
        data = DataManager.instance.Load();

        if(data.currentSkin == null)
        {
            data.currentSkin = skins[0];
        }

        skin = data.currentSkin;
        GameManager.instance.skin = skin;
    }
}
