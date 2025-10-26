using System.Collections.Generic;
using UnityEngine;

public class MapTemplateScript : MonoBehaviour
{
    public SO_Object allGameObject;
    public List<StatusData> statusDatas = new List<StatusData>();
    public GameObject background;
    public List<Transform> signs = new List<Transform>();
    public List<Transform> items = new List<Transform>();
    public List<Transform> structure = new List<Transform>();
    public List<StatusData> validObjects = new List<StatusData>();

    void Start()
    {
        foreach (StatusData statusData in allGameObject.gameObjects)
        {
            statusDatas.Add(statusData);
            Debug.Log("add : " + statusData.name);

        }
    }
    public void RandomObject(int currentMoralStats, int currentInsaneStats, int currentSenseStats)
    {

        foreach (StatusData statusData in allGameObject.gameObjects)
        {
            statusDatas.Add(statusData);

        }
        validObjects.Clear();

        // สร้างลิสต์เฉพาะที่ผ่านเงื่อนไข requirement
        validObjects = new List<StatusData>(statusDatas);


        for (int i = validObjects.Count - 1; i >= 0; i--)
        {
            StatusData scopeData = validObjects[i];
            if (scopeData.reuquirMoralStats < currentMoralStats ||
                scopeData.reuquirInsaneStats < currentInsaneStats ||
                scopeData.reuquirSenseStats < currentSenseStats)
            {

                validObjects.RemoveAt(i);

            }
        }
            foreach (StatusData valid in validObjects)
            {
                Debug.Log("NEW : " + valid.objectName);
            }
            SpawnRandomObjects(validObjects, StatusData.objectType.Sign, signs);
            SpawnRandomObjects(validObjects, StatusData.objectType.Item, items);
            SpawnRandomObjects(validObjects, StatusData.objectType.structure, structure);





        }
    public void RandomObject(int currentMoralStats, int currentInsaneStats, int currentSenseStats, string eventName)
    {
        Debug.Log($"[Before Spawn] Valid count = {validObjects.Count}");
        foreach (var v in validObjects)
        {
            Debug.Log($"✅ {v.name} | Type: {v.object_Type} | Require M/I/S: {v.reuquirMoralStats}/{v.reuquirInsaneStats}/{v.reuquirSenseStats}");
        }

        statusDatas.Clear();
        validObjects.Clear();
        foreach (StatusData statusData in allGameObject.gameObjects)
        {
            statusDatas.Add(statusData);

        }
        // สร้างลิสต์เฉพาะที่ผ่านเงื่อนไข requirement
        validObjects = new List<StatusData>(statusDatas);


        for (int i = validObjects.Count - 1; i >= 0; i--)
        {
            StatusData scopeData = validObjects[i];
            if (scopeData.eventName != eventName &&
            (scopeData.reuquirMoralStats > currentMoralStats ||
                scopeData.reuquirInsaneStats > currentInsaneStats ||
                scopeData.reuquirSenseStats > currentSenseStats))
            {


                Debug.Log($"❌ Removed {scopeData.name} | Require M/I/S: {scopeData.reuquirMoralStats}/{scopeData.reuquirInsaneStats}/{scopeData.reuquirSenseStats}");
                validObjects.RemoveAt(i);
            }
            else
            {
                Debug.Log($"✅ Kept {scopeData.name} | Type: {scopeData.object_Type}");
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
