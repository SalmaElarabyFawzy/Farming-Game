using UnityEngine;
using Farm.Enums;
using Farm.Inventory.Factory;
using UnityEngine.UIElements;

namespace Farm.Inventory.View
{
    public class InventoryToolsSectionView : InventorySectionView
    {
        public InventoryToolsSectionView(VisualElement root, SlotFactory slotFactory) : base(root,InventorySlotType.Tool, slotFactory)
        {
            Debug.Log("InventoryToolsSectionView created");
        }
    }
}