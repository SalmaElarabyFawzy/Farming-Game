using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;
using UnityEngine;

namespace Farm.Inventory.Events
{
    public class InventoryHandSlotClickedEvent: IEvent
    {
        private InventorySlotType slotType;

        public InventoryHandSlotClickedEvent(InventorySlotType slotType)
        {
            Debug.Log("Hand slot clicked event created with slotType: " + slotType);
            this.slotType = slotType;
        }

        public InventorySlotType SlotType => slotType;
    }
}
