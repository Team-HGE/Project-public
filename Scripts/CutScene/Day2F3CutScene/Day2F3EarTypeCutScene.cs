using UnityEngine;
using UnityEngine.Playables;

public class Day2F3EarTypeCutScene : MonoBehaviour
{
    public GameObject cutSceneObject;
    private ICutSceneEvent _trigger;
    public GameObject SM;

    [field: Header("TimeLine")]
    public PlayableDirector cutScene;
    public GameObject TLmonster;
    public GameObject VC;

    private void Start()
    {
        if (cutSceneObject != null)
        {
            _trigger = cutSceneObject.gameObject.GetComponent<ICutSceneEvent>();
            _trigger.OnEvent += PlayCutScene;
        }

        if (cutScene != null)
        {
            cutScene.stopped += OnPlayableDirectorStopped;
        }
    }

    private void PlayCutScene()
    {
        if (cutScene != null)
        {
            VC.SetActive(true);
            TLmonster.SetActive(true);

            GameManager.Instance.NowPlayCutScene = true;
            GameManager.Instance.PlayerStateMachine.Player.VCOnOff();
            GameManager.Instance.PlayerStateMachine.Player.PlayerControllOnOff();
            // ���̾�α� ������
            DialogueManager.Instance.quest.questCanvas.SetActive(false);
            cutScene.Play();
        }
        else Debug.LogError("Day2F3EarTypeCutScene - Ÿ�Ӷ����� �����ϴ�.");
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        VC.SetActive(false);
        GameManager.Instance.NowPlayCutScene = false;
        GameManager.Instance.PlayerStateMachine.Player.VCOnOff();
        GameManager.Instance.PlayerStateMachine.Player.PlayerControllOnOff();
        TLmonster.SetActive(false);
        SM.SetActive(true);
        DialogueManager.Instance.quest.questCanvas.SetActive(true);
    }
}
