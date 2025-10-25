using System.Collections.Generic;
using UnityEngine;

public class EventMapManager : MonoBehaviour
{
    public SO_Object allGameObject;
    public List<StatusData> statusDatas = new List<StatusData>();
    public GameObject background;
    public List<Transform> signs = new List<Transform>();
    public List<Transform> items = new List<Transform>();
    public List<Transform> structure = new List<Transform>();
    public List<StatusData> validObjects = new List<StatusData>();

    public void RandomObject(int currentMoralStats, int currentInsaneStats, int currentSenseStats, string eventName)
    {
        statusDatas.Clear();
        validObjects.Clear();
        foreach (StatusData statusData in allGameObject.gameObjects)
        {
            statusDatas.Add(statusData);

        }
        // สร้างลิสต์เฉพาะที่ผ่านเงื่อนไข requirement
        validObjects = statusDatas;


        for (int i = validObjects.Count - 1; i >= 0; i--)
        {
            StatusData scopeData = validObjects[i];
            if ((scopeData.reuquirMoralStats < currentMoralStats || scopeData.reuquirInsaneStats < currentInsaneStats || scopeData.reuquirSenseStats < currentSenseStats) && scopeData.eventName != eventName)
            {
                validObjects.RemoveAt(i);
            }
        }
        SpawnRandomObjects(validObjects, StatusData.objectType.Sign, signs);
        SpawnRandomObjects(validObjects, StatusData.objectType.Item, items);
        SpawnRandomObjects(validObjects, StatusData.objectType.structure, structure);

    }
    

    private void SpawnRandomObjects(List<StatusData> validObjects, StatusData.objectType type, List<Transform> spawnPoints)
    {
        List<StatusData> filtered = validObjects.FindAll(o => o.object_Type == type);
        
        Debug.Log($"Spawning type {type} | Found {filtered.Count} valid objects | Spawn points {spawnPoints.Count}");

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (filtered.Count == 0) break;

            int randomIndex = Random.Range(0, filtered.Count);
            StatusData chosen = filtered[randomIndex];
            Transform spawn = spawnPoints[i];

            Debug.Log($"Spawning {chosen.objectName} at {spawn.position}");
            GameObject Prefab = Instantiate(chosen.ObjectPrefab, spawn.position, spawn.rotation, transform);
            Prefab.name = chosen.objectName;

            filtered.RemoveAt(randomIndex);
        }
    }
}
