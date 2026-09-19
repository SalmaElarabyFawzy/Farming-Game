using UnityEngine;

namespace Farm.Core.DesignPatterns.EventSystem
{
    public delegate void EventDelegate<T>(T eventData) where T : IEvent;
}
