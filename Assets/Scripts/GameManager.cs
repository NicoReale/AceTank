using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ItemManager itemManager;
    public Dictionary<int, int> itemDataSaved = new Dictionary<int, int>();
    public ItemManager p_ItemManager { get { return itemManager; }}


    public SaveBehaviour saveBehaviour;
    public SaveData saveData = new SaveData();

    public int Currency;
    public int totalCurrency;
    public int shots;
    public int levelsFlags;
    //public List<float> playerStats = new List<float>(2);

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<GameManager>();
        }
        else Destroy(gameObject);

        if (saveBehaviour == null)
            saveBehaviour = new SaveBehaviour();
        saveBehaviour.checkForData();
        saveData = saveBehaviour.LoadGame();
    }

    private void Start()
    {
        itemDataSaved = saveData.ConvertItemsToDic();
        LoadStats();
        itemManager = new ItemManager(itemDataSaved);
        if(MenuUIManager.instance != null)
            MenuUIManager.instance.CheckLevels();
    }

    public void LoadStats()
    {
        //playerStats = saveData.playerModifiers;
        Currency = saveData.Currency;
        totalCurrency = saveData.totalCurr;
        shots = saveData.shots;
        levelsFlags = saveData.levelsUnlocked;
    }

    public void SaveGame()
    {
        saveData.Currency = Currency;
        saveData.totalCurr = totalCurrency;
        saveData.shots = shots;
        //saveData.playerModifiers = playerStats;
        saveData.levelsUnlocked = levelsFlags;
        saveData.ConvertItemDicToList(p_ItemManager.Items);
        saveBehaviour.SaveGame(saveData);
    }

    public void AddShot()
    {
        shots++;
    }

    public void AddCurr(int amount)
    {
        Currency += amount;
        totalCurrency += amount;

    }

    public void AddLevelFlag(int flag)
    {
        levelsFlags = flag;
    }

    public void EraseData()
    {
        saveData.Currency = 0;
        saveData.totalCurr = 0;
        saveData.shots = 0;
        saveData.levelsUnlocked = 0;
        levelsFlags = 0;
        PlayerPrefs.SetFloat("SpeedMult", 0);
        PlayerPrefs.SetFloat("DamageMult", 0);
        Currency = 0;
        totalCurrency = 0;
        shots = 0;
        saveData.ConvertItemDicToList(p_ItemManager.ClearItems());
        saveBehaviour.SaveGame(saveData);
    }

    public void clearStats()
    {
        PlayerPrefs.SetFloat("SpeedMult", 0);
        PlayerPrefs.SetFloat("DamageMult", 0);
        SaveGame();
    }
}
