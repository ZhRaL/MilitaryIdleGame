using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Util
{
    public class Calculator
    {
        private float rewardMultiplier = 1.2f;

        private Dictionary<string, int> startValues = new()
        {
            /*
             * T Tank
             * S Ship
             * J Jet
             */
            // Rewards
            { "JR", 10 },
            { "TR", 10 },
            { "SR", 10 },
            // Costs
            { "JRC", 10 },
            { "TRC", 10 },
            { "SRC", 10 },

            // Duration
            { "JD", 10 },
            { "TD", 10 },
            { "SD", 10 },
            // Costs
            { "JDC", 10 },
            { "TDC", 10 },
            { "SDC", 10 },

            
            { "KD", 10 },
            { "BD", 10 },
            { "SLD", 10 },
            
            // Costs
            { "KAC", 10 },
            { "KSC", 10 },
            
            { "BAC", 10 },
            { "BSC", 10 },

            { "SLAC", 10 },
            { "SLSC", 10 },
            
            
            


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