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
using Farm.Player.View;
using Farm.Player.Model;
using Farm.Player.Commands;
using Farm.Player.Presenter;

namespace Farm.Core.DI
{
    public class GameLifetimeScope : LifetimeScope
    {

        [Header("Inventory")]
        [SerializeField] private UIDocument uiDocument;
        [SerializeField] private List<InventoryEntry> inventoryInitialItems;
        [SerializeField] private List<InventoryEntry> inventoryInitialTools;
        [SerializeField] private int inventoryItemsCount;
        [SerializeField] private int inventoryToolsCount;


        [Header("Player")]
        [SerializeField] private PlayerInteractionsView playerInteractionsView;
        [SerializeField] private PlayerCutScenesView playerCutScenesView;
        [SerializeField] private PlayerInputHandlerView playerInputHandlerView;
        [SerializeField] private PlayerControllerView playerControllerView;


        protected override void Configure(IContainerBuilder builder)
        {
            // Register Services
            builder.Register<EventSystem>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<SlotFactory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            // Register Player
            builder.RegisterComponent(playerInteractionsView);
            builder.RegisterComponent(playerCutScenesView);
            builder.RegisterComponent(playerInputHandlerView);
            builder.RegisterComponent(playerControllerView);
            builder.RegisterEntryPoint<PlayerInteractionsPresenter>(Lifetime.Singleton).AsSelf();
            builder.Register<PlayerInteractionsModel>(resolver => new PlayerInteractionsModel(resolver.Resolve<EventSystem>()), Lifetime.Singleton).AsSelf();
            builder.Register<PlayerControllerModel>(resolver => new PlayerControllerModel(resolver.Resolve<EventSystem>()), Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<PlayerControllerPresenter>(Lifetime.Singleton).AsSelf();
            builder.Register<PlayerCutScenesCommandsDispatcher>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            //Player Command Registration
            builder.Register<PlayerHarvestCutSceneCommand>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<PlayerPlantCutSceneCommand>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<PlayerWaterCutSceneCommand>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<PlayerPlowCutSceneCommand>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            //Inventory
            builder.RegisterComponent(uiDocument);
            builder.Register<InventoryModel>(resolver => new InventoryModel(inventoryItemsCount, inventoryToolsCount, inventoryInitialItems, inventoryInitialTools, resolver.Resolve<EventSystem>()), Lifetime.Singleton).AsSelf();
            builder.Register<InventoryView>(resolver => new InventoryView(uiDocument, resolver.Resolve<SlotFactory>()), Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<InventoryPresenter>(Lifetime.Singleton).AsSelf();

        }
    }
}
