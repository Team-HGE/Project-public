using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterGameData
{
    public MonsterGameData()
    {
        position = new SerializableVecter3();
        rotation = new SerializableVecter3();
    }
    public SerializableVecter3 position; // ���� ��ġ����
    public SerializableVecter3 rotation; // ���� ȸ�� ����
}

public class MonsterGameDataManager : MonoBehaviour
{
    public Transform[] monsters;
    public List<MonsterGameData> GetData() // ���̺�
    {
        List<MonsterGameData> monsterDataList = new List<MonsterGameData>();

        monsters = HotelFloorScene_DataManager.Instance.GetMonster_Transform();
        for (int i = 0; i < monsters.Length; i++)
        {
            MonsterGameData monsterGameData = new MonsterGameData();
            monsterGameData.position.SetVector(monsters[i].position);
            monsterGameData.rotation.SetVector(monsters[i].rotation.eulerAngles);
            monsterDataList.Add(monsterGameData);
        }
        monsterDataList.Clear();
        return monsterDataList;
    }

    public void ApplyGameData(List<MonsterGameData> monsterGameData) // �ҷ�����
    {
        monsters = HotelFloorScene_DataManager.Instance.GetMonster_Transform();
        return;
        for (int i = 0; i < monsterGameData.Count;i++)
        {
            if (monsters.Length < i)
            {
                return;
            }
            monsters[i].position = monsterGameData[i].position.GetVector();
            monsters[i].rotation = Quaternion.Euler(monsterGameData[i].rotation.GetVector());
        }
    }
}
