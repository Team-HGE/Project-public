using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SceneGameData
{
    public List<bool> switchStates;
}
public class SceneGameDataManager : MonoBehaviour
{
    public SceneGameData GetData() // ���̺�
    {
        SceneGameData sceneGameData = new SceneGameData();
        sceneGameData.switchStates = EventManager.Instance.switchStates;
        return sceneGameData;
    }
    public void ApplyGameData(SceneGameData sceneGameData) // �ҷ�����
    {
        if (sceneGameData.switchStates == null)
        {
            SwitchListInit(sceneGameData);
        }
        EventManager.Instance.switchStates = sceneGameData.switchStates;
    }

    public void SwitchListInit(SceneGameData sceneGameData)
    {
        int switchCount = System.Enum.GetValues(typeof(GameSwitch)).Length;

        sceneGameData.switchStates = new List<bool>();
        for (int i = 0; i < switchCount; i++)
        {
            sceneGameData.switchStates.Add(false); 
        }
    }
}


