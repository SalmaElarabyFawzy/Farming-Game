using Farm.Core.DesignPatterns.EventSystem;
using Farm.Entry;
using Farm.Enums;
using Farm.Slot.Model;

namespace Farm.Inventory.Model
{
    public class InventorySlotModel : SlotModel
    {
        protected InventorySlotType slotType;

        public InventorySlotModel(InventorySlotType slotType, EventSystem eventSystem): base(eventSystem)
        {
            this.slotType = slotType;
        }
        public override void SetItemEntry(ItemEntry entry)
        {
            base.SetItemEntry(entry);
        }

    }
}