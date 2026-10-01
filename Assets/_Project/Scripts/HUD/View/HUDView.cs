using System;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.HUD.Events;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer.Unity;

namespace Farm.HUD.View
{
    public class HUDView : IStartable, IDisposable
    {

        private readonly UIDocument _uiDocument;
        private readonly EventSystem _eventSystem;

        private Button _inventoryButton;

        public HUDView(EventSystem eventSystem, UIDocument uiDocument)
        {
            _eventSystem = eventSystem;
            _uiDocument = uiDocument;
        }

        public void Start()
        {
            Build();
        }

        public void Dispose()
        {
            _inventoryButton.clicked -= OnInventoryButtonClicked;
        }

        private void Build()
        {

            _inventoryButton = _uiDocument.rootVisualElement.Q<Button>("inventory-toggle-button");

            if (_inventoryButton == null)
            {
                Debug.LogError("Inventory button not found in the HUD UI.");
                return;
            }
            _inventoryButton.clicked += OnInventoryButtonClicked;
        }

        private void OnInventoryButtonClicked()
        {
            _eventSystem.Publish(new InventoryButtonClickedEvent());
        }
    }
}
