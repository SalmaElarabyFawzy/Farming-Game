using System;
using Farm.Crops.View;
using Farm.Enums;
using Farm.FarmLand.Enums;
using Farm.Inventory.Presenter;
using Farm.TimeManager.Interfaces;
using UnityEngine;

namespace Farm.FarmLand.Model
{
    public class FarmLandModel
    {

        public event Action<FarmLandStatus> OnLandStatusChanged;
        public event Action<SeedSO> OnCropPlanted;
        public event Action OnLandWatered;
        private FarmLandStatus _currentStatus = FarmLandStatus.Land;
        private GameTimeStamp _timeLandWatered;

        private bool _hasCropPlanted = false;
        private SeedSO _equipedSeed = null;

        private IInventoryProvider _inventoryProvider;
        private ITimeManager _timeManager;

        public FarmLandModel(IInventoryProvider inventoryProvider, ITimeManager timeManager)
        {
            _inventoryProvider = inventoryProvider;
            _timeManager = timeManager;
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
            if (equipedSeed != null && _currentStatus == FarmLandStatus.Plowed)
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
                        _currentStatus = FarmLandStatus.Plowed;
                    }
                    break;
                case EquipmentType.WateringCan:
                    if (_currentStatus == FarmLandStatus.Plowed)
                    {
                        OnLandStatusChanged?.Invoke(FarmLandStatus.Watered);
                        _currentStatus = FarmLandStatus.Watered;
                        _timeLandWatered = _timeManager.GetCurrentTimeStamp();
                        OnLandWatered?.Invoke();
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
            if (_currentStatus == FarmLandStatus.Watered)
                OnLandWatered?.Invoke();
            _hasCropPlanted = true;
        }
        public void ClockUpdated(GameTimeStamp timeStamp)
        {
            if (_currentStatus == FarmLandStatus.Watered)
            {
                int elapsedHours = GameTimeStamp.CompareTwoTimeStamps(timeStamp, _timeLandWatered);
                if (elapsedHours > 24)
                {
                    _currentStatus = FarmLandStatus.Plowed;
                    OnLandStatusChanged?.Invoke(FarmLandStatus.Plowed);
                    OnLandWatered?.Invoke();
                }
            }
        }
        public void HarvestCrop()
        {
            _hasCropPlanted = false;
            _equipedSeed = null;
        }
    }
}