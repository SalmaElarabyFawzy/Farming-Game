

using UnityEngine.UIElements;

namespace Farm.Inventory.View
{
    public class InventoryDescriptionSectionView
    {
        private Label itemNameLabel;
        private Label itemDescriptionLabel;

        public InventoryDescriptionSectionView(VisualElement root)
        {

            itemNameLabel = root.Q<Label>("itemsName");
            itemDescriptionLabel = root.Q<Label>("description");
        }

        public void UpdateDescription(string itemName, string itemDescription)
        {
            itemNameLabel.text = itemName;
            itemDescriptionLabel.text = itemDescription;
        }
    }
}