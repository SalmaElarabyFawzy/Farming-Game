using UnityEngine;
using Farm.Enums;
using Farm.Inventory.Factory;
using UnityEngine.UIElements;

namespace Farm.Inventory.View
{
    public class InventoryItemsSectionView: InventorySectionView
    {
        public InventoryItemsSectionView(VisualElement root, SlotFactory slotFactory) : base(root, slotFactory)
        {
            Debug.Log("InventoryItemsSectionView created");
            var handSlotRoot = root.Q<VisualElement>("handSlot");
            var gridRoot = root.Q<VisualElement>("grid");
            handSlotView = slotFactory.CreateHandSlotView(handSlotRoot, InventorySlotType.Item);
            inventoryGridView = new InventoryGridView(gridRoot, InventorySlotType.Item, slotFactory);
        }

    }
}