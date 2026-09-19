using UnityEngine;
using System;
using System.Collections.Generic;

namespace Farm.Core.DesignPatterns.EventSystem
{
    public class EventSystem : IEventSystem
    {
        private Dictionary<Type, List<Delegate>> eventHandlers = new Dictionary<Type, List<Delegate>>();

        public void Subscribe<T>(EventDelegate<T> eventHandler) where T : IEvent
        {
            Type eventType = typeof(T);
            if (!eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType] = new List<Delegate>();
            }
            eventHandlers[eventType].Add(eventHandler);
        }

        public void Unsubscribe<T>(EventDelegate<T> eventHandler) where T : IEvent
        {
            Type eventType = typeof(T);
            if (eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType].Remove(eventHandler);
                if (eventHandlers[eventType].Count == 0)
                {
                    eventHandlers.Remove(eventType);
                }
            }
        }

        public void Publish<T>(T eventData) where T : IEvent
        {
            Type eventType = typeof(T);
            if (eventHandlers.ContainsKey(eventType))
            {
                var handlers = eventHandlers[eventType].ToArray();

                foreach (Delegate handler in handlers)
                {
                    EventDelegate<T> eventHandler = handler as EventDelegate<T>;
                    eventHandler?.Invoke(eventData);
                }
            }
        }
    }
}
