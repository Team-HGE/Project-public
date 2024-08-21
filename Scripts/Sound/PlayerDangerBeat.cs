using System.Collections.Generic;
using UnityEngine;

public class PlayerDangerBeat : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public AudioSource bgmSource; // BGM AudioSource
    public List<Transform> objects; // Ư�� ������Ʈ���� Transform ����Ʈ
    public float maxVolume = 1.0f; // �ִ� ����
    public float minVolume = 0.0f; // �ּ� ����
    public float maxDistance = 10.0f; // �ִ� �Ÿ�

    void Update()
    {
        AdjustVolumeBasedOnDistance();
    }

    void AdjustVolumeBasedOnDistance()
    {
        float closestDistance = float.MaxValue;

        foreach (Transform obj in objects)
        {
            float distance = Vector3.Distance(player.position, obj.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
            }
        }

        // �Ÿ��� maxDistance�� �ʰ��ϸ� minVolume, �Ÿ��� �������� maxVolume�� �������
        float volume = Mathf.Lerp(maxVolume, minVolume, closestDistance / maxDistance);
        bgmSource.volume = Mathf.Clamp(volume, minVolume, maxVolume);
    }
}