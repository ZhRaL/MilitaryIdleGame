using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Util
{
    public class Calculator
    {
        private float rewardMultiplier = 1.2f;

        private Dictionary<string, int> startValues = new Dictionary<string, int>()
        {
            { "JR", 10 },
            { "AR", 10 },
            { "SR", 10 },

            { "JD", 10 },
            { "AD", 10 },
            { "SD", 10 },

            { "KD", 10 },
            { "BD", 10 },
            { "SLD", 10 },
        };

        public static Calculator INSTANCE;

        public Calculator()
        {
            INSTANCE ??= this;
        }

        private int CalculateReward(float start, int level)
        {
            return Mathf.RoundToInt(start * Mathf.Pow(rewardMultiplier, level - 1));
        }

        public int CalculateReward(string type, int level)
        {
            if (startValues.TryGetValue(type, out _))
            {
                return CalculateReward(startValues[type], level);
            }

            throw new ArgumentException("Key not found! Was: " + type);
        }
    }
}