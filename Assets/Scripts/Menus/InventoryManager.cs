using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private ItemDictionary m_itemDictionary;
    [SerializeField] private GameObject m_inventoryPanel;
    [SerializeField] private GameObject m_slotPrefab;
    [SerializeField] private GameObject[] m_itemPrefabs;
    [SerializeField] private int m_slotCount;
    
    public List<InventorySaveData> SaveInventoryItems()
    {
        List<InventorySaveData> inventoryData = new List<InventorySaveData>();

        foreach (Transform slotTransform in m_inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            // If there is an item in this inventory slot
            if (slot.m_currentItem != null)
            {
                Item item = slot.m_currentItem.GetComponent<Item>();
                inventoryData.Add(new InventorySaveData
                {
                    ItemID = item.m_itemID,
                    SlotIndex = slotTransform.GetSiblingIndex()
                });
            }
        }

        return inventoryData;
    }

    public void LoadInventoryItems(List<InventorySaveData> inventoryData)
    {
        // Clear inventoy panel to avoid duplicates

        foreach (Transform child in m_inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }
        
        // Create new slots
        for (int i = 0; i < m_slotCount; i++)
        {
            Instantiate(m_slotPrefab, m_inventoryPanel.transform);
        }
        
        // Populate slots with saved items
        foreach (InventorySaveData data in inventoryData)
        {
            // If it actually fits inside the inventory slots, avoid saving something invalid
            if (data.SlotIndex < m_slotCount)
            {
                Slot slot = m_inventoryPanel.transform.GetChild(data.SlotIndex).GetComponent<Slot>();
                // Get item from the ItemDictionary
                GameObject itemPrefab = m_itemDictionary.GetItemPrefab(data.ItemID);
                if (itemPrefab != null)
                {
                    // Create new item of this item prefab inside ItemDictionary and place in slot
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.m_currentItem = item;
                }
            }
        }
    }
}
