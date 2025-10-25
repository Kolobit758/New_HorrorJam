using UnityEngine;
using TMPro;
public class SignScript : MonoBehaviour
{
    PlayerManager playerManager;
    public StatusData statusData;
    public TextMeshProUGUI textMeshPro; // อ้างอิงถึงข้อความในฉาก

    void Start()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
        textMeshPro.text = statusData.signText;
    }
    public void PrintInfo()
    {
        Debug.Log("INFO : " + statusData.signText);
    }

    public void ChangePlayerstats()
    {
        playerManager.ChangePlayerStatus(statusData);
    }

}
