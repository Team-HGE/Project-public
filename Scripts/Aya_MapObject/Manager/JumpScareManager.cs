using UnityEngine;
using System;
using System.Collections;
using Sirenix.OdinInspector;

public enum JumpScareType
{
    GroupTypeMonster,
    EyeTypeMonster,
    EarTypeMonster
}
[Serializable]
public class MonstersJumpScare
{
    public JumpScareType jumpScareType;
    public GameObject gameObject;
    public float time;
}
public class JumpScareManager : MonoBehaviour
{
    [TitleGroup("JumpScareManager", "MonoBehaviour", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
    [Header("FlashLight")]
    public GameObject flashLight;

    [TabGroup("Tab", "Death", SdfIconType.EmojiDizzy, TextColor = "black")]
    [TabGroup("Tab", "Death")] public GameObject playerCanvas;
    [TabGroup("Tab", "Death")] public GameObject deathVideo;
    [TabGroup("Tab", "Death")] public GameObject mainMenuBtn;
    [TabGroup("Tab", "Death")] public GameObject retryBtn;
    [TabGroup("Tab", "Death")] public GameObject blackBG;

    [Title("MonsterType")]
    public MonstersJumpScare[] monstersJumpScare;

    public void PlayJumpScare(JumpScareType jumpScareType)
    {
        AudioManager.Instance.StopAllClips();
        GameManager.Instance.playerDie = true;
        flashLight.SetActive(false);
        GameManager.Instance.PlayerStateMachine.Player.PlayerControllOff();
        GameManager.Instance.PlayerStateMachine.Player.VCOnOff();
        playerCanvas.SetActive(false);
        blackBG.SetActive(true);
        foreach (var mon in monstersJumpScare)
        {
            if (mon.jumpScareType == jumpScareType)
            {
                mon.gameObject.SetActive(true);
                StartCoroutine(OnDeathCanvas(mon.time, mon.gameObject));
                break;
            }
        }
    }
    IEnumerator OnDeathCanvas(float time, GameObject monsterObject)
    {
        yield return new WaitForSeconds(time);
        monsterObject.SetActive(false);
        playerCanvas.SetActive(true);
        deathVideo.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSeconds(3);
        retryBtn.SetActive(true);
        mainMenuBtn.SetActive(true);
        GameManager.Instance.fadeManager.fadeComplete += OffCanvas;
    }

    public void ReturnMainMenu()
    {
        OffBtn();
        EventManager.Instance.SetSwitch(GameSwitch.IsPlayingGame, false);
        GameManager.Instance.fadeManager.MoveScene(SceneEnum.MainMenuScene);
    }

    public void RetryGame()
    {
        OffBtn();
        GameDataSaveLoadManager.Instance.LoadGameData(0);
        EventManager.Instance.SetSwitch(GameSwitch.IsPlayingGame, false);
        GameManager.Instance.fadeManager.MoveScene(GameDataSaveLoadManager.Instance.ReturnSceneEnum());
    }

    public void OffCanvas()
    {
        deathVideo.SetActive(false);
    }

    public void OffBtn()
    {
        retryBtn.SetActive(false);
        mainMenuBtn.SetActive(false);
    }
}
