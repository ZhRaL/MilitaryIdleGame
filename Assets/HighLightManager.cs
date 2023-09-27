using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class HighLightManager
{
    private static Image current_img;
    private static Color savedColor;

    public static void highlight(Image img)
    {
        if(current_img!=null)
        current_img.color = savedColor;
        
        savedColor = img.color;
        img.color = Color.white;
        current_img = img;
    }
}
