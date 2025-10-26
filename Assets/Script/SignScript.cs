using UnityEngine;
using TMPro;

public class SignScript : MonoBehaviour
{
    PlayerManager playerManager;
    public GameManager gameManager;
    public StatusData thisStatusData; // สร้าง Sign นี้จาก MapTemplateScript
    public TextMeshProUGUI textMeshPro; // อ้างอิงข้อความในฉาก

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerManager = FindFirstObjectByType<PlayerManager>();

        if (thisStatusData != null && textMeshPro != null)
        {
            textMeshPro.text = thisStatusData.signText;
        }
    }

    public void PrintInfo()
    {
        if (thisStatusData != null)
            Debug.Log("INFO : " + thisStatusData.signText);
    }

    public void ChageRound()
    {
        if (thisStatusData != null)
            playerManager.ChangePlayerStatus(thisStatusData);
        gameManager.ChangeRound();
    }

    public void ChageRoundEvent()
    {
        if (thisStatusData != null)
            playerManager.ChangePlayerStatus(thisStatusData);
        gameManager.ChangeRound(thisStatusData.eventName);
    }
}
