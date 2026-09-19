using UnityEngine;

namespace Farm.Core.DesignPatterns.EventSystem
{
    public interface IPublisher
    {
        void Publish<T>(T eventData) where T : IEvent;
    }
}
