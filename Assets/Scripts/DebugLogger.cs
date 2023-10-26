using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class DebugLogger : MonoBehaviour
{
    
    public void print(string s)
    {
         logger.log(s);
    }
}
