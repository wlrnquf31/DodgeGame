using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DataInfo;

public enum EUpgrade
{
    Speed,
    ShieldSpeed,
    HpCureChance,
}

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
        data = DataManager.instance.Load();

        upgradeInfo = new string[,]{
            { "플레이어의 이동속도를 높힐 수 있습니다.\n 현재 속도 : " + data.speed, "100" },
            { "실드의 움직이는 속도를 높힐 수 있습니다.\n 현재 속도 : " + data.shieldSpeed, "100" },
            { "실드로 가드시 되는 HP회복의 확률을 높힐 수 있습니다.\n 현재 확률 : " + data.hpCureChance * 100 + "%", "100" }
        };

        boostInfo = new string[,] {
            { "30초간 가드시 스코어 획득량 2배.\n 보유 : " + data.keepBoost[0], "100" },
            { "10초간 무적.\n 보유 : " + data.keepBoost[1], "100" },
            { "1회 피격 무시.\n 보유 : " + data.keepBoost[2], "100" },
            { "게임 끝날 시 획득한 코인 2배.\n 보유 : " + data.keepBoost[3], "100" }
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
                float compareValue;
                if (itemIndex.Equals((int)EUpgrade.Speed))
                {
                    compareValue = data.speed;
                    data.speed += 0.25f;
                    if(!compareValue.Equals(data.speed))
                    {
                        data.cash -= itemPrice;
                    }
                }
                else if (itemIndex.Equals((int)EUpgrade.ShieldSpeed))
                {
                    compareValue = data.shieldSpeed;
                    data.shieldSpeed += 5f;
                    if (!compareValue.Equals(data.shieldSpeed))
                    {
                        data.cash -= itemPrice;
                    }
                }
                else
                {
                    compareValue = data.hpCureChance;
                    data.hpCureChance += 0.02f;
                    if (!compareValue.Equals(data.hpCureChance))
                    {
                        data.cash -= itemPrice;
                    }
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

    //업그레이드 할려는 스텟이 Max를 찍었는지 체크하는 함수.
    private bool CheckMaxStatus(int itemIndex)
    {
        if (itemIndex.Equals(EUpgrade.Speed))
        {
            data.speed += 0.25f;
        }
        else if (itemIndex.Equals(EUpgrade.ShieldSpeed))
        {
            data.shieldSpeed += 5f;
        }
        else if (itemIndex.Equals(EUpgrade.HpCureChance))
        {
            data.hpCureChance += 0.02f;
        }
        return false;
    }
}
