

using Farm.Crops.View;
using Farm.FarmLand.Interfaces;
using UnityEngine;

namespace Farm.Crops.Factory
{
    public interface ICropFactory
    {
        CropView CreateCrop(SeedSO seedData, Transform parent, Vector3 localPosition, IFarmTile farmTile);
    }
}