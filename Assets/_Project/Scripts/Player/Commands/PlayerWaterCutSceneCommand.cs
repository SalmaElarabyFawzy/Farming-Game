using Assets.Scripts.Interfaces;
using Farm.Enums;
using Farm.Inventory.Presenter;
using Farm.Player.View;
using UnityEngine;

namespace Farm.Player.Commands
{
    public class PlayerWaterCutSceneCommand : IPlayerCutSceneCommand
    {
        private readonly PlayerCutScenesView cutScenesView;
        private readonly IInventoryProvider inventoryProvider;

        public PlayerWaterCutSceneCommand(PlayerCutScenesView cutScenesView, IInventoryProvider inventoryProvider)
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

                if (tool.equipmentType != EquipmentType.WateringCan)
                    return false;
                Debug.Log("Water Cutscene Executed");

                cutScenesView.PlayWaterCutScene();
                return true;
            }
            return false;
        }
    }
}