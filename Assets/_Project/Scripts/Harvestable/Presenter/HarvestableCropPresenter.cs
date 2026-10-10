using Farm.FarmLand.Interfaces;
using Farm.HarvestableCrop.Model;
using Farm.HarvestableCrop.View;

namespace Farm.HarvestableCrop.Presenter
{
    public class HarvestableCropPresenter
    {
        private readonly HarvestableCropModel _model;
        private readonly HarvestableCropView _view;

        public HarvestableCropPresenter(HarvestableCropModel model, HarvestableCropView view)
        {
            _model = model;
            _view = view;
        }

        public void Start()
        {
            _model.OnCropHarvested += OnCropHarvested;
        }
        public void Dispose()
        {
            _model.OnCropHarvested -= OnCropHarvested;
        }

        public void SetFarmTile(IFarmTile farmTile)
        {
            _model.SetFarmTile(farmTile);
        }
        public void HarvestCrop()
        {
            _model.HarvestCrop();
        }
        private void OnCropHarvested()
        {
            _view.DestroyHarvestableCrop();
        }
    }
}