using System;
using System.Linq;
using UnityEngine.UI;
using Util;

namespace DefaultNamespace
{
    public class SliderValues
    {
        private Slider _slider;

        // Ascending order!
        private static int[] magicValues = { 10, 25, 100, 250 };

        public SliderValues(Slider slider)
        {
            _slider = slider;
        }

        public static int getRankForLevel(int level)
        {
            for (int i = 0; i < magicValues.Length; i++)
            {
                if (level < magicValues[i]) 
                    return i;
            }

            if (level <= 500) 
                return magicValues.Length - 1;

            throw new ArgumentException("Invalid Level!");

        }

        public static bool IsLevelUpNumber(int number)
        {
            return magicValues.Contains(number);
        }

        private void CheckMagix(float value)
        {
            int min = 0;
            int max = magicValues[0];
            for (int i = 0; i < magicValues.Length - 1; i++)
            {
                if (value >= magicValues[i])
                {
                    min = max;
                    max = magicValues[i + 1];
                }
            }
            if (_slider.minValue == min) return;

            _slider.minValue = min;
            _slider.maxValue = max;
        }

        public void setValue(float value)
        {
            CheckMagix(value);
            _slider.value = value;
        }
    }
}