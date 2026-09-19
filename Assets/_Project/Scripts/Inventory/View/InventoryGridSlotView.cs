using Farm.Enums;
using UnityEngine.UIElements;
using UnityEngine;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Inventory.Events;

namespace Farm.Inventory.View
{
    public class InventoryGridSlotView : InventorySlotView
    {
        private int slotIndex;
        public InventoryGridSlotView(VisualElement root, int slotIndex, InventorySlotType slotType, EventSystem eventSystem) : base(root, eventSystem, slotType)
        {
            this.slotIndex = slotIndex;
            Debug.Log("InventoryGridSlotView created");
        }


        protected override void onSlotHover(MouseEnterEvent evt)
        {
            root.AddToClassList("itemSlot--hover");
            eventSystem.Publish(new InventoryGridSlotHoverEvent(slotIndex, slotType));
        }

        protected override void onSlotClick(ClickEvent evt)
        {
            eventSystem.Publish(new InventoryGridSlotClickedEvent(slotIndex, slotType));
        }

    }
}
