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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (StatusData statusData in allGameObject.gameObjects)
        {
            statusDatas.Add(statusData);

        }



    }
    public void RandomObject(int currentMoralStats, int currentInsaneStats, int currentSenseStats)
    {
        // สร้างลิสต์เฉพาะที่ผ่านเงื่อนไข requirement
        List<StatusData> validObjects = new List<StatusData>(statusDatas);

        if (signs != null && items != null && structure != null)
        {
            for (int i = validObjects.Count - 1; i >= 0; i--)
            {
                StatusData scopeData = validObjects[i];
                if (scopeData.reuquirMoralStats > currentMoralStats || scopeData.reuquirInsaneStats > currentInsaneStats || scopeData.reuquirSenseStats > currentSenseStats)
                {
                    validObjects.RemoveAt(i);
                }
            }

            SpawnRandomObjects(validObjects, StatusData.objectType.Sign, signs);
            SpawnRandomObjects(validObjects, StatusData.objectType.Item, items);
            SpawnRandomObjects(validObjects, StatusData.objectType.structure, structure);

        }
    }
    
    private void SpawnRandomObjects(List<StatusData> validObjects,StatusData.objectType type,List<Transform> spawnPoints)
    {
        List<StatusData> filtered = validObjects.FindAll(o => o.object_Type == type);
        Debug.Log(filtered);

        for (int i = 0; i < filtered.Count; i++)
        {
            int randomIndex = Random.Range(i, filtered.Count);
            (filtered[i], filtered[randomIndex]) = (filtered[randomIndex], filtered[i]);
        }

        // วนตามจำนวนจุด spawn
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (filtered.Count == 0) break; // ถ้าวัตถุหมดแล้วก็หยุด

            StatusData chosen = filtered[Random.Range(0, filtered.Count)];
            Transform spawn = spawnPoints[i];

            Instantiate(chosen.ObjectPrefab, spawn.position, spawn.rotation, transform);
        }
    }
    

    
}
