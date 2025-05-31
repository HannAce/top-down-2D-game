using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    public List<Item> ItemPrefabs = new();
    private Dictionary<int, GameObject> m_itemDictionary = new();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Auto increment IDs
        for (int i = 0; i < ItemPrefabs.Count; i++)
        {
            if (ItemPrefabs[i] != null)
            {
                // First item in list has ID of 1, then each item goes up by 1
                ItemPrefabs[i].m_itemID = i + 1;
            }
        }

        // each item in list is assigned ID as key, which is equal to that item
        foreach (Item item in ItemPrefabs)
        {
            m_itemDictionary[item.m_itemID] = item.gameObject;
        }
    }

    public GameObject GetItemPrefab(int itemID)
    {
        m_itemDictionary.TryGetValue(itemID, out GameObject itemPrefab);
        if (itemPrefab == null)
        {
            Debug.LogWarning($"Item with ID {itemID} not found");
        }

        return itemPrefab;
    }
}
