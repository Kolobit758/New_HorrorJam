using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // ✅ อย่าลืม import นี้

public class SubmaryUIManager : MonoBehaviour
{
    public List<Sprite> submaryImage = new List<Sprite>(); // ✅ ใช้ Sprite แทน Image
    public List<string> submaryText = new List<string>();
    public List<StatusData> KeyItems = new List<StatusData>();
    public GameObject TextCanvas;
    public TextMeshProUGUI text;
    public Image image; // UI Image ที่จะแสดงในหน้าจอ
    public ItemBarUI itemBarUI;

    void Start()
    {
        // TextCanvas.SetActive(false);
        
    }
    public void UIchanging(int index)
    {
        TextCanvas.SetActive(true);
        if (submaryText != null)
        {
            text.text = submaryText[index];
        }
        if (submaryImage != null)
        {
            image.sprite = submaryImage[index]; // ✅ เปลี่ยน sprite ของ Image บนจอ
        }

        Debug.Log("Ending");
    }

    public void GameSubmary()
    {
        if (itemBarUI == null || itemBarUI.slotItems == null) return;

        bool allKeyItemsCollected = true; // สมมติว่าครบก่อน

        foreach (StatusData keyItem in KeyItems)
        {
            bool found = false;

            // เช็คแต่ละ item ในช่อง
            foreach (StatusData slotItem in itemBarUI.slotItems)
            {
                if (slotItem == keyItem) // ตรวจว่าเป็นอันเดียวกัน
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                allKeyItemsCollected = false;
                break;
            }
        }

        if (allKeyItemsCollected)
        {
            Debug.Log("🎉 มี Key Item ครบแล้ว!");
            UIchanging(0);

        }
        else
        {
            Debug.Log("❌ ยังไม่ครบ Key Item");
            UIchanging(1);
        }
    }

    public void DieEnding()
    {
        UIchanging(2);
    }
    public void BacktoHomeEnding()
    {
        UIchanging(3);
    }

}
