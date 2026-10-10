using System;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Crops.Enums;
using Farm.Crops.Events;
using UnityEngine;
using VContainer.Unity;

namespace Farm.Crops.Model
{
    public class CropModel
    {
        public event Action<GameObject, GameObject> OnInstantiateCropVisuals;
        public event Action<CropState> OnSwitchCropState;

        private CropState _currentCropState;
        private int _growthDaysInMinutes = 0;
        private int _maxGrowthDaysInMinutes = 0;

        private bool _isWatered = false;

        public void Plant(SeedSO seed)
        {
            var _seedToGrow = seed;
            var _seeding = _seedToGrow.seeding;
            var cropItem = _seedToGrow.cropItem;
            var _harvestable = cropItem.itemPrefab;

            InvokeInstantiateCropVisuals(_seeding, _harvestable);

            int maxGrowthDays = _seedToGrow.daysToGrow;
            int maxGrowthHours = GameTimeStamp.DaysToHours(maxGrowthDays);
            _maxGrowthDaysInMinutes = GameTimeStamp.HoursToMinutes(maxGrowthHours);

            InvokeSwitchCropState(CropState.Seed);
            _currentCropState = CropState.Seed;
        }
        public void WaterCrop()
        {
            Debug.Log($"CropModel WaterCrop: {_isWatered}");
            _isWatered = !_isWatered;
        }
        private void InvokeInstantiateCropVisuals(GameObject seedingPrefab, GameObject harvestablePrefab)
        {
            OnInstantiateCropVisuals?.Invoke(seedingPrefab, harvestablePrefab);
        }
        private void InvokeSwitchCropState(CropState newState)
        {
            OnSwitchCropState?.Invoke(newState);
        }

        public void ClockUpdated(GameTimeStamp timeStamp)
        {
            Grow();
        }
        public void Grow()
        {
            _growthDaysInMinutes++;

            if (!_isWatered)
                return;

            if (_growthDaysInMinutes >= _maxGrowthDaysInMinutes / 2 && _currentCropState == CropState.Seed) ;
            {
                InvokeSwitchCropState(CropState.Seeding);
                _currentCropState = CropState.Seeding;
            }

            if (_growthDaysInMinutes >= _maxGrowthDaysInMinutes && _currentCropState == CropState.Seeding)
            {
                InvokeSwitchCropState(CropState.Harvestable);
                _currentCropState = CropState.Harvestable;
            }

        }
    }
}