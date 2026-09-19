using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Farm.Enums;

public class InventorySlot : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    private ItemSO currentItem;
    [SerializeField] private Image displayImage;
    [SerializeField] private InventorySlotType slotType;
    private int slotIndex;

    public ItemSO CurrentItem => currentItem;
    protected InventorySlotType SlotType => slotType;

    public void DisplayItem(ItemSO item)
    {
        if(item  == null)
        {
            currentItem = null;
            displayImage.gameObject.SetActive(false);
            return;
        }
        displayImage.gameObject.SetActive(true);
        displayImage.sprite = item.itemIcon;
        currentItem = item;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
       InventoryEvents.OnSlotClicked?.Invoke(slotIndex,currentItem, slotType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryUI.Instance.InfoName.text = currentItem != null ? currentItem.itemName : "";
        InventoryUI.Instance.InfoDescription.text = currentItem != null ? currentItem.description : "";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryUI.Instance.InfoName.text = "";
        InventoryUI.Instance.InfoDescription.text = "";
    }
    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }
    public void SetSlotType(InventorySlotType type)
    {
        slotType = type;
    }
}
