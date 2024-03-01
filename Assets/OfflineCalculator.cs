using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class OfflineCalculator
{
    private float percentage = .6f;
    
    private DateTime  savedTime;
    
    public float validOfflineTime = 60 * 60;
    
    private const string saveString = "OFFLINE_CALC";

    public OfflineCalculator()
    {
        string savedStartTime = PlayerPrefs.GetString(saveString, string.Empty);
        if (!string.IsNullOrEmpty(savedStartTime))
        {
            savedTime = DateTime.Parse(savedStartTime);
        }
    }
    
    public void safeTime()
    {
        logger.log("I saved: "+DateTime.Now);
        PlayerPrefs.SetString(saveString, DateTime.Now.ToString());
        PlayerPrefs.Save(); // PlayerPrefs speichern (wichtig!)
    }

    public void calculateReward()
    {
        TimeSpan elapsedTime = DateTime.Now - savedTime;

        int diff  = (int) elapsedTime.TotalSeconds;
        
        // Caluclate real reward

    }
}