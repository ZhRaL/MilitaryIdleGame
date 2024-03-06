using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager INSTANCE;

    private void Awake()
    {
        if (!INSTANCE) INSTANCE = this;
        new Calculator();
    }

    private bool isInitialized = false;

    #endregion
    private const string MARINESAFESTRING = "Marine_Levels";
    private const string ARMYAFESTRING = "Army_Levels";
    private const string AIRFORCEAFESTRING = "Airforce_Levels";
    private const string KITCHENSAFESTRING = "Kitchen_Levels";
    private const string BATHSAFESTRING = "Bath_Levels";
    private const string SLEEPINGSAFESTRING = "Sleeping_Levels";
    public const string RECRUITMENTSAFESTRING = "Recruitment_Levels";

    #region currencies

    private float _gold, _badges;

    public Text tx_Gold, tx_Badges;

    // Always SaveGame() at Changing Gold or Badge Amount
    public float gold
    {
        get => _gold;
        set
        {
            _gold = value;
            tx_Gold.text = "" + _gold;
            if (isInitialized) 
                SaveGame();
            OnMoneyChanged?.Invoke();
        }
    }

    public event Action OnMoneyChanged;

    public float badges
    {
        get => _badges;
        set
        {
            _badges = value;
            tx_Badges.text = "" + _badges;
            if (isInitialized) SaveGame();
        }
    }

    #endregion

    public MissionController MissionController;
    public KitchenController KitchenController;
    public BathController BathController;
    public SleepingController SleepingController;

    public GameObject[] Controllers;

    private OfflineCalculator _offlineCalculator;

    private void Start()
    {
        _offlineCalculator = new OfflineCalculator();
        initializeController();
        LoadGame();
    }

    private void initializeController()
    {
        // TODO
      //  MarineContr = Controllers[0].GetComponent<MarineController>();
      //  ArmyContr = Controllers[1].GetComponent<ArmyController>();
      //  AirforceContr = Controllers[2].GetComponent<AirforceController>();
      //  KitchenController = Controllers[3].GetComponent<KitchenController>();
      //  BathController = Controllers[4].GetComponent<BathController>();
      //  SleepingController = Controllers[5].GetComponent<SleepingController>();
      //  RecruitmentController = Controllers[6].GetComponent<RecruitmentController>();
    }

    public IController GetTopLevel(ObjectType type)
    {
        switch (type)
        {
            case ObjectType.CHAIR: return KitchenController;
       //     case ObjectType.BED: return SleepingController;
       //     case ObjectType.TOILET: return BathController;
            
            case ObjectType.JET_AMOUNT: 
            case ObjectType.JET_TIME:
            case ObjectType.SHIP_AMOUNT:
            case ObjectType.SHIP_TIME:
            case ObjectType.TANK_AMOUNT:
            case ObjectType.TANK_TIME:
        //        return MissionController;
                break;
        }

        throw new ArgumentException("Invalid ObjectType!");
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("Gold", gold);
        PlayerPrefs.SetFloat("Badges", badges);

        // TODO
      //   PlayerPrefs.SetString(MARINESAFESTRING, JsonHelper.ToJson(MarineContr.getState()));
      //   PlayerPrefs.SetString(AIRFORCEAFESTRING, JsonHelper.ToJson(AirforceContr.getState()));
      //   PlayerPrefs.SetString(ARMYAFESTRING,JsonHelper.ToJson(ArmyContr.getState()));
        PlayerPrefs.SetString(KITCHENSAFESTRING,JsonHelper.ToJson(KitchenController.getState()));
        PlayerPrefs.SetString(BATHSAFESTRING,JsonHelper.ToJson(BathController.getState()));
        PlayerPrefs.SetString(SLEEPINGSAFESTRING,JsonHelper.ToJson(SleepingController.getState()));
        PlayerPrefs.Save();
    }

    private void LoadGame()
    {
        gold = PlayerPrefs.GetFloat("Gold", 550);
        badges = PlayerPrefs.GetFloat("Badges", 0);
        
       //  MarineContr.loadState(JsonHelper.FromJson<int>(PlayerPrefs.GetString(MARINESAFESTRING, " {\"Items\":[1,1,0,0,0,0]}")));
       //  AirforceContr.loadState(JsonHelper.FromJson<int>(PlayerPrefs.GetString(AIRFORCEAFESTRING, " {\"Items\":[1,1,0,0,0,0]}")));
       //  ArmyContr.loadState(JsonHelper.FromJson<int>(PlayerPrefs.GetString(ARMYAFESTRING," {\"Items\":[1,1,0,0,0,0]}")));
        
        KitchenController.loadState(JsonHelper.FromJson<int>(PlayerPrefs.GetString(KITCHENSAFESTRING," {\"Items\":[1,0,0,0,1,0,0,0,1,0,0,0]}")));
        BathController.loadState(JsonHelper.FromJson<int>(PlayerPrefs.GetString(BATHSAFESTRING," {\"Items\":[1,1,1,1,1,1]}")));
        SleepingController.loadState(JsonHelper.FromJson<int>(PlayerPrefs.GetString(SLEEPINGSAFESTRING," {\"Items\":[1,0,0,0,1,0,0,0,1,0,0,0]}")));
        isInitialized = true;
        _offlineCalculator.calculateReward();
        SaveGame();
    }

    public void ResetPlayerprefs()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OnApplicationQuit()
    {
        logger.log("Closing...");
        _offlineCalculator.safeTime();
        SaveGame();
    }
}