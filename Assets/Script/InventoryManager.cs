using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public ItemBarUI itemBar; // ✅ ใช้อ้างอิงแทน InventoryUI

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(StatusData newItem,GameObject objectItem)
    {
        itemBar.AddItem(newItem,objectItem);
    }
}
