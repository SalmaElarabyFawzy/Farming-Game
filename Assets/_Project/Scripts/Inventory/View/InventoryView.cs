using Farm.Inventory.Entry;
using Farm.Inventory.Factory;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer.Unity;

namespace Farm.Inventory.View
{
    public class InventoryView
    {

        private UIDocument uiDocument;
        private InventoryToolsSectionView toolsSectionView;
        private InventoryItemsSectionView itemsSectionView;

        private InventoryDescriptionSectionView descriptionSectionView;

        private readonly SlotFactory slotFactory;

        public InventoryView(UIDocument uiDocument, SlotFactory slotFactory)
        {

            this.uiDocument = uiDocument;
            this.slotFactory = slotFactory;

        }

        public void Build()
        {
            Debug.Log("InventoryView started");

            var toolsSectionRoot = uiDocument.rootVisualElement.Q<VisualElement>("tootlsPanel");
            var itemsSectionRoot = uiDocument.rootVisualElement.Q<VisualElement>("itemsPanel");
            var descriptionSectionRoot = uiDocument.rootVisualElement.Q<VisualElement>("InfoPanel");

            toolsSectionView = new InventoryToolsSectionView(toolsSectionRoot, slotFactory);
            itemsSectionView = new InventoryItemsSectionView(itemsSectionRoot, slotFactory);
            descriptionSectionView = new InventoryDescriptionSectionView(descriptionSectionRoot);
        }

        public void BindToolsSectionSlotEntry(int slotIndex, InventoryEntry entry)
        {
            toolsSectionView.BindGridSlotEntry(slotIndex, entry);
        }

        public void BindItemsSectionSlotEntry(int slotIndex, InventoryEntry entry)
        {
            itemsSectionView.BindGridSlotEntry(slotIndex, entry);
        }
        public void BindToolsSectionHandSlotEntry(InventoryEntry entry)
        {
            toolsSectionView.BindHandSlotEntry(entry);
        }
        public void BindItemsSectionHandSlotEntry(InventoryEntry entry)
        {
            itemsSectionView.BindHandSlotEntry(entry);
        }

        public void UpdateDescription(string itemName, string itemDescription)
        {
            descriptionSectionView.UpdateDescription(itemName, itemDescription);
        }

    }
}
