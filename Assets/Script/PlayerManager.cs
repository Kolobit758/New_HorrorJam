using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int moralStats;
    [SerializeField] private int insaneStats;
    [SerializeField] private int senseStats;

    [Header("Current Values")]
    public int currentMoralStats;
    public int currentInsaneStats;
    public int currentSenseStats;

    [Header("UI Reference")]
    public Slider senseSlider; // <<<< เพิ่มตรงนี้

    void Start()
    {
        currentMoralStats = moralStats;
        currentInsaneStats = insaneStats;
        currentSenseStats = senseStats;

        // ตั้งค่าเริ่มต้นให้ slider
        if (senseSlider != null)
        {
            senseSlider.maxValue = 100; // ตั้งค่าสูงสุดตามต้องการ
            senseSlider.value = currentSenseStats;
        }
    }

    public void ChangePlayerStatus(StatusData statusData)
    {
        currentMoralStats += statusData.moralStatsChange;
        currentInsaneStats += statusData.insaneStatsChange;
        currentSenseStats += statusData.senseStatsChange;

        Check();

        Debug.Log($"Player current status Moral: {currentMoralStats}, Insane: {currentInsaneStats}, Sense: {currentSenseStats}");

        UpdateUI();
    }

    public void ReduceSense(int reduceValue)
    {
        currentSenseStats -= reduceValue;
        Check();
        Debug.Log($"Player current status Moral: {currentMoralStats}, Insane: {currentInsaneStats}, Sense: {currentSenseStats}");
        UpdateUI();
    }
    void UpdateUI()
    {
        if (senseSlider != null)
        {
            senseSlider.value = currentSenseStats;
        }
    }

    void Check()
    {
        if (currentInsaneStats > insaneStats)
        {
            currentInsaneStats = insaneStats;
        }
        else if (currentInsaneStats < 0)
        {
            currentInsaneStats = 0;
        }

        if (currentMoralStats > moralStats)
        {
            currentMoralStats = moralStats;
        }
        else if (currentMoralStats < 0)
        {
            currentMoralStats = 0;
        }
        if (currentSenseStats > senseStats)
        {
            currentSenseStats = senseStats;
        }
        else if (currentSenseStats < 0)
        {
            currentSenseStats = 0;
        }
    }
}
