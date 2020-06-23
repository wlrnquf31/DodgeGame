using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataInfo
{
    [System.Serializable]
    public class GameData
    {
        public float speed = 5;

        public float shieldSpeed = 100;

        public float hpCureChance = 0.05f;

        public int cash = 0;

        public int addScore = 50;

        public int highScore = 0;

        public int[] keepBoost = new int[4] {0, 0, 0, 0 };

        public int curSkinIndex = 0;

        public bool joystickIsLeft = false;

        public float bgmVolume = 0.5f;

        public float effectVolume = 0.5f;
    }
}
