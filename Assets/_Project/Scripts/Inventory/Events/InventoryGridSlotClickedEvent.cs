
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;
using UnityEngine;

namespace Farm.Inventory.Events
{
    public class InventoryGridSlotClickedEvent : IEvent
    {
        private int slotIndex;
        private InventorySlotType slotType;

        public InventoryGridSlotClickedEvent(int slotIndex, InventorySlotType slotType)
        {
            Debug.Log($"InventoryGridSlotClickedEvent created with slotIndex: {slotIndex}, slotType: {slotType}");
            this.slotIndex = slotIndex;
            this.slotType = slotType;
        }

        public int SlotIndex => slotIndex;
        public InventorySlotType SlotType => slotType;
    }
}