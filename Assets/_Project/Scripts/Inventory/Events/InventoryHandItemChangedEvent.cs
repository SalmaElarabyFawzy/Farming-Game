

using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;
using Farm.Inventory.Entry;

namespace Farm.Inventory.Events
{
    public class InventoryHandItemChangedEvent : IEvent
    {
        private readonly InventoryEntry itemEntry;
        private readonly InventorySlotType inventoryType;

        public InventoryHandItemChangedEvent(InventoryEntry itemEntry, InventorySlotType inventoryType)
        {
            this.itemEntry = itemEntry;
            this.inventoryType = inventoryType;
        }

        public InventoryEntry ItemEntry => itemEntry;
        public InventorySlotType InventoryType => inventoryType;
    }

}