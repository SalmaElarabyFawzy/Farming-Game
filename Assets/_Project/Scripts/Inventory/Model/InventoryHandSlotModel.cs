using Farm.Core.DesignPatterns.EventSystem;
using Farm.Entry;
using Farm.Enums;
using Farm.Inventory.Entry;
using Farm.Inventory.Events;

namespace Farm.Inventory.Model
{
    public class InventoryHandSlotModel : InventorySlotModel
    {
        public InventoryHandSlotModel(InventorySlotType slotType, EventSystem eventSystem) : base(slotType, eventSystem)
        {
        }

        public override void SetItemEntry(ItemEntry entry)
        {
            base.SetItemEntry(entry);
            eventSystem.Publish(new InventoryHandItemChangedEvent(entry as InventoryEntry, slotType));
        }
    }
}