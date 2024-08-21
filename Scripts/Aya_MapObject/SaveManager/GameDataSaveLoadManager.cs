using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO; //������ ����, ����, ����
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;


[Serializable]
public class GameBaseInfo
{
    public SceneEnum sceneEnum;  // ���� ����� �� �̸�
    public string dateTime;   // ����� ��¥�� �ð�

    public GameBaseInfo()
    {
        sceneEnum = SceneEnum.Hotel_Day1;
    }
}

[Serializable]
public class GameDataCore
{
    public GameDataCore()
    {
        sceneIndividualData = new SceneIndividualData();
        gameBaseInfo = new GameBaseInfo();
        playerGameData = new PlayerGameData();
        questGameData = new QuestGameData();
    }
    public SceneIndividualData sceneIndividualData;
    public GameBaseInfo gameBaseInfo;
    public PlayerGameData playerGameData;
    public QuestGameData questGameData;
}

[Serializable]
public class SceneIndividualData
{
    // �� ������ ����
    public string sceneName;
    public SceneGameData sceneGameData;
    public List<NPCGameData> npcGameData;
    public List<MonsterGameData> monsterGameData;

    public SceneIndividualData()
    {
        sceneGameData = new SceneGameData();
        npcGameData = new List<NPCGameData>();
        monsterGameData = new List<MonsterGameData>();
    }
}

public class GameDataSaveLoadManager : SingletonManager<GameDataSaveLoadManager>
{

    [TabGroup("Data", "DataManager", SdfIconType.GearFill, TextColor = "orange")]

    [TabGroup("Data", "DataManager")][HideInInspector] public PlayerGameDataManager playerGameDataManager;
    [TabGroup("Data", "DataManager")][HideInInspector] public NPCGameDataManager npcGameDataManager;
    [TabGroup("Data", "DataManager")][HideInInspector] public MonsterGameDataManager monsterGameDataManager;
    [TabGroup("Data", "DataManager")][HideInInspector] public SceneGameDataManager sceneGameDataManager;
    [TabGroup("Data", "DataManager")][HideInInspector] public QuestGameDataManager questGameDataManager;
    [TabGroup("Data", "DataManager")][HideInInspector] public GameSettings gameSettings;

    [Title("SavingCanvas")]
    public GameObject savingCanvas;

    protected override void Awake()
    {
        base.Awake();

        if (FindObjectsOfType<GameDataSaveLoadManager>().Length != 1)
        {
            Destroy(gameObject);
            return;
        }

        playerGameDataManager = GetComponent<PlayerGameDataManager>();
        npcGameDataManager = GetComponent<NPCGameDataManager>();
        monsterGameDataManager = GetComponent<MonsterGameDataManager>();
        sceneGameDataManager = GetComponent<SceneGameDataManager>();
        questGameDataManager = GetComponent<QuestGameDataManager>();

        gameSettings = GetComponent<GameSettings>();
    }

    private static bool isSaveLocked = false;
    /// <summary>
    /// Ư�� �������� ���̺� ���� [��]
    /// </summary>
    public static bool IsSaveLocked
    {
        get
        {
            return isSaveLocked;
        }
    }

    private static int _currentSlotId = -1;
    /// <summary>
    /// ���� ������ ������ ID [��]
    /// </summary>
    public static int CurrentSlotId
    {
        get
        {
            return _currentSlotId;
        }
    }

    public static string returnFilePath(int index)
    {
        return $"{Application.persistentDataPath}/GameData{index}.json";
    }
    

    /// <summary>
    /// �ε��� ��ȣ�� �ش��ϴ� ���̺� ������ �ִ��� [��]
    /// </summary>
    public bool FileExists(int index)
    {
        return File.Exists(returnFilePath(index));
    }

    private GameDataCore gameDataCore;
    public void CreatedNewData()
    {
        gameDataCore = new GameDataCore();
    }

    public SceneEnum ReturnSceneEnum()
    {
        return gameDataCore.gameBaseInfo.sceneEnum;
    }
    public void LoadGameData(int slot_Id)
    {
        if (FileExists(slot_Id) == false)
        {
            CreatedNewData();
            return;
        } 

        // ���� ���� ID ��� [��]
        _currentSlotId = slot_Id;

        // ���� �б�
        string fromJson = File.ReadAllText(returnFilePath(slot_Id));
        gameDataCore = JsonConvert.DeserializeObject<GameDataCore>(fromJson);
    }
    public void SaveGameData(int slot_Id)
    {
        StartCoroutine(SavingCanvas());
        // UniverSal Data
        // ���� �� ���� ������� ����� ������ [��]
        {
            gameDataCore.gameBaseInfo.dateTime = DateTime.Now.ToString();
            gameDataCore.gameBaseInfo.sceneEnum = (SceneEnum)SceneManager.GetActiveScene().buildIndex;

            gameDataCore.playerGameData = playerGameDataManager.GetData();
            // ����Ʈ ���� ������ ���� �ʿ�
            gameDataCore.questGameData = questGameDataManager.GetData();
        }

        // Scene Individual Data
        // ���� ���� ���� ������ ������ �ʿ��� ������
        {
            // ���� ���� �����͸� �������� [��]

            gameDataCore.sceneIndividualData.sceneName = SceneManager.GetActiveScene().name;
            gameDataCore.sceneIndividualData.sceneGameData = sceneGameDataManager.GetData();
            gameDataCore.sceneIndividualData.npcGameData = npcGameDataManager.GetData();
            gameDataCore.sceneIndividualData.monsterGameData = monsterGameDataManager.GetData();
        }

        //���� ���� [��]
        string toJsonData = JsonConvert.SerializeObject(gameDataCore);
        File.WriteAllText(returnFilePath(slot_Id), toJsonData);
    }
    public void RemoveGameData(int slotId) //[��]
    {
        if (FileExists(slotId) == false)
            return;

        File.Delete(returnFilePath(slotId));
    }

    public void TryLockSave(bool isEnable) //[��]
{
        isSaveLocked = isEnable;
    }

    public void ApplayGameData()
    {
        sceneGameDataManager.ApplyGameData(gameDataCore.sceneIndividualData.sceneGameData);

        questGameDataManager.ApplyGameData(gameDataCore.questGameData);
        npcGameDataManager.ApplyGameData(gameDataCore.sceneIndividualData.npcGameData);
        monsterGameDataManager.ApplyGameData(gameDataCore.sceneIndividualData.monsterGameData);

        playerGameDataManager.ApplyGameData(gameDataCore.playerGameData);
    }

    IEnumerator SavingCanvas()
    {
        savingCanvas.SetActive(true);
        yield return new WaitForSeconds(3);
        savingCanvas.SetActive(false);
    }
}

