using System.Collections;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [Header("Ghost Info")]
    public StatusData thisStatusData; // <- บอกว่าผีนี้คือผีตัวไหน (ควายธนู, กระสือ ฯลฯ)
    PlayerManager playerManager;

    [Header("Ghost Settings")]
    public GameObject GhostJumpScare;
    public AudioClip jumpscareWarning;
    public AudioClip jumpscareSound;
    public int senseReduceValue;
    public float jumpScareDelay = 5f;      // เวลารอจนกระทั่ง JumpScare เอง (ถ้าไม่ถูกเรียก)
    public float JumpScareDuration = 2f;   // เวลาที่ภาพ jumpscare แสดงบนจอ

    private bool isChasedAway = false;

    // ✅ ตัวแปร static สำหรับบอกว่า “ตอนนี้มีผีกำลัง jumpscare อยู่ไหม”
    public static bool anyGhostJumpscaring = false;

    void Start()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
        StartCoroutine(JumpScareTimer());
    }

    private IEnumerator JumpScareTimer()
    {
        float timer = 0f;
        while (timer < jumpScareDelay)
        {
            if (isChasedAway)
                yield break; // ถ้าผู้เล่นไล่ผีไป หยุด Coroutine

            timer += Time.deltaTime;
            yield return null;
        }

        JumpScare();
    }

    public void JumpScare()
    {
        if (anyGhostJumpscaring)
            return; // กันไม่ให้หลายตัว jumpscare พร้อมกัน

        anyGhostJumpscaring = true;

        if (GhostJumpScare != null)
            GhostJumpScare.SetActive(true);

        if (jumpscareSound != null)
            AudioSource.PlayClipAtPoint(jumpscareSound, transform.position);

        if (playerManager != null)
            playerManager.ReduceSense(senseReduceValue);

        Debug.Log("👻 JumpScare Activated!");
        StartCoroutine(HideJumpScareAfterDelay());
    }

    private IEnumerator HideJumpScareAfterDelay()
    {
        yield return new WaitForSeconds(JumpScareDuration);

        if (GhostJumpScare != null)
            GhostJumpScare.SetActive(false);

        anyGhostJumpscaring = false;
        Destroy(gameObject);
    }

    public void ChasedAway()
    {
        isChasedAway = true;
        StopAllCoroutines();
        Destroy(gameObject);
        Debug.Log("👋 Ghost ถูกไล่ไปแล้ว");
    }
}
