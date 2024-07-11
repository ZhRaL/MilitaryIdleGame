using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Manager;
using Provider;
using UnityEngine;
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

    public OfflineCalculator OfflineCalculator;

    private void Start()
    {
        QualitySettings.vSyncCount = 2;
        Application.targetFrameRate = 30;
        OfflineCalculator = new OfflineCalculator();
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
    }


    public void SaveGame()
    {
        PlayerPrefsHelper.SetFloat("Gold", Gold);
        PlayerPrefs.SetFloat("Badges", Badges);

        string b = JsonUtility.ToJson(KitchenController.Save<JsonItem>());
        PlayerPrefs.SetString(KITCHENSAFESTRING, b);
        PlayerPrefs.SetString(BATHSAFESTRING, JsonUtility.ToJson(BathController.Save<JsonItem>()));
        PlayerPrefs.SetString(SLEEPINGSAFESTRING, JsonUtility.ToJson(SleepingController.Save<JsonItem>()));

        PlayerPrefs.SetString(MISSIONSAFESTRING, JsonUtility.ToJson(MissionController.Save<MissionItemJO>()));

        string s = JsonUtility.ToJson(SoldierController.Save());
        PlayerPrefs.SetString(RECRUITMENTSAFESTRING, s);
        PlayerPrefsHelper.SetString("PLAYER", JsonUtility.ToJson(Player));

        PlayerPrefs.Save();
    }

    private void LoadGame()
    {
        resetPlayerPrefsOnRestart = PlayerPrefs.GetInt("reset", 0) == 1;
        if (resetPlayerPrefsOnRestart)
            ResetAllOwnPlayerPrefs();


        Gold = PlayerPrefsHelper.GetFloat("Gold", 5550);
        Badges = PlayerPrefs.GetFloat("Badges", 100);
        string s = PlayerPrefs.GetString(KITCHENSAFESTRING, "");
        KitchenController.Load(JsonUtility.FromJson<JsonController<JsonItem>>(s) ?? JsonController<JsonItem>.Default(new JsonItem()));
        BathController.Load(JsonUtility.FromJson<JsonController<JsonItem>>(PlayerPrefs.GetString(BATHSAFESTRING, "")) ?? JsonController<JsonItem>.Default(new JsonItem()));
        SleepingController.Load(JsonUtility.FromJson<JsonController<JsonItem>>(PlayerPrefs.GetString(SLEEPINGSAFESTRING, "")) ?? JsonController<JsonItem>.Default(new JsonItem()));

        MissionController.Load(JsonUtility.FromJson<JsonController<MissionItemJO>>(PlayerPrefs.GetString(MISSIONSAFESTRING, "")) ?? JsonController<MissionItemJO>.Default(new MissionItemJO()));

        SoldierController.Load(JsonUtility.FromJson<JsonController<SoldierItemJO>>(PlayerPrefs.GetString(RECRUITMENTSAFESTRING, "")) ?? JsonController<SoldierItemJO>.Default(new SoldierItemJO()));
        Player = JsonUtility.FromJson<Player>(PlayerPrefsHelper.GetString("PLAYER", "")) ?? new Player();
        isInitialized = true;
        SaveGame();
    }

    public void ResetAllOwnPlayerPrefs()
    {
        string[] sl = { BATHSAFESTRING, SLEEPINGSAFESTRING, KITCHENSAFESTRING, MISSIONSAFESTRING, RECRUITMENTSAFESTRING, "Gold", "Badges", "reset", "saveString" };
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