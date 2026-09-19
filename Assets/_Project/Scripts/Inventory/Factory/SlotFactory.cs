using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;
using Farm.Inventory.View;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Farm.Inventory.Factory
{
    public class SlotFactory : ISlotFactory
    {
        [Inject]
        private EventSystem eventSystem;
    
       public InventoryGridSlotView CreateGridSlotView(VisualElement slotRoot, InventorySlotType inventoryType, int index)
        {
            Debug.Log("SlotFactory: CreateGridSlotView called");
            var slotView = new InventoryGridSlotView(slotRoot, index,inventoryType, eventSystem);
            return slotView;
        }

        public InventoryHandSlotView CreateHandSlotView(VisualElement slotRoot, InventorySlotType inventoryType)
        {
            Debug.Log("SlotFactory: CreateHandSlotView called");
            var slotView = new InventoryHandSlotView(slotRoot, inventoryType, eventSystem);
            return slotView;
        }
      
    }
}
