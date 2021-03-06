﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DataInfo;
using UnityEngine.UI;

public class SkinChange : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> skins;

    private GameObject curSelectSkin;

    private GameData data = new GameData();

    [SerializeField]
    private GameObject curSkinBoard;

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
        data = DataManager.instance.Load();
        data.curSkinIndex = int.Parse(curSelectSkin.name);

        UiManager.instance.SkinChangeUiController();
        GameManager.instance.skin = curSelectSkin.transform.GetChild(0).GetComponent<Image>().sprite;
        ShowCharacterBoard();

        DataManager.instance.Save(data);
    }

    private void ShowCharacterBoard()
    {
        curSkinBoard.GetComponent<Image>().sprite = GameManager.instance.skin;
        curSkinBoard.GetComponent<RectTransform>().sizeDelta = GameManager.instance.skin.rect.size;
    }

    public void SkinInit()
    {
        data = DataManager.instance.Load();

        GameManager.instance.skin = skins[data.curSkinIndex];
    }
}
