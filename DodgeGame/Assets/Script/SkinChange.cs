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
        GameManager.instance.skin = curSelectSkin.transform.GetChild(0).GetComponent<Image>().sprite;
        ShowCharacterBoard();
    }

    private void ShowCharacterBoard()
    {
        gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = GameManager.instance.skin;
        gameObject.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().sizeDelta = GameManager.instance.skin.rect.size;
    }

    public void SkinInit()
    {
        data = DataManager.instance.Load();

        GameManager.instance.skin = skins[data.curSkinIndex];
    }
}
