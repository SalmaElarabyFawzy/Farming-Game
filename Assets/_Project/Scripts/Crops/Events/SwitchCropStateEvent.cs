using Farm.Core.DesignPatterns.EventSystem;
using Farm.Crops.Enums;

namespace Farm.Crops.Events
{
    public class SwitchCropStateEvent : IEvent
    {
        public CropState cropState;

        public SwitchCropStateEvent(CropState cropState)
        {
            this.cropState = cropState;
        }
    }
}