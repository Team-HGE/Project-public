﻿using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class Day_2_SceneInitializer : MonoBehaviour
{
    private void Awake()
    {
        EventManager.Instance.SetSwitch(GameSwitch.DoorUnlocked, false);
        GameManager.Instance.lightManager.elementsForFloors.Clear();
        FloorInitializer.Instance.SetInitializerNull();

        if (GameManager.Instance.lightManager.levers.Count > 0)
        {
            GameManager.Instance.lightManager.levers.Clear();
        }
    }

    private void Start()
    {
        EventManager.Instance.SetSwitch(GameSwitch.IsPlayingGame, true);

        // 점프스케어 세팅
        JumpScareSetting();

        // 시네머신 세팅
        GameManager.Instance.cinemachineManager.mainCamera = mainCamera;
        GameManager.Instance.cinemachineManager.playerVC = playerVC;


        GameManager.Instance.NowPlayCutScene = false;
        //GameManager.Instance.On_UI();
        //EventManager.Instance.InitializeSwitches(); // 지워야댐

        // 2일차 게임 스위치 변경
        EventManager.Instance.SetSwitch(GameSwitch.OneFloorEndEscape, true);
        EventManager.Instance.SetSwitch(GameSwitch.NowDay2, true);
        
        //다이얼로그 세팅
        DialogueManager.Instance.StartStory(5);
        Invoke("SaveSetting", 5);

        
        //퀘스트 세팅
        Quest.Instance.NextQuest(15);
        SystemMsg.Instance.UpdateMessage(10);
        PSYNpc.npcEvent += SetDay2PasswordHint;
    
    }
    void SaveSetting()
    {
        DialogueManager.Instance.set.ui.playEvent += Day2Save;
    }
    void Day2Save()
    {
        Debug.Log("세이브 성공");
        GameDataSaveLoadManager.Instance.SaveGameData(0);
        NPCPos.Instance.SetDayTimePos();

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            player.transform.position = playerPosition.localPosition;
            controller.enabled = true;
        }
       
        OffLobbyLight();
    }
    void OffLobbyLight()
    {
        GameManager.Instance.lightManager.OffLaversAllLight();
        GameManager.Instance.lightManager.OffListLight(GameManager.Instance.lightManager.GetLightsForFloor(Floor.Lobby));
        GameManager.Instance.lightManager.OffChangeMaterial(GameManager.Instance.lightManager.GetRenderersForFloor(Floor.Lobby));
    }
    void SetDay2PasswordHint()
    {
        if (PSYNpc.nPC_Name == NPC_Name.PSY)
        {
            DialogueManager.Instance.npcScript.playEvent += Day2PasswordHint1;
        }
    }
    void Day2PasswordHint1()
    {
        HotelFloorScene_DataManager.Instance.controller.ComputerPassward += 1;
    }

    void JumpScareSetting()
    {
        GameManager.Instance.jumpScareManager.OffBtn();
        GameManager.Instance.jumpScareManager.blackBG = blackBG;
        GameManager.Instance.jumpScareManager.flashLight = playerFlashLight;
        foreach (var mon in GameManager.Instance.jumpScareManager.monstersJumpScare)
        {
            switch (mon.jumpScareType)
            {
                case JumpScareType.EarTypeMonster:
                    mon.gameObject = earTypeMonster;
                    break;
                case JumpScareType.EyeTypeMonster:
                    mon.gameObject = eyeTypeMonster;
                    break;
                case JumpScareType.GroupTypeMonster:
                    mon.gameObject = groupTypeMonster;
                    break;
            }
        }
    }

    [Header("Cinemachine")]
    [SerializeField] private CinemachineVirtualCamera playerVC;
    [SerializeField] private CinemachineBrain mainCamera;
    [SerializeField] private GameObject blackBG;

    [Header("Light")]
    [SerializeField] private GameObject playerFlashLight;

    [Title("NPC")]
    [SerializeField] private NPC PSYNpc;

    [Title("Monster")]
    [SerializeField] private GameObject groupTypeMonster;
    [SerializeField] private GameObject eyeTypeMonster;
    [SerializeField] private GameObject earTypeMonster;

    [Title("Player")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerPosition;
}
