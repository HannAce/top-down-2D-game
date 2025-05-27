using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject m_slotPrefab;
    [SerializeField] private GameObject[] m_itemPrefabs;
    [SerializeField] private GameObject m_inventoryPanel;
    [SerializeField] private int m_slotCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < m_slotCount; i++)
        {
            Slot slot = Instantiate(m_slotPrefab, m_inventoryPanel.transform).GetComponent<Slot>();
            //Debug.Log("Inventory slots instantiated!");
            if (i < m_itemPrefabs.Length)
            {
                GameObject item = Instantiate(m_itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.m_currentItem = item;
            }
        }
    }
}
