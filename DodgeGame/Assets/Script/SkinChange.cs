using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkinChange : MonoBehaviour
{
    public Sprite[] Skins;

    private GameObject curSelectSkin;

    public void ClickSkin()
    {
        curSelectSkin = EventSystem.current.currentSelectedGameObject;
    }

    public void ChangeSkin()
    {
        Sprite changedSkin = curSelectSkin.GetComponent<Sprite>();
        GameManager.instance.CharacterChange(changedSkin);
    }
}
