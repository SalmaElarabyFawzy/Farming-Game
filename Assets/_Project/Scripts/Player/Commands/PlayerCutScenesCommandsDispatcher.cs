using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using Farm.Enums;
using Farm.Inventory.Presenter;
using Farm.Player.View;
using UnityEngine;

namespace Farm.Player.Commands
{
    public class PlayerCutScenesCommandsDispatcher : IPlayerCutScenesCommandsDispatcher
    {
        private readonly IEnumerable<IPlayerCutSceneCommand> _cutSceneCommands;

        public PlayerCutScenesCommandsDispatcher(IEnumerable<IPlayerCutSceneCommand> cutSceneCommands)
        {
            _cutSceneCommands = cutSceneCommands;
        }

        public bool TryPlayCutScene(IInteractable interactable)
        {
            foreach (var command in _cutSceneCommands)
            {
                if (command.TryExecuteCutScene(interactable))
                {
                    return true;
                }
            }
            return false;
        }
    }
}