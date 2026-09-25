using Assets.Scripts.Interfaces;
using Farm.Inventory.Presenter;
using Farm.Player.View;
using UnityEngine;

namespace Farm.Player.Commands
{
    public class PlayerHarvestCutSceneCommand : IPlayerCutSceneCommand
    {
        private readonly PlayerCutScenesView cutScenesView;

        public PlayerHarvestCutSceneCommand(PlayerCutScenesView cutScenesView)
        {
            this.cutScenesView = cutScenesView;
        }
        public bool TryExecuteCutScene(IInteractable interactable)
        {
            if (interactable is HarvestableCrop)
            {
                Debug.Log("Harvest Cutscene Executed");
                cutScenesView.PlayHarvestCutScene();
                return true;
            }
            return false;
        }
    }
}