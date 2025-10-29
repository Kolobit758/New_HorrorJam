using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/GhostChaseSkill")]
public class GhostChaseSkill : ScriptableObject, ISkill
{
    [Header("Skill Info")]
    public StatusData statusData;              // ใช้เปลี่ยนสถานะผู้เล่น
    public List<StatusData> ghostCanChase;     // ลิสต์ผีที่สามารถถูกไล่ด้วยสกิลนี้
    public float chaseDelay = 0.2f;            // เวลาหน่วงก่อนผีหนี

    public void UseItem()
    {
        // หา Ghost ทั้งหมดในฉาก
        GhostScript[] allGhosts = FindObjectsByType<GhostScript>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        Debug.Log($"พบผีทั้งหมด {allGhosts.Length} ตัว");

        int chasedCount = 0;

        foreach (var ghost in allGhosts)
        {
            if (ghost == null || ghost.thisStatusData == null) continue;

            // ✅ ไล่เฉพาะผีที่อยู่ใน ghostCanChase
            if (ghostCanChase.Contains(ghost.thisStatusData))
            {
                ghost.ChasedAway();
                chasedCount++;
                Debug.Log($"💨 ไล่ผีชื่อ {ghost.thisStatusData.objectName}");
            }
        }

        Debug.Log($"👻 ไล่ผีได้ทั้งหมด {chasedCount} ตัว");
    }
}