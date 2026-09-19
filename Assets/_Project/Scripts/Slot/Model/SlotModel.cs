

using Farm.Core.DesignPatterns.EventSystem;
using Farm.Entry;

namespace Farm.Slot.Model
{
    public abstract class SlotModel
    {
        private ItemEntry itemEntry;
        protected EventSystem eventSystem;

        public SlotModel(EventSystem eventSystem)
        {
            this.eventSystem = eventSystem;
            itemEntry = null;   
        }


        public virtual void SetItemEntry(ItemEntry entry)
        {
            itemEntry = entry;
        }

        public virtual ItemEntry GetItemEntry()
        {
            return itemEntry;
        }
    }
}