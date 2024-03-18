using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecruitTemplate : MonoBehaviour
{
    private TMP_Text name;

    public Soldier Soldier { get; }

    public RecruitTemplate(Soldier soldier)
    {
        Soldier = soldier;
        name.text = Soldier.name;
        // TODO - add correct Portrait
    }
    
}
