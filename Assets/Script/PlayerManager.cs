using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    [SerializeField] private int moralStats; // ความดี
    [SerializeField] private int insaneStats; // ความบ้าคลั่ง
    [SerializeField] private int senseStats; // สติ
    public int currentMoralStats;
    public int currentInsaneStats;
    public int currentSenseStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMoralStats = moralStats;
        currentInsaneStats = insaneStats;
        currentSenseStats = senseStats;
    }

    
    public void ChangePlayerStatus(StatusData statusData)
    {
        currentMoralStats += statusData.moralStatsChange;
        currentInsaneStats += statusData.insaneStatsChange;
        currentSenseStats += statusData.senseStatsChange;

        Debug.Log("Player current status Moral: " + currentMoralStats + " Insane: " + currentInsaneStats + " Sense: " + currentSenseStats);
    }
}
