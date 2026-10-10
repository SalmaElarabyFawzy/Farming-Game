

using Farm.Crops.View;
using Farm.FarmLand.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Farm.Crops.Factory
{
    public class CropFactory : ICropFactory
    {
        private readonly CropView _cropPrefab;
        private readonly IObjectResolver objectResolver;

        public CropFactory(CropView cropPrefab, IObjectResolver objectResolver)
        {
            _cropPrefab = cropPrefab;
            this.objectResolver = objectResolver;
        }

        public CropView CreateCrop(SeedSO seedData, Transform parent, Vector3 localPosition, IFarmTile farmTile)
        {
            CropView cropView = objectResolver.Instantiate(_cropPrefab, parent);
            cropView.transform.localPosition = localPosition;
            cropView.Plant(seedData, farmTile);
            return cropView;
        }


    }

}