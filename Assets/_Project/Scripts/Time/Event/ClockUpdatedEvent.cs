using Farm.Core.DesignPatterns.EventSystem;

namespace Farm.TimeManager.Event
{
    public class ClockUpdatedEvent : IEvent
    {
        public GameTimeStamp GameTimeStamp { get; private set; }

        public ClockUpdatedEvent(GameTimeStamp gameTimeStamp)
        {
            this.GameTimeStamp = gameTimeStamp;
        }
    }
}