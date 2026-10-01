using Farm.Core.DesignPatterns.EventSystem;
using Farm.Crops.Enums;
using Farm.Crops.Events;
using VContainer.Unity;

namespace Farm.Crops.Model
{
    public class CropModel : ITickable
    {
        private CropState _currentCropState;
        private int _growthDaysInMinutes = 0;
        private int _maxGrowthDaysInMinutes = 0;

        private readonly EventSystem _eventSystem;

        public CropModel(EventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

// TODO: Add a method to reset the crop state and growth days when a new seed is planted or when the crop is harvested. This will ensure that the crop model can be reused for multiple planting cycles without retaining old state information.
        public void Plant(SeedSO seed)
        {
            var _seedToGrow = seed;
            var _seeding = _seedToGrow.seeding;
            var cropItem = _seedToGrow.cropItem;
            var _harvestable = cropItem.itemPrefab;

            _eventSystem.Publish(new InstantiateCropVisualsEvent(_seeding, _harvestable));

            int maxGrowthDays = _seedToGrow.daysToGrow;
            int maxGrowthHours = GameTimeStamp.DaysToHours(maxGrowthDays);
            _maxGrowthDaysInMinutes = GameTimeStamp.HoursToMinutes(maxGrowthHours);

            _eventSystem.Publish(new SwitchCropStateEvent(CropState.Seed));
            _currentCropState = CropState.Seed;
        }

        public void Tick()
        {
            Grow();
        }
        public void Grow()
        {
            _growthDaysInMinutes++;

            if (_growthDaysInMinutes >= _maxGrowthDaysInMinutes / 2 && _currentCropState == CropState.Seed) ;
            {
                _eventSystem.Publish(new SwitchCropStateEvent(CropState.Seeding));
                _currentCropState = CropState.Seeding;
            }

            if (_growthDaysInMinutes >= _maxGrowthDaysInMinutes && _currentCropState == CropState.Seeding)
            {
                _eventSystem.Publish(new SwitchCropStateEvent(CropState.Harvestable));
                _currentCropState = CropState.Harvestable;
            }

        }
    }
}