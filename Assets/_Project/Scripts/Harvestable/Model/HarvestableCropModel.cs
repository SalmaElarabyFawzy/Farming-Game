using UnityEngine;
using Farm.FarmLand.Interfaces;
using Farm.Inventory.Presenter;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.HarvestableCrop.Events;
using Farm.Enums;
using System;

namespace Farm.HarvestableCrop.Model
{
    public class HarvestableCropModel
    {
        public event Action OnCropHarvested;
        private readonly ItemSO _cropItem;
        private EventSystem _eventSystem;

        private IFarmTile _farmTile;

        public HarvestableCropModel(ItemSO cropItem, EventSystem eventSystem)
        {
            _cropItem = cropItem;
            _eventSystem = eventSystem;
        }

        public void SetFarmTile(IFarmTile farmTile)
        {
            _farmTile = farmTile;
        }

        public void HarvestCrop()
        {
            Debug.Log("done!!!");
            _farmTile.HarvestCrop();
            _farmTile = null;
            OnCropHarvested?.Invoke();
           _eventSystem.Publish(new CropHarvestedEvent(_cropItem, InventorySlotType.Item));
        }
    }


}