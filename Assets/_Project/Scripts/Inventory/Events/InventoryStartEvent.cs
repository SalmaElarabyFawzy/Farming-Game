using UnityEngine;
using Farm.Core.DesignPatterns.EventSystem;

namespace Farm.Inventory.Events
{
    public class InventoryStartEvent : IEvent
    {
        private ItemSO toolSO;
        private ItemSO itemSO;

        public InventoryStartEvent(ItemSO toolSO, ItemSO itemSO)
        {
            this.toolSO = toolSO;
            this.itemSO = itemSO;
        }
    }
}
