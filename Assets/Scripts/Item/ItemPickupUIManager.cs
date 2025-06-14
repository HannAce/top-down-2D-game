using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickupUIManager : MonoBehaviourSingleton<ItemPickupUIManager>
{
    public GameObject PopupPrefab;
    public int MaxPopups = 3;
    public float PopupDuration = 2f;

    private readonly Queue<GameObject> m_activePopups = new();

    public void ShowItemPickup(string itemName, Sprite itemIcon)
    {
        GameObject newPopup = Instantiate(PopupPrefab, transform);
        newPopup.GetComponentInChildren<TMP_Text>().text = itemName;
        
        // Find the ItemIcon object, ? checks if it's null or not
        Image itemImage = newPopup.transform.Find("ItemIcon")?.GetComponent<Image>();
        if (itemImage)
        {
            itemImage.sprite = itemIcon;
        }
        
        m_activePopups.Enqueue(newPopup);
        if (m_activePopups.Count > MaxPopups)
        {
            Destroy(m_activePopups.Dequeue());
        }
        
        // Fade out and destroy
        StartCoroutine(FadeOutAndDestroy(newPopup)); // can't use nameof?
    }

    private IEnumerator FadeOutAndDestroy(GameObject popup)
    {
        yield return new WaitForSeconds(PopupDuration);
        if (popup == null) yield break;
        
        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        for (float timePassed = 0f; timePassed < 1f; timePassed += Time.deltaTime)
        {
            if (popup == null) yield break;

            canvasGroup.alpha = 1f - timePassed;
            yield return null;
        }
        
        Destroy(popup);
    }
}
