using System.Collections;
using UnityEngine;
using TMPro;

public class SignScript : MonoBehaviour
{
    PlayerManager playerManager;
    MapTemplateScript mapTemplateScript;
    public GameManager gameManager;
    public StatusData thisStatusData; // ข้อมูลของป้ายนี้
    public TextMeshProUGUI textMeshPro; // อ้างอิงข้อความในฉาก

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerManager = FindFirstObjectByType<PlayerManager>();
        mapTemplateScript = FindFirstObjectByType<MapTemplateScript>();

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

    // 🔹 ฟังก์ชันกดป้ายเปลี่ยนรอบ (เวอร์ชันใหม่)
    public void ChageRound()
    {
        StartCoroutine(WaitGhostJumpscareThenChangeRound());
    }

    private IEnumerator WaitGhostJumpscareThenChangeRound()
    {
        bool hasGhost = false;

        // เรียก JumpScare ทุกตัวในแมพปัจจุบันก่อน
        foreach (GameObject ghost in mapTemplateScript.ghostGameObj)
        {
            if (ghost == null)
                continue;

            GhostScript ghostScript = ghost.GetComponent<GhostScript>();
            if (ghostScript != null)
            {
                hasGhost = true;
                ghostScript.JumpScare();
            }
        }

        // ✅ ถ้ามีผี ให้รอจนกว่า JumpScare จะเสร็จ
        if (hasGhost)
        {
            Debug.Log("⏳ Waiting for ghosts to finish jumpscare...");
            yield return new WaitUntil(() => !GhostScript.anyGhostJumpscaring);
        }

        // เปลี่ยนสถานะและเปลี่ยนรอบ
        if (thisStatusData != null)
            playerManager.ChangePlayerStatus(thisStatusData);

        Debug.Log("🌍 Changing to new round...");
        gameManager.ChangeRound();
    }

    // 🔹 กรณีที่เป็น Event Sign (ก็ใช้ระบบรอเหมือนกัน)
    public void ChageRoundEvent()
    {
        StartCoroutine(WaitGhostJumpscareThenChangeRoundEvent());
    }

    private IEnumerator WaitGhostJumpscareThenChangeRoundEvent()
    {
        bool hasGhost = false;

        if (mapTemplateScript.ghostGameObj != null)
        {
            foreach (GameObject ghost in mapTemplateScript.ghostGameObj)
            {
                if (ghost == null) continue;
                GhostScript ghostScript = ghost.GetComponent<GhostScript>();
                if (ghostScript != null)
                {
                    hasGhost = true;
                    ghostScript.JumpScare();
                }
            }
        }

        if (hasGhost)
        {
            Debug.Log("⏳ Waiting for ghosts to finish jumpscare...");
            yield return new WaitUntil(() => !GhostScript.anyGhostJumpscaring);
        }

        if (thisStatusData != null)
            playerManager.ChangePlayerStatus(thisStatusData);

        Debug.Log("🎭 Changing event round...");
        gameManager.ChangeRound(thisStatusData.eventName);
    }
}
