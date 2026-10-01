using System;
using Farm.Crops.View;
using Farm.Enums;
using Farm.FarmLand.Enums;
using Farm.Inventory.Presenter;
using UnityEngine;

namespace Farm.FarmLand.Model
{
    public class FarmLandModel
    {

        public event Action<FarmLandStatus> OnLandStatusChanged;
        public event Action<SeedSO> OnCropPlanted;
        private FarmLandStatus _currentStatus = FarmLandStatus.Land;
        private GameTimeStamp _timeLandWatered;

        private bool _hasCropPlanted = false;
        private SeedSO _equipedSeed = null;

        private IInventoryProvider _inventoryProvider;

        public FarmLandModel(IInventoryProvider inventoryProvider)
        {
            _inventoryProvider = inventoryProvider;
        }
        public void Interact()
        {
            ItemSO equipedTool = _inventoryProvider.GetEquipedTool();

            if (equipedTool == null)
                return;

            EquipmentSO equipedEquipment = equipedTool as EquipmentSO;
            if (equipedEquipment != null)
            {
                EquipmentType toolType = equipedEquipment.equipmentType;
                ChangeLandStatusBasedOnTool(toolType);
                return;
            }

            SeedSO equipedSeed = equipedTool as SeedSO;
            if (equipedSeed != null)
                TryPlantCrop(equipedSeed);
        }

        private void ChangeLandStatusBasedOnTool(EquipmentType toolType)
        {
            switch (toolType)
            {
                case EquipmentType.Hoe:
                    if (_currentStatus == FarmLandStatus.Land)
                    {
                        OnLandStatusChanged?.Invoke(FarmLandStatus.Plowed);
                    }
                    break;
                case EquipmentType.WateringCan:
                    if (_currentStatus == FarmLandStatus.Plowed)
                    {
                        OnLandStatusChanged?.Invoke(FarmLandStatus.Watered);
                    }
                    break;
                case EquipmentType.Axe:
                case EquipmentType.Pickaxe:
                    // No interaction for these tools
                    break;
            }
        }

        private void TryPlantCrop(SeedSO seedData)
        {
            if (_currentStatus == FarmLandStatus.Land && _hasCropPlanted)
                return;

            _equipedSeed = seedData;
            OnCropPlanted?.Invoke(_equipedSeed);
            _hasCropPlanted = true;
        }
        public void ClockUpdated(GameTimeStamp timeStamp)
        {
            if (_currentStatus == FarmLandStatus.Watered)
            {
                int elapsedHours = GameTimeStamp.CompareTwoTimeStamps(timeStamp, _timeLandWatered);
                if (elapsedHours > 24)
                    OnLandStatusChanged?.Invoke(FarmLandStatus.Plowed);
            }
        }
    }
}