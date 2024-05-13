using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Util
{
    public static class ImageExtensions
    {
        public static void changeAlphaValue(this Image currImage,float newValue)
        {
            Color currColor = currImage.color;
            currColor.a = newValue;
            currImage.color = currColor;
        }
        public static void changeAlphaValue(this TMP_Text currImage,float newValue)
        {
            Color currColor = currImage.color;
            currColor.a = newValue;
            currImage.color = currColor;
        }
    }
}