using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform m_originalParent;
    [SerializeField] private CanvasGroup m_canvasGroup;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // save OG parent
        m_originalParent = transform.parent;
        // Above other canvas'
        transform.SetParent(transform.root); 
        // block raycast, cannot click on other items while dragging
        m_canvasGroup.blocksRaycasts = false;
        // Make semi-transparent during drag
        m_canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // follow mouse
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // enable raycast, can click on again
        m_canvasGroup.blocksRaycasts = true;
        // no longer transparent
        m_canvasGroup.alpha = 1f;
        
        // Slot where item dropped
        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>();
        if (dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if (dropItem != null)
            {
                dropSlot = dropItem.GetComponentInParent<Slot>();
            }
        }
        Slot originalSlot = m_originalParent.GetComponent<Slot>();

        if (dropSlot != null)
        {
            if (dropSlot.m_currentItem != null)
            {
                // if slot has an item, swap item
                dropSlot.m_currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.m_currentItem = dropSlot.m_currentItem;
                dropSlot.m_currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                originalSlot.m_currentItem = null;
            }
            
            // Move item into drop slot
            transform.SetParent(dropSlot.transform);
            dropSlot.m_currentItem = gameObject;
        }
        else
        {
            // No slot under drop point, return to original slot
            transform.SetParent(m_originalParent);
        }
        
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // centre inside slot
    }
}
