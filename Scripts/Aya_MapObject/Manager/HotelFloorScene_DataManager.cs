using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HotelFloorScene_DataManager : MonoBehaviour
{
    private static HotelFloorScene_DataManager _instance;
    public static HotelFloorScene_DataManager Instance
    {
        get
        {
            _instance ??= FindObjectOfType<HotelFloorScene_DataManager>();
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        elevatorManager ??= GetComponent<ElevatorManager>();
        controller ??= GetComponent<HotelFloorScene_Controller>();

        LoadDataByInitialize();
    }

    [Title("Manager")]
    public ElevatorManager elevatorManager;
    public HotelFloorScene_Controller controller;

    [Title("PLAYER")]
    public Transform player;

    [Title("NPC")]
    [SerializeField] public Transform[] npc_Transforms;
    public Transform[] GetNPC_Transform()
    {
        return npc_Transforms;
    }
    [Title("MONSTER")]
    [SerializeField] private Transform[] monsters_Transfroms;
    public Transform[] GetMonster_Transform()
    {
        return monsters_Transfroms;
    }

    [ShowInInspector]
    public IInitializeByLoadedDataWraper[] initializeByLoadedDatas;

    public void LoadDataByInitialize()
    {
        GameDataSaveLoadManager.Instance.playerGameDataManager.playerTransform = player;

        GameDataSaveLoadManager.Instance.ApplayGameData();

        foreach (var data in initializeByLoadedDatas)
        {
            data.InitializeByData();
        }
    }
    #region ������ ��ư
    public void SetInitializeLoadData()
    {
        initializeByLoadedDatas = FIND(true);
    }

    private IInitializeByLoadedDataWraper[] FIND(bool includeInactive)
    {
        List<IInitializeByLoadedData> results = new List<IInitializeByLoadedData>();

        foreach (GameObject go in FindObjectsOfType<GameObject>(true))
        {
            if (includeInactive || go.activeInHierarchy)
            {
                // MonoBehaviour�κ��� ��� ������Ʈ�� ��������, �������̽��� ������ �͸� ���͸�
                var components = go.GetComponents<MonoBehaviour>().OfType<IInitializeByLoadedData>().ToArray();

                if (components.Length > 0)
                {
                    results.AddRange(components);
                }
            }
        }

        // �ߺ� ����
        results = results.Distinct().ToList();

        IInitializeByLoadedDataWraper[] ret = new IInitializeByLoadedDataWraper[results.Count];
        for (int i = 0; i < ret.Length; i++)
        {
            ret[i] = new IInitializeByLoadedDataWraper(results[i]);
        }

        return ret;
    }
    #endregion
}
