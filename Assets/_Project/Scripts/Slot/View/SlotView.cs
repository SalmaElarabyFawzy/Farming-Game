using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;
using Farm.Inventory.Entry;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using Farm.Inventory.Events;
using Farm.Entry;

namespace Farm.Inventory.View
{
    public abstract class SlotView
    {
        protected VisualElement root;
        protected VisualElement icon;
        protected Label quantityLabel;
        protected VisualElement selectionBorder;

        const string IconElementName = "icon";
        const string QuantityLabelElementName = "quantityLabel";
        const string SelectionBorderElementName = "selectionBorder";

        protected EventSystem eventSystem;

        public SlotView(VisualElement root, EventSystem eventSystem)
        {
            this.root = root;
            this.eventSystem = eventSystem;
            icon = root.Q<VisualElement>(IconElementName);
            quantityLabel = root.Q<Label>(QuantityLabelElementName);
            selectionBorder = root.Q<VisualElement>(SelectionBorderElementName);

            root.RegisterCallback<MouseEnterEvent>(_ => onSlotHover(null));
            root.RegisterCallback<MouseLeaveEvent>(_ => onSlotLeave(null));
            root.RegisterCallback<ClickEvent>(_ => onSlotClick(null));
        }

        protected virtual void onSlotHover(MouseEnterEvent evt)
        {
            root.AddToClassList("itemSlot--hover");

            eventSystem.Publish(new SlotHoverEvent());

        }
        protected virtual void onSlotLeave(MouseLeaveEvent evt)
        {
            root.RemoveFromClassList("itemSlot--hover");
            eventSystem.Publish(new SlotHoverEndedEvent());
        }
        protected virtual void onSlotClick(ClickEvent evt)
        {
            eventSystem.Publish(new SlotClickedEvent());
        }

        public virtual void BindInventoryEntry(ItemEntry entry)
        {
            if (entry != null && entry.Item != null)
                icon.style.backgroundImage = new StyleBackground(entry.Item.itemIcon);
            else
                icon.style.backgroundImage = null;

            quantityLabel.text = "";
        }

        public void SetSelected(bool isSelected)
        {
            selectionBorder.style.display = isSelected ? DisplayStyle.Flex : DisplayStyle.None;
        }

    }
}
