using System;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Crops.Events;
using Farm.Crops.Model;
using Farm.Crops.View;
using VContainer.Unity;

namespace Farm.Crop.Presenter
{
    public class CropPresenter : IStartable, IDisposable
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
            _eventSystem.Subscribe<InstantiateCropVisualsEvent>(OnInstantiateCropVisuals);
            _eventSystem.Subscribe<SwitchCropStateEvent>(OnSwitchCropState);
        }
        public void Dispose()
        {
            _eventSystem.Unsubscribe<InstantiateCropVisualsEvent>(OnInstantiateCropVisuals);
            _eventSystem.Unsubscribe<SwitchCropStateEvent>(OnSwitchCropState);
        }

        private void OnInstantiateCropVisuals(InstantiateCropVisualsEvent eventData)
        {
            _cropView.InstantiateVisuals(eventData.seedingPrefab, eventData.harvestablePrefab);
        }

        private void OnSwitchCropState(SwitchCropStateEvent eventData)
        {
            _cropView.SwitchState(eventData.cropState);
        }

        public void Tick()
        {
            _cropModel.Tick();
        }
    }
}