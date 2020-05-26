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
    private string[,] upgradeInfo;

    public GameObject boost;
    private string[,] boostInfo;

    private void Start()
    {
        InitInfoData();
    }

    public GameObject description;

    private GameObject curSelectItem;

    public void InitInfoData()
    {
        upgradeInfo = new string[,]{
            { "플레이어의 이동속도를 높힐 수 있습니다.\n 현재 속도 : " + DataManager.instance.Load().speed, "2500" },
            { "실드의 움직이는 속도를 높힐 수 있습니다.\n 현재 속도 : " + DataManager.instance.Load().shieldSpeed, "2500" },
            { "HP회복 아이템의 드롭 확률을 높힐 수 있습니다.\n 현재 확률 : " + DataManager.instance.Load().hpCureDrop * 100 + "%", "2500" }
        };

        boostInfo = new string[,] {
            { "30초간 가드시 스코어 획득량 2배.\n 보유 : " + DataManager.instance.Load().keepBoost[0], "1500" },
            { "10초간 무적.\n 보유 : " + DataManager.instance.Load().keepBoost[1], "2000" },
            { "1회 피격 무시.\n 보유 : " + DataManager.instance.Load().keepBoost[2], "1500" },
            { "게임 끝날 시 획득한 코인 2배.\n 보유 : " + DataManager.instance.Load().keepBoost[3], "2500" }
        };
    }

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
            description.transform.GetChild(0).GetComponent<Text>().text = upgradeInfo[int.Parse(spItemName[1]), 0] + "\n 가격 : " + upgradeInfo[int.Parse(spItemName[1]), 1];
            description.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            description.transform.GetChild(0).GetComponent<Text>().text = boostInfo[int.Parse(spItemName[1]), 0] + "\n 가격 : " + boostInfo[int.Parse(spItemName[1]), 1];
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
                if(itemIndex.Equals(0))
                {
                    data.speed += 0.25f;
                }
                else if(itemIndex.Equals(1))
                {
                    data.shieldSpeed += 5f;
                }
                else
                {
                    data.hpCureDrop += 0.02f;
                }
            }
            else
            {
                data.cash -= itemPrice;
                data.keepBoost[itemIndex]++;
            }
            DataManager.instance.Save(data);
            UiManager.instance.SetCashText();
            InitInfoData();
            ShowDescription();
        }
        else
        {
            //돈이 부족할 시 알림 띄우는 코드
        }
        
    }

    //아이템의 가격과 현재 보유한 돈을 비교하는 함수.
    private bool CheckHaveCash(int price)
    {
        if (price > DataManager.instance.Load().cash)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
