using System.Collections.Generic;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;
using Farm.Inventory.Entry;

namespace Farm.Inventory.Model
{
    public class InventoryModel
    {
        private List<InventoryGridSlotModel> items;
        private List<InventoryGridSlotModel> tools;
        private InventoryHandSlotModel equipedItem;
        private InventoryHandSlotModel equipedTool;

        private int inventoryItemsCount;
        private int inventoryToolsCount;
        private List<InventoryEntry> itemsInitial;
        private List<InventoryEntry> toolsInitial;

        private EventSystem eventSystem;

        public InventoryHandSlotModel EquipedItem => equipedItem;
        public InventoryHandSlotModel EquipedTool => equipedTool;

        public InventoryModel(int inventoryItemsCount, int inventoryToolsCount, List<InventoryEntry> items, List<InventoryEntry> tools, EventSystem eventSystem)
        {
            this.inventoryItemsCount = inventoryItemsCount;
            this.inventoryToolsCount = inventoryToolsCount;
            itemsInitial = items;
            toolsInitial = tools;
            this.eventSystem = eventSystem;
        }

        public void Build()
        {
            InitializeInventoryItemsSlots(inventoryItemsCount);
            InitializeInventoryToolsSlots(inventoryToolsCount);
            SetInventoryItems(itemsInitial);
            SetInventoryTools(toolsInitial);

            equipedItem = new InventoryHandSlotModel(InventorySlotType.Item, eventSystem);
            equipedTool = new InventoryHandSlotModel(InventorySlotType.Tool, eventSystem);
        }

        private void InitializeInventoryItemsSlots(int initialItemsCount)
        {
            items = new List<InventoryGridSlotModel>(initialItemsCount);
            for (int i = 0; i < initialItemsCount; i++)
            {
                items.Add(new InventoryGridSlotModel(InventorySlotType.Item, i, eventSystem));
            }
        }

        private void InitializeInventoryToolsSlots(int initialToolsCount)
        {
            tools = new List<InventoryGridSlotModel>(initialToolsCount);
            for (int i = 0; i < initialToolsCount; i++)
            {
                tools.Add(new InventoryGridSlotModel(InventorySlotType.Tool, i, eventSystem));
            }
        }

        private void SetInventoryTools(List<InventoryEntry> tools)
        {
            for (int i = 0; i < tools.Count; i++)
            {
                SetToolInInventory(tools[i]);
            }
        }

        public void SetToolInInventory(InventoryEntry tool)
        {
            for (int i = 0; i < tools.Count; i++)
            {
                if (tools[i].GetItemEntry() == null)
                {
                    tools[i].SetItemEntry(tool);
                    break;
                }
            }
        }

        private void SetInventoryItems(List<InventoryEntry> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                SetItemInInventory(items[i]);
            }
        }

        public void SetItemInInventory(InventoryEntry item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetItemEntry() == null)
                {
                    items[i].SetItemEntry(item);
                    break;
                }
            }
        }

        public void ResetEquipedItemOrTool(InventorySlotType type)
        {
            if (type == InventorySlotType.Item)
                ResetEquipedItem();
            else if (type == InventorySlotType.Tool)
                ResetEquipedTool();
        }
        public void SetEquipedItemOrTool(int index, InventorySlotType type)
        {

            if (type == InventorySlotType.Item)
                SetEquipedItem(index);
            else if (type == InventorySlotType.Tool)
                SetEquipedTool(index);

        }
        private void ResetEquipedItem()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetItemEntry() == null)
                {
                    items[i].SetItemEntry(equipedItem.GetItemEntry());
                    equipedItem.SetItemEntry(null);
                    break;
                }
            }
        }
        private void ResetEquipedTool()
        {
            for (int i = 0; i < tools.Count; i++)
            {
                if (tools[i].GetItemEntry() == null)
                {
                    tools[i].SetItemEntry(equipedTool.GetItemEntry());
                    equipedTool.SetItemEntry(null);
                    break;
                }
            }
        }
        private void SetEquipedItem(int index)
        {
            InventoryEntry prevEquipedItem = equipedItem.GetItemEntry() as InventoryEntry;
            InventoryEntry item = items[index].GetItemEntry() as InventoryEntry;
            equipedItem.SetItemEntry(item);
            items[index].SetItemEntry(prevEquipedItem);
        }
        private void SetEquipedTool(int index)
        {
            InventoryEntry prevEquipedTool = equipedTool.GetItemEntry() as InventoryEntry;
            InventoryEntry tool = tools[index].GetItemEntry() as InventoryEntry;
            equipedTool.SetItemEntry(tool);
            tools[index].SetItemEntry(prevEquipedTool);
        }

    }
}