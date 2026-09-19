using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;
using Farm.Inventory.Entry;

namespace Farm.Inventory.Events
{
    public class InventoryGridSlotItemChangedEvent : IEvent
    {
        private InventoryEntry itemEntry;
        private InventorySlotType slotType;
        private int slotIndex;

        public InventoryGridSlotItemChangedEvent(InventoryEntry itemEntry, int slotIndex, InventorySlotType slotType)
        {
            this.itemEntry = itemEntry;
            this.slotType = slotType;
            this.slotIndex = slotIndex;
        }


        public InventoryEntry ItemEntry => itemEntry;
        public InventorySlotType SlotType => slotType;
        public int SlotIndex => slotIndex;
    }
}