using UnityEngine.UI;
using Util;

namespace DefaultNamespace
{
    public class SliderValues
    {
        private Slider _slider;

        // Ascending order!
        private int[] magicValues = { 10, 25, 50, 100 };

        public SliderValues(Slider slider)
        {
            _slider = slider;
        }
        
        
        private void CheckMagix(float value)
        {
            logger.log("Checking Value: "+value);
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
            if(_slider.minValue==min) return;

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