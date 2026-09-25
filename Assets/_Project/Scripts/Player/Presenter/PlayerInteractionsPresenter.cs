using System;
using System.Collections.Generic;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Inventory.Presenter;
using Farm.Player.Commands;
using Farm.Player.Events;
using Farm.Player.Model;
using Farm.Player.View;
using VContainer.Unity;

namespace Farm.Player.Presenter
{
    public class PlayerInteractionsPresenter : IStartable, IDisposable, ITickable
    {

        private readonly PlayerInteractionsView _view;
        private readonly PlayerInteractionsModel _model;
        private readonly EventSystem _eventSystem;
        private readonly IPlayerCutScenesCommandsDispatcher _cutSceneCommandsDispatcher;

        private bool _canInteract = true;
        public PlayerInteractionsPresenter(PlayerInteractionsView view, PlayerInteractionsModel model, EventSystem eventSystem, IPlayerCutScenesCommandsDispatcher cutSceneCommandsDispatcher)
        {
            _view = view;
            _model = model;
            _eventSystem = eventSystem;
            _cutSceneCommandsDispatcher = cutSceneCommandsDispatcher;

        }

        public void Start()
        {
            _eventSystem.Subscribe<PlayerInteractEvent>(HandleInteract);
            _eventSystem.Subscribe<PlayerCutSceneStartedEvent>(HandleCutSceneStarted);
            _eventSystem.Subscribe<PlayerCutSceneFinishedEvent>(HandleCutSceneFinished);
        }

        public void Dispose()
        {
            _eventSystem.Unsubscribe<PlayerInteractEvent>(HandleInteract);
            _eventSystem.Unsubscribe<PlayerCutSceneStartedEvent>(HandleCutSceneStarted);
            _eventSystem.Unsubscribe<PlayerCutSceneFinishedEvent>(HandleCutSceneFinished);
        }

        public void Tick()
        {
            var interactable = _view.CheckForInteractable();
            _model.FocusOnInteractable(interactable);
        }
        private void HandleInteract(PlayerInteractEvent eventData)
        {
            if (!_canInteract)
                return;
            if (_model.CurrentInteractable == null)
                return;
            if (CanPlayCutScene())
                return;
            _model.InteractWithCurrentInteractable();
        }
        private void HandleCutSceneStarted(PlayerCutSceneStartedEvent eventData)
        {
            _canInteract = false;
        }

        private void HandleCutSceneFinished(PlayerCutSceneFinishedEvent eventData)
        {
            _canInteract = true;
            _model.InteractWithCurrentInteractable();
        }
        private bool CanPlayCutScene()
        {
            if (_cutSceneCommandsDispatcher.TryPlayCutScene(_model.CurrentInteractable))
            return true;
            return false;
        }
    }
}