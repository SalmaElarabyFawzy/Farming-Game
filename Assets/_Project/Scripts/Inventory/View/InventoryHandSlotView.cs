using Farm.Enums;
using Farm.Inventory.Events;
using UnityEngine.UIElements;
using UnityEngine;
using Farm.Core.DesignPatterns.EventSystem;

namespace Farm.Inventory.View
{
    public class InventoryHandSlotView : InventorySlotView
    {
        public InventoryHandSlotView(VisualElement root, InventorySlotType slotType, EventSystem eventSystem) : base(root,eventSystem, slotType)
        {
            Debug.Log("InventoryHandSlotView created");
            root.RegisterCallback<ClickEvent>(_ => onSlotClick(null));
        }


        protected override void onSlotHover(MouseEnterEvent evt)
        {
            root.AddToClassList("itemSlot--hover");
            eventSystem.Publish(new InventoryHandSlotHoverEvent(slotType));
        }

        protected override void onSlotClick(ClickEvent evt)
        {
            eventSystem.Publish(new InventoryHandSlotClickedEvent(slotType));
        }
    }

}
