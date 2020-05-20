using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DataInfo;

public class StatusUpgrade : MonoBehaviour
{
    private GameData data = new GameData();

    public GameObject upgrade;
    private string[,] upgradeInfo = new string[3, 2] {
        { "플레이어의 이동속도를 높힐 수 있습니다.", "2500" },
        { "실드의 움직이는 속도를 높힐 수 있습니다.", "2500" },
        { "HP회복 아이템의 드롭 확률을 높힐 수 있습니다.", "2500" }
    };

    public GameObject boost;
    private string[,] boostInfo = new string[4,2] {
        { "30초간 가드시 스코어 획득량 2배", "1500" },
        { "10초간 무적", "2000" },
        { "1회 피격 무시", "1500" },
        { "게임 끝날 시 획득한 코인 2배", "2500" }
    };

    public GameObject description;

    private GameObject curSelectItem;


    //아이템을 클릭할 때 실행되는 함수.
    public void ClickItem()
    {
        curSelectItem = EventSystem.current.currentSelectedGameObject;

        ShowDescription();
    }

    //클릭한 아이템에 설명과 구매버튼을 보여주는 함수.
    private void ShowDescription()
    {
        string[] spItemName = curSelectItem.name.Split('-');//선택한 아이템을 split하여 아이템 이름과 인덱스를 분리

        if (spItemName[0].Equals("Upgrade"))
        {
            description.transform.GetChild(0).GetComponent<Text>().text = upgradeInfo[int.Parse(spItemName[1]), 1];
            description.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            description.transform.GetChild(0).GetComponent<Text>().text = boostInfo[int.Parse(spItemName[1]), 1];
            description.transform.GetChild(1).gameObject.SetActive(true);
        }
    }


    //Buy 버튼을 클릭시 아이템을 구매하는 함수.
    public void BuyItem()
    {
        string[] spItemName = curSelectItem.name.Split('-');

        string itemName = spItemName[0];//split한 아이템의 이름을 저장
        int itemIndex = int.Parse(spItemName[1]);//split한 아이템의 인덱스를 저장
        int itemPrice = int.Parse(boostInfo[itemIndex, 1]);//인덱스를 이용하여 itemInfo에서 가격을 가져옴

        data = DataManager.instance.Load();

        if (CheckHaveCash(itemPrice))
        {
            if (itemName.Equals("Upgrade"))
            {
                data.cash -= itemPrice;
                //업그레이드 해주는 코드
            }
            else
            {
                data.cash -= itemPrice;
                data.keepBoost[itemIndex]++;
            }
            DataManager.instance.Save(data);
        }
        else
        {
            //돈이 부족할 시 알림 띄우는 코드
        }
        
    }

    //아이템의 가격과 현재 보유한 돈을 비교하는 함수.
    private bool CheckHaveCash(int price)
    {
        if(price > DataManager.instance.Load().cash)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
