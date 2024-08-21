using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public struct Switch
{
    public GameSwitch switchType;
    public bool state;
}

public class EventManager : SingletonManager<EventManager>
{
    public List<bool> switchStates;
    public Switch[] switches;
    public event Action<GameSwitch, bool> OnSwitchChanged;

    [Title("SceneEventManager")]
    public SceneEventManager sceneEventManager;

    protected override void Awake()
    {
        base.Awake();
        if (sceneEventManager == null) sceneEventManager = GetComponent<SceneEventManager>();
    }


    public void InitializeSwitches()
    {
        int switchCount = System.Enum.GetValues(typeof(GameSwitch)).Length;
        if (switchStates == null || switchStates.Count != switchCount)
        {
            switchStates = new List<bool>(new bool[switchCount]);
        }
    }
    public void SetSwitch(GameSwitch switchType, bool state)
    {
        // ���� switchStates ����Ʈ���� �ش� ����ġ�� ���¸� ������Ʈ
        int index = (int)switchType;
        if (index >= 0 && index < switchStates.Count)
        {
            switchStates[index] = state; // ���� ����ġ ���� ������Ʈ

            OnSwitchChanged?.Invoke(switchType, state);
        }
        else
        {
            
            return;
        }
    }

    public bool GetSwitch(GameSwitch switchType)
    {
        int index = (int)switchType;
        if (index >= 0 && index < switchStates.Count)
        {
            return switchStates[index];
        }
        return false;
    }

    //EventManager.Instance.SetSwitch(GameSwitch.HasKey, true);
}