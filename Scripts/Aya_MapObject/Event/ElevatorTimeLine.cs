﻿using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class ElevatorTimeLine : MonoBehaviour
{
    public bool triggerOn; 
    public PlayableDirector timelineDirector;
    public GameObject timeLineObject;
    public GameObject playerLight;

    public TextMeshPro roomTxt101;
    public TextMeshPro roomTxt102;
    public TextMeshPro roomTxt101_111;
    public TextMeshPro roomTxt112_121;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggerOn)
        {
            triggerOn = true;
            AudioManager.Instance.StopBackGroundSound(BackGroundSound.ChaseBG, true);
            GameManager.Instance.fadeManager.fadeComplete += ElevatorMovie;
            StartCoroutine(GameManager.Instance.fadeManager.FadeStart(FadeState.FadeOut));
        }
    }

    private void ElevatorMovie()
    {
        GameManager.Instance.NowPlayCutScene = true;

        timeLineObject.SetActive(true);
        playerLight.SetActive(false);

        GameManager.Instance.Off_UI();
        GameManager.Instance.playerDie = true;
        timelineDirector.Play();
        GameManager.Instance.fadeManager.fadeComplete -= ElevatorMovie;
    }

    public void OffLight()
    {
        GameManager.Instance.lightManager.OffLaversAllLight();
    }

    public void ChangeTxt()
    {
        roomTxt101.text = "2";
        roomTxt102.text = "2";
        roomTxt101_111.text = "201 - 211";
        roomTxt112_121.text = "212 - 221";
    }

    public void FadeOff()
    {
        StartCoroutine(GameManager.Instance.fadeManager.FadeStart(FadeState.FadeIn));
    }
    public void ChangeScene()
    {
        GameManager.Instance.playerDie = false;
        // 2일차 신 호출
        GameManager.Instance.fadeManager.MoveScene(SceneEnum.Hotel_Day2);
        Invoke("SetUI", 1.5f);
    }

    void SetUI()
    {
        GameManager.Instance.On_UI();
    }
}
