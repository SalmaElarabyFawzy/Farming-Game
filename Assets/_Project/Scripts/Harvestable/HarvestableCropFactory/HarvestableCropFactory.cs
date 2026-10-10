using Farm.HarvestableCrop.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Farm.HarvestableCrop.Factory
{
    public class HarvestableCropFactory : IHarvestableCropFactory
    {
        private IObjectResolver _objectResolver;

        public HarvestableCropFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        public HarvestableCropView CreateHarvestableCrop(HarvestableCropView harvestablePrefab, Transform parent, Vector3 localPosition)
        {
            var harvestable = _objectResolver.Instantiate(harvestablePrefab, parent);
            harvestable.transform.localPosition = localPosition;
            return harvestable;
        }
    }
}