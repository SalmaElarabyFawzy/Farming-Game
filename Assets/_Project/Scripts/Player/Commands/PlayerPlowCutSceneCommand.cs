using UnityEngine;
using Assets.Scripts.Interfaces;
using Farm.Enums;
using Farm.Inventory.Presenter;
using Farm.Player.View;

namespace Farm.Player.Commands
{
    public class PlayerPlowCutSceneCommand : IPlayerCutSceneCommand
    {
        private readonly PlayerCutScenesView cutScenesView;
        private readonly IInventoryProvider inventoryProvider;

        public PlayerPlowCutSceneCommand(PlayerCutScenesView cutScenesView, IInventoryProvider inventoryProvider)
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

                EquipmentSO tool = equipedTool as EquipmentSO;
                if (tool == null)
                    return false;

                if (tool.equipmentType != EquipmentType.Hoe)
                    return false;

                cutScenesView.PlayPlowCutScene();
                Debug.Log("Plow Cutscene Executed");
                return true;
            }
            return false;
        }
    }
}