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
        skin = curSelectSkin.GetComponentInChildren<Image>().sprite;
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

        skin = skins[data.curSkinIndex];

        //GameManager.instance.skin = skin;
    }
}
