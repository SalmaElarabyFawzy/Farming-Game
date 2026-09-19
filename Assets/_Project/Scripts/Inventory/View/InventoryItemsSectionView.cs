using UnityEngine;
using Farm.Enums;
using Farm.Inventory.Factory;
using UnityEngine.UIElements;

namespace Farm.Inventory.View
{
    public class InventoryItemsSectionView: InventorySectionView
    {
        public InventoryItemsSectionView(VisualElement root, SlotFactory slotFactory) : base(root, InventorySlotType.Item, slotFactory)
        {
            Debug.Log("InventoryItemsSectionView created");
        }

    }
}