using Farm.Enums;
using Farm.Inventory.Entry;
using Farm.Inventory.Factory;
using Farm.Inventory.View;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Farm.Inventory.View
{
    public abstract class InventorySectionView
    {
        protected InventoryHandSlotView handSlotView;
        protected InventoryGridView inventoryGridView;
        protected SlotFactory slotFactory;

        public InventoryGridView InventoryGridView => inventoryGridView;

        public InventorySectionView(VisualElement root, InventorySlotType slotType, SlotFactory slotFactory)
        {
            this.slotFactory = slotFactory;
            var handSlotRoot = root.Q<VisualElement>("handSlot");
            var gridRoot = root.Q<VisualElement>("grid");
            handSlotView = slotFactory.CreateHandSlotView(handSlotRoot, slotType);
            inventoryGridView = new InventoryGridView(gridRoot, slotType, slotFactory);
        }
        public void BindHandSlotEntry(InventoryEntry entry)
        {
            handSlotView.BindInventoryEntry(entry);
        }
        public void BindGridSlotEntry(int slotIndex, InventoryEntry entry)
        {
            inventoryGridView.BindInventoryEntry(slotIndex, entry);
        }

    }
}
