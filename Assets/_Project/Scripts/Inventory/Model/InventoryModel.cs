using System;
using System.Collections.Generic;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;
using Farm.Inventory.Entry;
using UnityEngine;

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
            SetInventoryItems(InventorySlotType.Item, itemsInitial);
            SetInventoryItems(InventorySlotType.Tool, toolsInitial);

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

        private void SetInventoryItems(InventorySlotType type, List<InventoryEntry> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                SetItemInEmptyInventorySlot(type, items[i]);
            }
        }

        private void SetItemInEmptyInventorySlot(InventorySlotType type, InventoryEntry item)
        {
            int index = GetEmptySlotIndex(type);
            List<InventoryGridSlotModel> targetList = type == InventorySlotType.Item ? items : tools;
            if (index == -1)
            {
                Debug.LogWarning("No empty slot available in the items section.");
                return;
            }
            targetList[index].SetItemEntry(item);
        }
        public void ResetEquipedItemOrTool(InventorySlotType type)
        {
            InventoryEntry equipedEntry = type == InventorySlotType.Item ? equipedItem.GetItemEntry() as InventoryEntry : equipedTool.GetItemEntry() as InventoryEntry;
            SetItemInEmptyInventorySlot(type, equipedEntry);
        }
        public void SetEquipedItemOrTool(int index, InventorySlotType type)
        {
            List<InventoryGridSlotModel> targetList = type == InventorySlotType.Item ? items : tools;
            InventoryHandSlotModel equipedEntry = type == InventorySlotType.Item ? equipedItem : equipedTool;

            InventoryEntry prevEquipedItem = equipedEntry.GetItemEntry() as InventoryEntry;
            InventoryEntry item = targetList[index].GetItemEntry() as InventoryEntry;

            equipedEntry.SetItemEntry(item);
            targetList[index].SetItemEntry(prevEquipedItem);
        }
        private int GetEmptySlotIndex(InventorySlotType type)
        {
            List<InventoryGridSlotModel> targetList = type == InventorySlotType.Item ? items : tools;

            for (int i = 0; i < targetList.Count; i++)
            {
                if (targetList[i].GetItemEntry() == null)
                    return i;
            }
            return -1;
        }
        public string GetItemOrToolDescription(InventorySlotType type, int index)
        {
            if(InventorySlotType.None == type)
            {
                Debug.LogError($"Invalid InventorySlotType: {type}");
                return string.Empty;
            }
            List<InventoryGridSlotModel> targetList = type == InventorySlotType.Item ? items : tools;
            return targetList[index].GetItemEntry()?.GetItem()?.description ?? string.Empty;
        }

        public string GetItemOrToolName(InventorySlotType type, int index)
        {
            if(InventorySlotType.None == type)
            {
                Debug.LogError($"Invalid InventorySlotType: {type}");
                return string.Empty;
            }
            List<InventoryGridSlotModel> targetList = type == InventorySlotType.Item ? items : tools;
            return targetList[index].GetItemEntry()?.GetItem()?.itemName ?? string.Empty;
        }

        public string GetHandSlotItemOrToolDescription(InventorySlotType type)
        {
            if(InventorySlotType.None == type)
            {
                Debug.LogError($"Invalid InventorySlotType: {type}");
                return string.Empty;
            }
            
            InventoryHandSlotModel equipedEntry = type == InventorySlotType.Item ? equipedItem : equipedTool;
            return equipedEntry.GetItemEntry()?.GetItem()?.description ?? string.Empty;
        }

        public string GetHandSlotItemOrToolName(InventorySlotType type)
        {
            if(InventorySlotType.None == type)
            {
                Debug.LogError($"Invalid InventorySlotType: {type}");
                return string.Empty;
            }
            InventoryHandSlotModel equipedEntry = type == InventorySlotType.Item ? equipedItem : equipedTool;
            return equipedEntry.GetItemEntry()?.GetItem()?.itemName ?? string.Empty;
        }

        public ItemSO GetEquipedTool()
        {
            return equipedTool.GetItemEntry()?.GetItem();
        }
        public void AddItemToInventory(ItemSO item, InventorySlotType slotType)
        {
            if (SearchForSimilarItemInInventoryAndUpdateQuantity(item, slotType))
                return;

            InventoryEntry newEntry = new InventoryEntry(item, 1);
            SetItemInEmptyInventorySlot(slotType, newEntry);
        }
        private bool SearchForSimilarItemInInventoryAndUpdateQuantity(ItemSO item, InventorySlotType slotType)
        {
            List<InventoryGridSlotModel> targetList = slotType == InventorySlotType.Item ? items : tools;

            for (int i = 0; i < targetList.Count; i++)
            {
                InventoryEntry entry = targetList[i].GetItemEntry() as InventoryEntry;
                if (entry != null && entry.GetItem() == item)
                {
                    entry.UpdateQuantity(entry.Quantity + 1);
                    return true;
                }
            }
            return false;
        }
    }
}