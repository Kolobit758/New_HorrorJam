using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int gameRoundTotal = 10;
    [SerializeField] private int currentRound = 1;
    public PlayerManager playerManager;
    public List<GameObject> mapTemplates = new List<GameObject>();
    public MapTemplateScript currentMapTemplate;
    public GameObject currentMapInstance;


    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        CreateMap();
        
    }

    public void ChangeRound()
    {
        currentRound++;

        if (currentMapInstance != null)
            Destroy(currentMapInstance);

        CreateMap();
    }

    void CreateMap()
    {
        // เลือก map prefab แบบสุ่ม
        int randomIndex = Random.Range(0, mapTemplates.Count);
        GameObject mapPrefab = mapTemplates[randomIndex];

        // สร้าง map instance
        currentMapInstance = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity);

        // ดึง MapTemplateScript ของ instance
        currentMapTemplate = currentMapInstance.GetComponent<MapTemplateScript>();

        // สุ่มวัตถุตามสเตตัสผู้เล่น
        if (currentMapTemplate != null)
        {
            currentMapTemplate.RandomObject(playerManager.currentMoralStats, playerManager.currentInsaneStats, playerManager.currentSenseStats);
        }
        else
        {
            Debug.LogError("Map prefab ไม่มี MapTemplateScript!");
        }
    }

}
