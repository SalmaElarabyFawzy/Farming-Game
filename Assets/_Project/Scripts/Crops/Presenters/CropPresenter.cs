using System;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Crops.Enums;
using Farm.Crops.Events;
using Farm.Crops.Model;
using Farm.Crops.View;
using UnityEngine;
using VContainer.Unity;

namespace Farm.Crop.Presenter
{
    public class CropPresenter : IDisposable
    {
        private readonly CropModel _cropModel;
        private readonly CropView _cropView;

        private readonly EventSystem _eventSystem;

        public CropPresenter(CropModel cropModel, CropView cropView, EventSystem eventSystem)
        {
            _cropModel = cropModel;
            _cropView = cropView;
            _eventSystem = eventSystem;
        }

        public void Start()
        {
            _cropModel.OnInstantiateCropVisuals += OnInstantiateCropVisuals;
            _cropModel.OnSwitchCropState += OnSwitchCropState;
        }
        public void Dispose()
        {
            _cropModel.OnInstantiateCropVisuals -= OnInstantiateCropVisuals;
            _cropModel.OnSwitchCropState -= OnSwitchCropState;
        }

        private void OnInstantiateCropVisuals(GameObject seedingPrefab, GameObject harvestablePrefab)
        {
            _cropView.InstantiateVisuals(seedingPrefab, harvestablePrefab);
        }

        private void OnSwitchCropState(CropState newState)
        {
            _cropView.SwitchState(newState);
        }

        public void Plant(SeedSO seedData)
        {
            _cropModel.Plant(seedData);
        }
        public void WaterCrop()
        {
            _cropModel.WaterCrop();
        }

        public void UpdateClock(GameTimeStamp timeStamp)
        {
            _cropModel.ClockUpdated(timeStamp);
        }
    }
}