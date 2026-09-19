
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Entry;
using Farm.Enums;
using Farm.Inventory.Entry;
using Farm.Inventory.Events;

namespace Farm.Inventory.Model
{
    public class InventoryGridSlotModel : InventorySlotModel
    {
        private int slotIndex;

        public InventoryGridSlotModel(InventorySlotType slotType, int slotIndex, EventSystem eventSystem) : base(slotType, eventSystem)
        {
            this.slotIndex = slotIndex;
        }

        public int GetSlotIndex()
        {
            return slotIndex;
        }

        public override void SetItemEntry(ItemEntry entry)
        {
            base.SetItemEntry(entry);
            eventSystem.Publish(new InventoryGridSlotItemChangedEvent(entry as InventoryEntry, slotIndex, slotType));
        }
    }
}