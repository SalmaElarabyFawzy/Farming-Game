using Farm.FarmLand.Interfaces;
using Farm.HarvestableCrop.View;
using UnityEngine;

namespace Farm.HarvestableCrop.Factory
{
    public interface IHarvestableCropFactory
    {
        HarvestableCropView CreateHarvestableCrop(HarvestableCropView harvestablePrefab, Transform parent, Vector3 localPosition);
    }
}