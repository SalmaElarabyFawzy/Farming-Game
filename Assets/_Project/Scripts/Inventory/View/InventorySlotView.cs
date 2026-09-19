using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;
using UnityEngine.UIElements;
using UnityEngine;
using Farm.Inventory.Entry;
using Farm.Entry;

namespace Farm.Inventory.View
{
    public class InventorySlotView : SlotView
    {
        protected InventorySlotType slotType;
        public InventorySlotView(VisualElement root, EventSystem eventSystem, InventorySlotType slotType) : base(root, eventSystem)
        {
            this.slotType = slotType;
            Debug.Log("InventorySlotView created");
        }

        public override void BindInventoryEntry(ItemEntry entry)
        {
            base.BindInventoryEntry(entry);

            if (entry is InventoryEntry inventoryEntry)
                quantityLabel.text = inventoryEntry.Quantity.ToString();
            else
                quantityLabel.text = string.Empty;
        }
    }
}