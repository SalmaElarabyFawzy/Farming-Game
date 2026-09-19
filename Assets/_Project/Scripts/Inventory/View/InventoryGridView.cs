using System.Collections.Generic;
using Farm.Enums;
using Farm.Inventory.Factory;
using UnityEngine.UIElements;
using UnityEngine;
using Farm.Inventory.Entry;

namespace Farm.Inventory.View
{
    public class InventoryGridView
    {

        private VisualElement gridRoot;

        private List<InventoryGridSlotView> gridSlotViews = new List<InventoryGridSlotView>();
        public List<InventoryGridSlotView> GridSlotViews => gridSlotViews;
        private ISlotFactory slotFactory;
        public InventoryGridView(VisualElement root, InventorySlotType inventoryType, ISlotFactory slotFactory)
        {
            Debug.Log("InventoryGridView created");
            gridRoot = root;
            this.slotFactory = slotFactory;
            for (int i = 0; i < gridRoot.childCount; i++)
            {
                var slotRoot = gridRoot.ElementAt(i);

                var itemSlotView = slotFactory.CreateGridSlotView(slotRoot, inventoryType, i);
                gridSlotViews.Add(itemSlotView);
            }

        }

        public void BindInventoryEntry(int slotIndex, InventoryEntry entry)
        {
            if (slotIndex >= 0 && slotIndex < gridSlotViews.Count)
            {
                gridSlotViews[slotIndex].BindInventoryEntry(entry);
            }
            else
            {
                Debug.LogWarning($"Invalid slot index: {slotIndex}");
            }
        }
    }
}
