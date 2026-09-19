using VContainer;
using VContainer.Unity;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Inventory.Factory;
using Farm.Inventory.View;
using UnityEngine.UIElements;
using UnityEngine;
using Farm.Inventory.Presenter;
using Farm.Inventory.Model;
using Farm.Inventory.Entry;
using System.Collections.Generic;

namespace Farm.Core.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private UIDocument uiDocument;
        [SerializeField] private List<InventoryEntry> inventoryInitialItems;
        [SerializeField] private List<InventoryEntry> inventoryInitialTools;
        [SerializeField] private int inventoryItemsCount;
        [SerializeField] private int inventoryToolsCount;


        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<EventSystem>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<SlotFactory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            // Register Components
            builder.RegisterComponent(uiDocument);
            //Inventory
            builder.Register<InventoryModel>(resolver => new InventoryModel(inventoryItemsCount, inventoryToolsCount, inventoryInitialItems, inventoryInitialTools, resolver.Resolve<EventSystem>()), Lifetime.Singleton).AsSelf();
            builder.Register<InventoryView>(resolver => new InventoryView(uiDocument, resolver.Resolve<SlotFactory>()), Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<InventoryPresenter>(Lifetime.Singleton);

        }
    }
}
