using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn; // ������ ���� ������Ʈ�� ����Ʈ

    [Header("Spawn Range")]
    public float minX = -10f;
    public float maxX = 60f;
    public float minZ = 150f;
    public float maxZ = 215f;
    public float fixedY = 0f;

    [Header("Except Range")]
    public float exMinX = -10f;
    public float exMaxX = 70f;
    public float exMinZ = 150f;
    public float exMaxZ = 160f;

    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Start()
    {
        if (objectsToSpawn.Count == 0)
        {
            Debug.LogError("������ ������Ʈ ����� ��� �ֽ��ϴ�.");
            return;
        }
    }

    public void SpawnObjects()
    {
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            HotelFloorScene_DataManager.Instance.npc_Transforms[i] = objectsToSpawn[i].transform;
        }
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            Vector3 spawnPosition = GenerateRandomPosition();
            while (spawnedPositions.Contains(spawnPosition))
            {
                spawnPosition = GenerateRandomPosition();
            }

            GameObject objectToSpawn = objectsToSpawn[i];
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            spawnedPositions.Add(spawnPosition);
        }
    }

    Vector3 GenerateRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        // ���� ���� ���� ������ ���� ��, �ƴ� ������ ���� ������
        while ((randomX >= exMinX && randomX <= exMaxX) && (randomZ >= exMinZ && randomZ <= exMaxZ))
        {
            randomX = Random.Range(minX, maxX);
            randomZ = Random.Range(minZ, maxZ);
        }

        return new Vector3(randomX, fixedY, randomZ);
    }
}
