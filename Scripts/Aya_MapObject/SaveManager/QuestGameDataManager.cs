using System;
using UnityEngine;


[Serializable]
public class QuestGameData
{

}
public class QuestGameDataManager : MonoBehaviour
{
    public QuestGameData GetData() // ���̺�
    {
        QuestGameData questGameData = new QuestGameData();

        return questGameData;
    }

    public void ApplyGameData(QuestGameData questGameData) // �ҷ�����
    {

    }
}
