using DiceNook.View;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonManager<GameManager>
{
    [TitleGroup("GameManager", "Singleton", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
    [InfoBox("�ν����� �ٹ̴� ��� �ʿ��Ѱ� �����ø� �������ֽø� �˴ϴ�.")]
    [SerializeField] GameObject ����;

    [TabGroup("Tab", "PlayerCanvas", SdfIconType.Image, TextColor = "lightgreen")]
    [TabGroup("Tab", "PlayerCanvas")] public GameObject playerInteractionCanvas;
    [TabGroup("Tab", "PlayerCanvas")] public TextMeshProUGUI interactableText;
    [TabGroup("Tab", "PlayerCanvas")] public Image interactionImage;
    [TabGroup("Tab", "PlayerCanvas")] public Image fillAmountImage;
    [TabGroup("Tab", "PlayerCanvas")] public PlayerInteractable player;
    [TabGroup("Tab", "PlayerCanvas")] public GameObject circleUI;
    [TabGroup("Tab", "PlayerCanvas")] public GameObject timeUI;
    [TabGroup("Tab", "PlayerCanvas")] public GameObject crossHairCanvas;


    [TabGroup("Tab", "Manager", SdfIconType.GearFill, TextColor = "orange")]
    [TabGroup("Tab", "Manager")] public FadeManager fadeManager;
    [TabGroup("Tab", "Manager")] public CinemachineManager cinemachineManager;
    [TabGroup("Tab", "Manager")] public LightManager lightManager;
    [TabGroup("Tab", "Manager")] public JumpScareManager jumpScareManager;


    [TabGroup("Tab", "GimmickCanvas", SdfIconType.ImageAlt, TextColor = "red")]
    [TabGroup("Tab", "GimmickCanvas")] public GameObject keyPadGimmickCanvas;
    [TabGroup("Tab", "GimmickCanvas")] public KeyPadGimmick keyPadGimmick;

    [TitleGroup("Time")]
    public DayNightUI dayNightUI;

    [TitleGroup("Player")]
    public bool playerDie { get; set; }
    [ShowInInspector] public PlayerStateMachine PlayerStateMachine { get; set; }

    [TitleGroup("CutScene")]
    public bool NowPlayCutScene { get; set; }


    protected override void Awake()
    {
        base.Awake();
        if (fadeManager == null) fadeManager = GetComponent<FadeManager>();

        if (dayNightUI == null) dayNightUI = GetComponent<DayNightUI>();

        if (cinemachineManager == null) cinemachineManager = GetComponent<CinemachineManager>();

        if (lightManager == null) lightManager = GetComponent<LightManager>();

        if (jumpScareManager == null) jumpScareManager = GetComponent<JumpScareManager>();
    }
    public void Init(Player _player)
    {
        player = _player.GetComponent<PlayerInteractable>();
        player.fillAmountImage = fillAmountImage;
        player.interactableText = interactableText;
        player.playerInteraction = playerInteractionCanvas;
        player.interactionImage = interactionImage;

        this.PlayerStateMachine = _player.GetStateMachine();
        playerDie = false;
    }


    public void Off_UI()
    {
        playerInteractionCanvas.SetActive(false);
        circleUI.SetActive(false);
        timeUI.SetActive(false);
        crossHairCanvas.SetActive(false);

        if(DialogueManager.Instance.quest.questCanvas != null)
        {
            DialogueManager.Instance.quest.questCanvas.SetActive(false);
        }
    }

    public void On_UI()
    {
        playerInteractionCanvas.SetActive(true);
        circleUI.SetActive(true);
        timeUI.SetActive(true);
        crossHairCanvas.SetActive(true);

        if (DialogueManager.Instance.quest.questCanvas != null)
        {
            DialogueManager.Instance.quest.questCanvas.SetActive(true);
        }
    }
}
