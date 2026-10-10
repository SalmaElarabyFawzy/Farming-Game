using System.Collections;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.TimeManager.Interfaces;
using Farm.TimeManager.Event;
using UnityEngine;
using VContainer;

namespace Farm.TimeManager.View
{
    public class TimeManagerView : MonoBehaviour, ITimeManager
    {
        [SerializeField] private float timeScale = 1f;
        [SerializeField] private Transform sunTransform;

        private GameTimeStamp gameTimeStamp;
        private EventSystem eventSystem;

        public GameTimeStamp GetTimeStamp()
        {
            return new GameTimeStamp(gameTimeStamp);
        }

        [Inject]
        public void Construct(EventSystem eventSystem)
        {
            this.eventSystem = eventSystem;
        }
        private void Start()
        {
            gameTimeStamp = new GameTimeStamp(1, 0, 6, 0, Season.Spring);
            StartCoroutine(TickCoroutine());
        }

        private IEnumerator TickCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1 / timeScale);
                Tick();
            }
        }
        private void Tick()
        {
            eventSystem.Publish(new ClockUpdatedEvent(gameTimeStamp));
            gameTimeStamp.UpdateClock();
            ControlSunRotation();
        }
        private void ControlSunRotation()
        {
            int timeInMinutes = GameTimeStamp.HoursToMinutes(gameTimeStamp.Hour) + gameTimeStamp.Minute;
            float sunAngle = .25f * timeInMinutes - 90f;
            sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
        }
        public GameTimeStamp GetCurrentTimeStamp()
        {
            return new GameTimeStamp(gameTimeStamp);
        }

    }
}