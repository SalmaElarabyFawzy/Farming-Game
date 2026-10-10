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

        public void SetFarmTile(IFarmTile farmTile)
        {
            _model.SetFarmTile(farmTile);
        }
        public void HarvestCrop()
        {
            _model.HarvestCrop();
        }
    }
}