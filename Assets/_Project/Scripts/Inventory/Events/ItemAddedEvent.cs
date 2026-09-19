using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;
using UnityEngine;

namespace Farm.Inventory.Events
{
    public class ItemAddedEvent : IEvent
    {
        private int itemIndex;
        private ItemSO item;
        private InventorySlotType slotType;

        public ItemAddedEvent(int itemIndex, ItemSO item, InventorySlotType slotType)
        {
            this.itemIndex = itemIndex;
            this.item = item;
            this.slotType = slotType;
        }
    
    }
}
