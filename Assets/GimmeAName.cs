using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Util;

public class GimmeAName : MonoBehaviour
{
    [SerializeField] private ObjectType _objectType;

    public Transform AvailablesParent;

    public GameObject IconPrefab;

    private IconScript selected;

    private void OnEnable()
    {
        
        var tl = GameManager.INSTANCE.GetTopLevel(_objectType);
        IManageItem imanag;
        switch (_objectType.defenseType)
        {
            case DefenseType.ARMY:
                imanag = tl.ArmyManager;
                break;
            case DefenseType.AIRFORCE:
                imanag = tl.AirforceManager;
                break;
            case DefenseType.MARINE:
                imanag = tl.MarineManager;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        

        foreach (Item item in imanag.Items)
        {
            logger.log("NR: "+item.Index);
            if (item is MissionItem missionItem)
            {
                return;
            }
            
            GameObject go = Instantiate(IconPrefab, AvailablesParent);
            IconScript script = go.GetComponent<IconScript>();
            script.InitializePreview(this,item);

        }
    }

    public void Selected(IconScript child)
    {
        logger.log("Item Nr: "+child.Item.Index);
    }
}
