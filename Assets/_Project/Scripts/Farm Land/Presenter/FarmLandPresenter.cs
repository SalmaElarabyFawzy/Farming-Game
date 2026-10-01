using System;
using Farm.FarmLand.Enums;
using Farm.FarmLand.Model;
using Farm.FarmLand.View;
using Farm.Inventory.Presenter;
using VContainer.Unity;

namespace Farm.FarmLand.Presenter
{
    public class FarmLandPresenter : IDisposable
    {
        private FarmLandModel _model;
        private FarmLandView _view;


        public FarmLandPresenter(FarmLandModel model, FarmLandView view)
        {
            _model = model;
            _view = view;
        }
        public void Start()
        {
            _model.OnLandStatusChanged += HandleLandStatusChanged;
            _model.OnCropPlanted += HandleCropPlanted;
        }

        public void Dispose()
        {
            _model.OnLandStatusChanged -= HandleLandStatusChanged;
            _model.OnCropPlanted -= HandleCropPlanted;
        }

        private void HandleLandStatusChanged(FarmLandStatus status)
        {
            _view.UpdateLandAppearance(status);
        }

        private void HandleCropPlanted(SeedSO seed)
        {
            _view.InstantiateCrop(seed);
        }

        public void Interact()
        {
            _model.Interact();
        }

    }
}