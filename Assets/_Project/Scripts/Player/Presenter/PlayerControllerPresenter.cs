using System;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Player.Events;
using Farm.Player.Model;
using Farm.Player.View;
using UnityEngine;
using VContainer.Unity;

namespace Farm.Player.Presenter
{
    public class PlayerControllerPresenter : IStartable, IDisposable, ITickable
    {

        private readonly PlayerControllerModel _model;
        private readonly PlayerControllerView _view;

        private readonly EventSystem _eventSystem;

        public PlayerControllerPresenter(PlayerControllerModel model, PlayerControllerView view, EventSystem eventSystem)
        {
            _model = model;
            _view = view;
            _eventSystem = eventSystem;
        }

        public void Start()
        {
            _eventSystem.Subscribe<PlayerMoveEvent>(HandleMove);
            _eventSystem.Subscribe<PlayerJumpEvent>(HandleJump);
            _eventSystem.Subscribe<PlayerCutSceneStartedEvent>(HandleCutSceneStarted);
            _eventSystem.Subscribe<PlayerCutSceneFinishedEvent>(HandleCutSceneFinished);
        }

        public void Dispose()
        {
            _eventSystem.Unsubscribe<PlayerMoveEvent>(HandleMove);
            _eventSystem.Unsubscribe<PlayerJumpEvent>(HandleJump);
            _eventSystem.Unsubscribe<PlayerCutSceneStartedEvent>(HandleCutSceneStarted);
            _eventSystem.Unsubscribe<PlayerCutSceneFinishedEvent>(HandleCutSceneFinished);
        }
        private void HandleMove(PlayerMoveEvent eventData)
        {
            if (!_model.CanMove)
                return;
            _model.MoveInput = eventData.MoveDirection;
        }
        private void HandleJump(PlayerJumpEvent eventData)
        {
            if (!_model.CanMove)
                return;

            if (!_view.IsGrounded)
                return;
            _model.Jump(_view.Rb, _view.JumpForce);
            _view.IsGrounded = false;
        }

        private void HandleCutSceneStarted(PlayerCutSceneStartedEvent eventData)
        {
            _model.CanMove = false;
        }

        private void HandleCutSceneFinished(PlayerCutSceneFinishedEvent eventData)
        {
            _model.CanMove = true;
        }

        public void Tick()
        {
            if (!_model.CanMove)
                return;
            _model.MoveAndRotate(_view.CameraTransform, _view.Rb, _view.MoveSpeed, _view.RotationSpeed);
        }

    }
}