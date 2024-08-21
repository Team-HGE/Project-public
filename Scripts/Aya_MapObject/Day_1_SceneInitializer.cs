using Cinemachine;
using UnityEngine;

public class Day_1_SceneInitializer : MonoBehaviour
{
    [SerializeField] private GameObject blackBG;
    [SerializeField] private GameObject playerLight;

    [SerializeField] private GameObject earTypeMonster;
    [SerializeField] private GameObject eyeTypeMonster;
    [SerializeField] private GameObject groupTypeMonster;

    [SerializeField] private CinemachineBrain mainCamera;
    [SerializeField] private CinemachineVirtualCamera playerVC;

    [SerializeField] private NPC[] npcs;
   
    private void Awake()
    {
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
        // �÷��̾� �˹��� ����

        GameManager.Instance.On_UI();
        // ����ġ ����
        GameManager.Instance.dayNightUI.UpdateDayNightUI(EventManager.Instance.GetSwitch(GameSwitch.IsDaytime));
        EventManager.Instance.SetSwitch(GameSwitch.IsDaytime, true);
        EventManager.Instance.SetSwitch(GameSwitch.isCentralPowerActive, true);

        // �ó׸ӽ� ����
        GameManager.Instance.cinemachineManager.playerVC = playerVC;
        GameManager.Instance.cinemachineManager.mainCamera = mainCamera;

        // �������ɾ� ����
        JumpScareMonsterSetting();


        // ���̾�α� ����
        DialogueManager.Instance.StartStory(1);

        foreach (var n in npcs)
        {
            n.npcEvent += BedInteracted;
        }
    }

    void JumpScareMonsterSetting()
    {
        GameManager.Instance.jumpScareManager.blackBG = blackBG;
        GameManager.Instance.jumpScareManager.flashLight = playerLight;
        foreach (var mon in GameManager.Instance.jumpScareManager.monstersJumpScare)
        {
            switch(mon.jumpScareType)
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

    void BedInteracted()
    {
        bool canSleep = DialogueManager.Instance.npcData.AllInteracted();
        Debug.Log(canSleep);
        if (canSleep)
        {
            SystemMsg.Instance.UpdateMessage(5);
            Quest.Instance.NextQuest(2);
            EventManager.Instance.SetSwitch(GameSwitch.GoToBed, true);
        }
    }
}
