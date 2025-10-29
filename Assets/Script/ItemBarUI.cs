using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBarUI : MonoBehaviour
{
    public UI_Notifier uI_Notifier;
    [Header("Slots")]
    public List<GameObject> slots = new List<GameObject>();
    public Sprite defaultSlotSprite; // ใส่ sprite ฉากขาวใน inspector
    public int slotCount = 8;

    [Header("Animation")]
    public RectTransform animationLayer; // ใส่ Canvas Panel สำหรับ Animation
    public float animDuration = 0.5f;

    public StatusData[] slotItems; // แทน List

    void Start()
    {
        slotItems = new StatusData[slotCount];
    }

    public void AddItem(StatusData item,GameObject objectItem)
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (slotItems[i] == null)
            {
                slotItems[i] = item;
                UpdateSlotUI(i);
                Destroy(objectItem);
                return;
            }
        }
        Debug.Log("Inventory เต็มแล้ว!");
        
        uI_Notifier.Show("Inventory is Full!");
        
        
    }

    public void UseItem(int index)
    {
        if (index < 0 || index >= slotCount) return;
        if (slotItems[index] == null) return;

        StatusData item = slotItems[index];
        Debug.Log($"ใช้ของ: {item.objectName}");

        // เรียกใช้สกิล
        if (item.SO_Skill is ISkill skill)
        {
            skill.UseItem();
        }

        // เล่น Animation
        SpriteRenderer sr = item.ObjectPrefab.GetComponent<SpriteRenderer>();
        if (sr != null)
            StartCoroutine(PlayUseItemAnimation(sr.sprite));

        // ลบของออกจาก slot
        slotItems[index] = null;
        UpdateSlotUI(index);
    }

    private void UpdateSlotUI(int index)
    {
        TMP_Text text = slots[index].GetComponentInChildren<TMP_Text>();
        Image img = slots[index].GetComponentInChildren<Image>();
        Button btn = slots[index].GetComponent<Button>();

        if (slotItems[index] != null)
        {
            StatusData item = slotItems[index];
            text.text = item.objectName;
            img.color = Color.white;

            SpriteRenderer sr = item.ObjectPrefab.GetComponent<SpriteRenderer>();
            if (sr != null)
                img.sprite = sr.sprite;

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => UseItem(index));
        }
        else
        {
            text.text = "EMPTY";
            img.sprite = defaultSlotSprite;
            img.color = Color.white;
            btn.onClick.RemoveAllListeners();
        }
    }

    private IEnumerator PlayUseItemAnimation(Sprite itemSprite)
    {
        GameObject tempGO = new GameObject("ItemAnim");
        tempGO.transform.SetParent(animationLayer, false);

        Image img = tempGO.AddComponent<Image>();
        img.sprite = itemSprite;
        img.SetNativeSize();

        RectTransform rt = img.rectTransform;

        // จุดเริ่มต้น: มุมซ้ายล่าง
        rt.anchoredPosition = new Vector2(-Screen.width / 2, -Screen.height / 2);
        rt.localScale = Vector3.one * 0.5f;
        img.color = Color.white;

        float elapsed = 0f;
        float moveDuration = animDuration * 0.6f; // 60% เวลาเลื่อน
        float fadeDuration = animDuration * 0.4f; // 40% เวลา fade

        Vector2 startPos = rt.anchoredPosition;
        Vector2 midPos = Vector2.zero; // กลางจอ
        Vector3 startScale = rt.localScale;
        Vector3 endScale = Vector3.one;

        // Phase 1: เลื่อน + ขยาย
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;

            rt.anchoredPosition = Vector2.Lerp(startPos, midPos, t);
            rt.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        // หยุดกลางจอสักพัก
        yield return new WaitForSeconds(0.2f);

        // Phase 2: ค่อย ๆ จาง
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            img.color = new Color(1f, 1f, 1f, 1f - t);

            yield return null;
        }

        Destroy(tempGO);
    }


    void Update()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                UseItem(i);
            }
        }
    }
}
