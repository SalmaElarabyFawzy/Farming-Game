using UnityEngine;
using Farm.FarmLand.Interfaces;
using Farm.Inventory.Presenter;

namespace Farm.HarvestableCrop.Model
{
    public class HarvestableCropModel
    {
        private readonly ItemSO _cropItem;
        private readonly IInventoryProvider _inventoryProvider;

        private IFarmTile _farmTile;

        public HarvestableCropModel(ItemSO cropItem, IInventoryProvider inventoryProvider)
        {
            _cropItem = cropItem;
            _inventoryProvider = inventoryProvider;
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
            // InventoryEvents.OnItemAdded(-1 , cropItem , InventorySlotType.Item);
            //Destroy(gameObject);
        }
    }
}