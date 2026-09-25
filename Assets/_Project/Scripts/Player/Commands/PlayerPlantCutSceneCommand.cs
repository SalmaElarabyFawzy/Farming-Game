using Assets.Scripts.Interfaces;
using Farm.Inventory.Presenter;
using Farm.Player.View;
using UnityEngine;

namespace Farm.Player.Commands
{
   
    public class PlayerPlantCutSceneCommand : IPlayerCutSceneCommand
    {
        private readonly PlayerCutScenesView cutScenesView;
        private readonly IInventoryProvider inventoryProvider;

        public PlayerPlantCutSceneCommand(PlayerCutScenesView cutScenesView, IInventoryProvider inventoryProvider)
        {
            this.cutScenesView = cutScenesView;
            this.inventoryProvider = inventoryProvider;
        }
        public bool TryExecuteCutScene(IInteractable interactable)
        {
            if (interactable is FarmLand)
            {
                var equipedTool = inventoryProvider.GetEquipedTool();
                if (equipedTool == null)
                    return false;

                SeedSO seed = equipedTool as SeedSO;
                if (seed == null)
                    return false;
                Debug.Log("Plant Cutscene Executed");
                cutScenesView.PlayPlantCutScene();
                return true;
            }
            return false;
        }
    }
}