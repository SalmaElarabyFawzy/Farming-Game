using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;
using UnityEngine;

namespace Farm.Inventory.Events
{
    public class InventoryGridSlotHoverEvent :IEvent
    {
        private int slotIndex;
        private InventorySlotType slotType;

        public InventoryGridSlotHoverEvent(int slotIndex, InventorySlotType slotType)
        {
            this.slotIndex = slotIndex;
            this.slotType = slotType;
        }
    }
}
