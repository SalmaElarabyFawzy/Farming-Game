
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;

namespace  Farm.Inventory.Events
{

    public class InventoryHandSlotHoverEvent : IEvent
    {
        private InventorySlotType slotType;

        public InventoryHandSlotHoverEvent(InventorySlotType slotType)
        {
            this.slotType = slotType;
        }
    }
    
}