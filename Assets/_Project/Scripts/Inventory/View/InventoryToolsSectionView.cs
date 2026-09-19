using UnityEngine;
using Farm.Enums;
using Farm.Inventory.Factory;
using UnityEngine.UIElements;

namespace Farm.Inventory.View
{
    public class InventoryToolsSectionView : InventorySectionView
    {
        public InventoryToolsSectionView(VisualElement root, SlotFactory slotFactory) : base(root, slotFactory)
        {
            Debug.Log("InventoryToolsSectionView created");
            var handSlotRoot = root.Q<VisualElement>("handSlot");
            var gridRoot = root.Q<VisualElement>("grid");
            handSlotView = slotFactory.CreateHandSlotView(handSlotRoot, InventorySlotType.Tool);
            inventoryGridView = new InventoryGridView(gridRoot, InventorySlotType.Tool, slotFactory);
        }
    }
}