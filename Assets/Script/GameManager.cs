using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int gameRoundTotal = 10;
    [SerializeField] private int currentRound = 1;
    public PlayerManager playerManager;
    public List<GameObject> mapTemplates = new List<GameObject>();
    public List<GameObject> eventMapTemplates = new List<GameObject>();
    public MapTemplateScript currentMapTemplate;
    public GameObject currentMapInstance;


    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        CreateMap();

    }

    public void ChangeRound()
    {
        if (currentRound <= gameRoundTotal)
        {
            currentRound++;

            if (currentMapInstance != null)
                Destroy(currentMapInstance);

            CreateMap();
        }

    }
    public void ChangeRound(string eventName)
    {
        if (currentRound <= gameRoundTotal)
        {
            currentRound++;

            if (currentMapInstance != null)
                Destroy(currentMapInstance);

            CreateMap(eventName);
        }

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
    void CreateMap(string eventName)
{
    // หาว่า eventMapTemplates มีแมพที่ชื่อเดียวกับ eventName ไหม
    GameObject selectedEventMap = eventMapTemplates.Find(map => map.name == eventName);

    if (selectedEventMap != null)
    {
        // สร้างแมพใหม่
        currentMapInstance = Instantiate(selectedEventMap, Vector3.zero, Quaternion.identity);

        // ✅ ดึงสคริปต์ MapTemplateScript จากแมพใหม่
        currentMapTemplate = currentMapInstance.GetComponent<MapTemplateScript>();

        // ✅ เรียกสุ่มวัตถุใน event map
        if (currentMapTemplate != null)
        {
            currentMapTemplate.RandomObject(
                playerManager.currentMoralStats,
                playerManager.currentInsaneStats,
                playerManager.currentSenseStats,
                eventName

            );
        }
        else
        {
            Debug.LogError($"Event map '{eventName}' ไม่มี MapTemplateScript!");
        }

        Debug.Log($"Loaded event map: {eventName}");
    }
    else
    {
        // ถ้าไม่เจอ event map ให้ fallback ไปสร้าง map ปกติ
        Debug.LogWarning($"ไม่พบ event map ชื่อ '{eventName}' ใน eventMapTemplates — จะใช้แมพปกติแทน");
        CreateMap();
    }
}



}
