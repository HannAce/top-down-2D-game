using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ItemID;
    public string ItemName;

    public virtual void Pickup()
    {
        Sprite itemIcon = GetComponent<Image>().sprite;
        if (ItemPickupUIManager.Instance != null)
        {
            ItemPickupUIManager.Instance.ShowItemPickup(ItemName, itemIcon);
        }
    }
}
