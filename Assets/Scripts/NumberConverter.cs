using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using UnityEngine;

public static class NumberConverter
{
    public static string[] abbrev = new string[]
    {
        "K","M","B","T","q","Q","s","S",
        "aa","ab","ac","ad","ae","af","ag","ah","ai","aj",
        "ak","al","am","an","ao","ap","aq","ar","aw","at",
        "au","av","aw","ax", "ay","az",
        "ba","bb","bc","bd","be","bf","bg","bh","bi","bj",
        "bk","bl","bm","bn","bo","bp","bq","br","bs","bt",
        "bu","bv","bw","bx","by","bz"
    };

    public static string ConvIntToString(this double bigInt)
    {
        if (bigInt < 1000) return bigInt.ToString();
        string toret = "";
        string s = bigInt.ToString();
        if (s.Length < 4) return s;
        int length = s.Length;
        int ind = length / 3;
        int ind2 = length % 3;
        if (ind2 == 0)
        {
            ind--;
            toret = "" + s[0] + s[1] +  s[2]  + abbrev[--ind];
        } else if(ind2 ==1)
        {
            toret = "" + s[0] + "," + s[1] + s[2] + abbrev[--ind];
        }
        else if(ind2 ==2)
        {
            toret = ""+ s[0] + s[1]+","+s[2]+abbrev[--ind];
        }
        return toret;
    }
    public static string ArrayToPrint<T>(this T[] array)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            if (i != 0) sb.Append(",");
            sb.Append(array[i]);
        }

        return sb.ToString();
    }
    public static string ConvIntToString(this float bigInt)
    {
        if (bigInt < 1000) return bigInt.ToString();
        string toret = "";
        string s = bigInt.ToString();
        if (s.Length < 4) return s;
        int length = s.Length;
        int ind = length / 3;
        int ind2 = length % 3;
        if (ind2 == 0)
        {
            ind--;
            toret = "" + s[0] + s[1] + s[2] + abbrev[--ind];
        }
        else if (ind2 == 1)
        {
            toret = "" + s[0] + "," + s[1] + s[2] + abbrev[--ind];
        }
        else if (ind2 == 2)
        {
            toret = "" + s[0] + s[1] + "," + s[2] + abbrev[--ind];
        }
        return toret;
    }
}
