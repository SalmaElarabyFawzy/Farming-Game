using System;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;
using Farm.Inventory.Events;
using Farm.Inventory.Model;
using Farm.Inventory.View;
using VContainer.Unity;

namespace Farm.Inventory.Presenter
{
    public class InventoryPresenter : IStartable, IInitializable, IDisposable
    {

        private readonly EventSystem eventSystem;
        private InventoryModel inventoryModel;
        private InventoryView inventoryView;

        public InventoryPresenter(InventoryModel inventoryModel, InventoryView inventoryView, EventSystem eventSystem)
        {
            this.inventoryModel = inventoryModel;
            this.inventoryView = inventoryView;
            this.eventSystem = eventSystem;
        }

        public void Initialize()
        {
            eventSystem.Subscribe<InventoryGridSlotItemChangedEvent>(OnInventoryGridSlotItemChanged);
            eventSystem.Subscribe<InventoryHandItemChangedEvent>(OnInventoryHandItemChanged);
            eventSystem.Subscribe<InventoryGridSlotClickedEvent>(OnInventoryGridSlotClicked);
            eventSystem.Subscribe<InventoryHandSlotClickedEvent>(OnInventoryHandSlotClicked);

        }
        public void Dispose()
        {
            eventSystem.Unsubscribe<InventoryGridSlotItemChangedEvent>(OnInventoryGridSlotItemChanged);
            eventSystem.Unsubscribe<InventoryGridSlotClickedEvent>(OnInventoryGridSlotClicked);
            eventSystem.Unsubscribe<InventoryHandItemChangedEvent>(OnInventoryHandItemChanged);
            eventSystem.Unsubscribe<InventoryHandSlotClickedEvent>(OnInventoryHandSlotClicked);
        }
        public void Start()
        {
            inventoryView.Build();
            inventoryModel.Build();
        }


        private void OnInventoryHandItemChanged(InventoryHandItemChangedEvent eventData)
        {
            switch (eventData.InventoryType)
            {
                case InventorySlotType.Item:
                    inventoryView.BindItemsSectionHandSlotEntry(eventData.ItemEntry);
                    break;
                case InventorySlotType.Tool:
                    inventoryView.BindToolsSectionHandSlotEntry(eventData.ItemEntry);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnInventoryGridSlotItemChanged(InventoryGridSlotItemChangedEvent eventData)
        {
            switch (eventData.SlotType)
            {
                case InventorySlotType.Item:
                    inventoryView.BindItemsSectionSlotEntry(eventData.SlotIndex, eventData.ItemEntry);
                    break;
                case InventorySlotType.Tool:
                    inventoryView.BindToolsSectionSlotEntry(eventData.SlotIndex, eventData.ItemEntry);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnInventoryGridSlotClicked(InventoryGridSlotClickedEvent eventData)
        {
            inventoryModel.SetEquipedItemOrTool(eventData.SlotIndex, eventData.SlotType);
        }

        private void OnInventoryHandSlotClicked(InventoryHandSlotClickedEvent eventData)
        {
            inventoryModel.ResetEquipedItemOrTool(eventData.SlotType);
        }

    }
}
