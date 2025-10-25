using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Rendering.LookDev;
public class SignScript : MonoBehaviour
{
    PlayerManager playerManager;
    public GameManager gameManager;
    public List<StatusData> SignDatas = new List<StatusData>();
    public StatusData thisStatusData;
    public TextMeshProUGUI textMeshPro; // อ้างอิงถึงข้อความในฉาก

    void Start()
    {
        thisStatusData = SignDatas[Random.Range(0, SignDatas.Count)];
        gameManager = FindFirstObjectByType<GameManager>();
        playerManager = FindFirstObjectByType<PlayerManager>();
        textMeshPro.text = thisStatusData.signText;
    }
    public void PrintInfo()
    {
        Debug.Log("INFO : " + thisStatusData.signText);
    }

    public void SetEventSignName(string name)
    {
        List<StatusData> filtered = SignDatas.FindAll(o => o.eventName == name);

        if (filtered.Count > 0)
        {
            thisStatusData = filtered[Random.Range(0, filtered.Count)];
            textMeshPro.text = thisStatusData.signText;
        }
        else
        {
            Debug.LogWarning($"ไม่พบ SignData ที่มี eventName = {name}");
        }
    }

    public void ChageRound()
    {
        playerManager.ChangePlayerStatus(thisStatusData);
        gameManager.ChangeRound();
    }
    public void ChageRoundEvent()
    {
        playerManager.ChangePlayerStatus(thisStatusData);
        gameManager.ChangeRound(thisStatusData.eventName);
    }

}
