using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float timeScale = 1f;
    [SerializeField] private Transform sunTransform;
    public static TimeManager instance { get; private set; }
    private GameTimeStamp gameTimeStamp;
    private List<ITimeTracker> listeners = new List<ITimeTracker>();

    public GameTimeStamp GetTimeStamp()
    {
        return new GameTimeStamp(gameTimeStamp);
    }
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);

        else
            instance = this;

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
            yield return new WaitForSeconds(1/timeScale);
            Tick();
        }
    }
    private void Tick()
    {
        foreach (var listener in listeners)
            listener.ClockUpdated(gameTimeStamp);

        gameTimeStamp.UpdateClock();
        int timeInMinutes = GameTimeStamp.HoursToMinutes(gameTimeStamp.Hour) + gameTimeStamp.Minute;
        float sunAngle = .25f * timeInMinutes - 90f;
        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }
    public void RegisterTracker(ITimeTracker tracker)
    {
        listeners.Add(tracker);
    }
    public void RemoveTracker(ITimeTracker tracker)
    {
        listeners.Remove(tracker);
    }
}
