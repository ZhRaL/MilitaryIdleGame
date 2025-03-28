﻿using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Manager;
using Provider;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Util;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager INSTANCE;

    [FormerlySerializedAs("curve")] public AnimationCurve itemCurve;
    public AnimationCurve missionItemCurve;
    private void Awake()
    {
        if (!INSTANCE) INSTANCE = this;
        new Calculator(itemCurve, missionItemCurve);
    }

    private bool isInitialized = false;
    public bool resetPlayerPrefsOnRestart = false;

    public bool ResetEnable
    {
        get => resetPlayerPrefsOnRestart;
        set
        {
            PlayerPrefs.SetInt("reset", value ? 1 : 0);
            resetPlayerPrefsOnRestart = value;
        }

    }

    #endregion

    private const string MISSIONSAFESTRING = "Mission_Levels";
    private const string KITCHENSAFESTRING = "Kitchen_Levels";
    private const string BATHSAFESTRING = "Bath_Levels";
    private const string SLEEPINGSAFESTRING = "Sleeping_Levels";
    public const string RECRUITMENTSAFESTRING = "Recruitment_Levels";

    public Player Player;
    [SerializeField]
    private AudioManager _audioManager; 
    public DataProvider DataProvider = new();

    #region currencies

    [SerializeField]
    private float _gold;
    private float _badges;

    public Text tx_Gold, tx_Badges;

    // Always SaveGame() at Changing Gold or Badge Amount
    public float Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            tx_Gold.text = _gold.ConvertBigNumber();
            if (isInitialized)
                SaveGame();
            OnMoneyChanged?.Invoke();
        }
    }

    public event Action OnMoneyChanged;
    public UnityAction OnSceneLoaded;

    public float Badges
    {
        get => _badges;
        set
        {
            _badges = value;
            tx_Badges.text = _badges.ConvertBigNumber();
            if (isInitialized) SaveGame();
        }
    }
    

    #endregion

    public MissionController MissionController;
    public KitchenController KitchenController;
    public BathController BathController;
    public SleepingController SleepingController;
    public SoldierController SoldierController;

    public OfflineCalculator OfflineCalculator;
    public StatisticsManager StatisticsManager;
    public InAppBuyManager InAppBuyManager;
    public TutorialManager TutorialManager;
    public SkillManager SkillManager;
    public float HourlyReward;

    private void Start()
    {
        OnSceneLoaded += TutorialManager.ShowTutorial;
        QualitySettings.vSyncCount = 2;
        Application.targetFrameRate = 30;
        OfflineCalculator = new OfflineCalculator(this);
        InAppBuyManager = new InAppBuyManager();
        LoadGame();
        StatisticsManager = new StatisticsManager(OfflineCalculator);
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
    }


    public void SaveGame()
    {
        PlayerPrefsHelper.SetFloat("Gold", Gold);
        PlayerPrefsHelper.SetFloat("Badges", Badges);
        PlayerPrefsHelper.SetInt("ResearchPoints", SkillManager.ResearchPoints);
        PlayerPrefsHelper.SetInt("TutorialIndex", TutorialManager.index);
        
        string b = JsonUtility.ToJson(KitchenController.Save<JsonItem>());
        PlayerPrefsHelper.SetString(KITCHENSAFESTRING, b);
        PlayerPrefsHelper.SetString(BATHSAFESTRING, JsonUtility.ToJson(BathController.Save<JsonItem>()));
        PlayerPrefsHelper.SetString(SLEEPINGSAFESTRING, JsonUtility.ToJson(SleepingController.Save<JsonItem>()));

        PlayerPrefsHelper.SetString(MISSIONSAFESTRING, JsonUtility.ToJson(MissionController.Save<MissionItemJO>()));

        string s = JsonUtility.ToJson(SoldierController.Save());
        PlayerPrefsHelper.SetString(RECRUITMENTSAFESTRING, s);
        Player.SoundEnabled = _audioManager.SoundEnabled;
        Player.MusicEnabled = _audioManager.MusicEnabled;
        PlayerPrefsHelper.SetString("PLAYER", JsonUtility.ToJson(Player));

        PlayerPrefs.Save();
    }

    private void LoadGame()
    {
        resetPlayerPrefsOnRestart = PlayerPrefsHelper.GetInt("reset", 0) == 1;
        if (resetPlayerPrefsOnRestart)
            ResetAllOwnPlayerPrefs();
        
        Gold = PlayerPrefsHelper.GetFloat("Gold", 5550);
        Badges = PlayerPrefsHelper.GetFloat("Badges", 100);
        SkillManager.ResearchPoints = PlayerPrefsHelper.GetInt("ResearchPoints");
        TutorialManager.index = PlayerPrefsHelper.GetInt("TutorialIndex", 0);
        string s = PlayerPrefsHelper.GetString(KITCHENSAFESTRING, "");
        KitchenController.Load(JsonUtility.FromJson<JsonController<JsonItem>>(s) ?? JsonController<JsonItem>.Default(new JsonItem()));
        BathController.Load(JsonUtility.FromJson<JsonController<JsonItem>>(PlayerPrefsHelper.GetString(BATHSAFESTRING, "")) ?? JsonController<JsonItem>.Default(new JsonItem()));
        SleepingController.Load(JsonUtility.FromJson<JsonController<JsonItem>>(PlayerPrefsHelper.GetString(SLEEPINGSAFESTRING, "")) ?? JsonController<JsonItem>.Default(new JsonItem()));
        MissionController.Load(JsonUtility.FromJson<JsonController<MissionItemJO>>(PlayerPrefsHelper.GetString(MISSIONSAFESTRING, "")) ?? JsonController<MissionItemJO>.Default(new MissionItemJO()));
        SoldierController.Load(JsonUtility.FromJson<JsonController<SoldierItemJO>>(PlayerPrefsHelper.GetString(RECRUITMENTSAFESTRING, "")) ?? JsonController<SoldierItemJO>.Default(new SoldierItemJO()));
        Player = JsonUtility.FromJson<Player>(PlayerPrefsHelper.GetString("PLAYER", "")) ?? new Player();
        _audioManager.SoundEnabled = Player.SoundEnabled;
        _audioManager.MusicEnabled = Player.MusicEnabled;
        isInitialized = true;
        SaveGame();
        OnSceneLoaded?.Invoke();
    }

    public void ResetAllOwnPlayerPrefs()
    {
        string[] sl = PlayerPrefsHelper.GetAllKeys().ToArray();
        foreach (var s in sl)
        {
            PlayerPrefs.DeleteKey(s);
        }
    }

    private void OnApplicationQuit()
    {
        OfflineCalculator.SafeTime();
        SaveGame();
    }

    public void AddOfflineTime(int hours)
    {
        OfflineCalculator.AddOfflineTime(hours * 3600);
    }
}