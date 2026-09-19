

using Farm.Enums;
using Farm.Inventory.View;
using UnityEngine.UIElements;

namespace Farm.Inventory.Factory
{

    public interface ISlotFactory
    {
        InventoryGridSlotView CreateGridSlotView(VisualElement slotRoot, InventorySlotType inventoryType, int index);
        InventoryHandSlotView CreateHandSlotView(VisualElement slotRoot, InventorySlotType inventoryType);
    }
}