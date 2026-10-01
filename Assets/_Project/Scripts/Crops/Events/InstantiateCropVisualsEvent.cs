using Farm.Core.DesignPatterns.EventSystem;
using UnityEngine;

namespace Farm.Crops.Events
{
    public class InstantiateCropVisualsEvent : IEvent
    {
        public GameObject seedingPrefab;
        public GameObject harvestablePrefab;

        public InstantiateCropVisualsEvent(GameObject seedingPrefab, GameObject harvestablePrefab)
        {
            this.seedingPrefab = seedingPrefab;
            this.harvestablePrefab = harvestablePrefab;
        }
    }
}