using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DataInfo
{
    [System.Serializable]
    public class GameData
    {
        private readonly float MAX_SPEED = 10;
        private readonly float MAX_SHIELDSPEED = 150;
        private readonly float MAX_HPCURECHANCE = 0.3f;

        private float _speed = 5;
        public float speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
                if (_speed > MAX_SPEED)
                {
                    _speed = MAX_SPEED;
                }
            }
        }

        private float _shieldSpeed = 100;
        public float shieldSpeed
        {
            get
            {
                return _shieldSpeed;
            }
            set
            {
                _shieldSpeed = value;
                if (_shieldSpeed > MAX_SHIELDSPEED)
                {
                    _shieldSpeed = MAX_SHIELDSPEED;
                }
            }
        }

        private float _hpCureChance = 0.04f;
        public float hpCureChance
        {
            get
            {
                return _hpCureChance;
            }
            set
            {
                _hpCureChance = value;
                if (_hpCureChance > MAX_HPCURECHANCE)
                {
                    _hpCureChance = MAX_HPCURECHANCE;
                }
            }
        }

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
