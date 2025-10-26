using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBarUI : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public int slotCount = 8;

    private StatusData[] slotItems; // แทน List

    void Start()
    {
        slotItems = new StatusData[slotCount];
        
    }

    public void AddItem(StatusData item)
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (slotItems[i] == null) // เจอช่องว่าง
            {
                slotItems[i] = item;
                UpdateSlotUI(i);
                return; // เพิ่มเสร็จแล้วออก
            }
        }
        Debug.Log("Inventory เต็มแล้ว!");
    }

    public void UseItem(int index)
    {
        if (index < 0 || index >= slotCount) return;
        if (slotItems[index] == null) return;

        Debug.Log($"ใช้ของ: {slotItems[index].objectName}");
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

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => UseItem(index));
        }
        else
        {
            text.text = "EMPTY";
            img.color = new Color(255, 255, 255);
            btn.onClick.RemoveAllListeners();
        }
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
