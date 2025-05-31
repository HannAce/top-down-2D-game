using System;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    [SerializeField] private InventoryManager m_inventoryManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Item item = other.gameObject.GetComponent<Item>();
            if (item != null)
            {
                // Add item to inventory
                bool itemAdded = m_inventoryManager.AddItem(other.gameObject);

                if (itemAdded)
                {
                    Destroy(other.gameObject);
                    Debug.Log("Item collected");
                }
            }
        }
    }
}
