using UnityEngine;

namespace Farm.Core.DesignPatterns.EventSystem
{
    public interface ISubscriber
    {
        void Subscribe<T>(EventDelegate<T> eventHandler) where T : IEvent;
        void Unsubscribe<T>(EventDelegate<T> eventHandler) where T : IEvent;
    }
}
