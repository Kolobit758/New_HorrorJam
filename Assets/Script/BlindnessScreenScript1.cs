using UnityEngine;

public class BlindnessScreenScript : MonoBehaviour
{
    [Header("ค่าความสว่างที่เริ่มมองเห็นได้")]
    public int minToBright = 80;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ReduceTransparency(int sense)
    {
        if (spriteRenderer == null) return;

        // แปลง sense เป็นค่า alpha ระหว่าง 0 ถึง 1
        // sense = 0 → มืดสุด (ทึบ)
        // sense = 100 → ใสสุด (โปร่งใส)
        float alpha = 1f - Mathf.Clamp01(sense / 100f);

        // หากอยากให้เริ่มใสขึ้นหลังจากเกิน minToBright
        if (sense > minToBright)
        {
            // ทำให้ค่อยๆ ใสขึ้นตาม sense
            float t = Mathf.InverseLerp(minToBright, 100f, sense);
            alpha = Mathf.Lerp(1f - (minToBright / 100f), 0f, t);
        }

        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}
