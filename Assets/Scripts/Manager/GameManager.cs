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
    private const string MISSIONSAFESTRING = "Mission_Levels";
    private const string KITCHENSAFESTRING = "Kitchen_Levels";
    private const string BATHSAFESTRING = "Bath_Levels";
    private const string SLEEPINGSAFESTRING = "Sleeping_Levels";
    public const string RECRUITMENTSAFESTRING = "Recruitment_Levels";
    
    public DataProvider DataProvider = new();

    #region currencies

    private float _gold, _badges;

    public Text tx_Gold, tx_Badges;

    // Always SaveGame() at Changing Gold or Badge Amount
    public float Gold
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

    public float Badges
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
    public SoldierController SoldierController;

    private OfflineCalculator _offlineCalculator;

    private void Start()
    {
        _offlineCalculator = new OfflineCalculator();
        LoadGame();
    }

    public IController GetTopLevel(ObjectType type)
    {
        switch (type.objectType)
        {
            case GenericObjectType.KITCHEN: return KitchenController;
            case GenericObjectType.SLEEPING: return SleepingController;
            case GenericObjectType.BATH: return BathController;
            default: return MissionController;
        }

        throw new ArgumentException("Invalid ObjectType!");
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("Gold", Gold);
        PlayerPrefs.SetFloat("Badges", Badges);
        
       PlayerPrefs.SetString(MISSIONSAFESTRING,JsonUtility.ToJson(MissionController.Save()));
       PlayerPrefs.SetString(KITCHENSAFESTRING,JsonUtility.ToJson(KitchenController.Save()));
       PlayerPrefs.SetString(BATHSAFESTRING,JsonUtility.ToJson(BathController.Save()));
       PlayerPrefs.SetString(SLEEPINGSAFESTRING,JsonUtility.ToJson(SleepingController.Save()));
       PlayerPrefs.SetString(RECRUITMENTSAFESTRING,JsonUtility.ToJson(SoldierController.Save()));

        PlayerPrefs.Save();
    }

    private void LoadGame()
    {
        Gold = PlayerPrefs.GetFloat("Gold", 550);
        Badges = PlayerPrefs.GetFloat("Badges", 0);
        
        MissionController.Load(JsonUtility.FromJson<JsonController>(PlayerPrefs.GetString(MISSIONSAFESTRING,"")) ?? JsonController.Default());
        KitchenController.Load(JsonUtility.FromJson<JsonController>(PlayerPrefs.GetString(KITCHENSAFESTRING,"")) ?? JsonController.Default());
        BathController.Load(JsonUtility.FromJson<JsonController>(PlayerPrefs.GetString(BATHSAFESTRING,""))?? JsonController.Default());
        SleepingController.Load(JsonUtility.FromJson<JsonController>(PlayerPrefs.GetString(SLEEPINGSAFESTRING,"")) ?? JsonController.Default());
        SoldierController.Load(JsonUtility.FromJson<JsonController>(PlayerPrefs.GetString(RECRUITMENTSAFESTRING,""))?? JsonController.Default());

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
