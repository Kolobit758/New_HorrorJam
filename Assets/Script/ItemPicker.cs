using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public StatusData itemData; // ข้อมูลไอเท็มจาก ScriptableObject

    private void OnMouseDown()
    {
        // ✅ คลิกซ้ายเพื่อเก็บของ
        if (itemData != null)
        {
            Debug.Log("เก็บของ: " + itemData.objectName);

            // เพิ่มของเข้าระบบ Inventory
            InventoryManager.Instance.AddItem(itemData);

            // ทำให้ของในฉากหายไป
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning($"ItemPickup: {gameObject.name} ไม่มี itemData อ้างอิง!");
        }
    }
}
