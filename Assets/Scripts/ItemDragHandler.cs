using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform m_originalParent;
    [SerializeField] private CanvasGroup m_canvasGroup;

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_originalParent = transform.parent; // save OG parent
        transform.SetParent(transform.root); // Above other canvas'
        m_canvasGroup.blocksRaycasts = false;
        m_canvasGroup.alpha = 0.6f; // Make semi-transparent during drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // follow mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_canvasGroup.blocksRaycasts = true; // enable raycast, can click on again
        m_canvasGroup.alpha = 1f; // no longer transparent
        
        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>(); // Slot where item dropped
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
                // dropSlot has an item, swap item
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
            // No slot under drop point
            transform.SetParent(m_originalParent);
        }
        
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // centre inside slot
    }
}
