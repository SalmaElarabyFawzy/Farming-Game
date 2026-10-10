using Farm.Core.DesignPatterns.EventSystem;
using Farm.Enums;

namespace Farm.HarvestableCrop.Events
{
    public class CropHarvestedEvent : IEvent
    {
        private ItemSO _cropItem;
        private InventorySlotType _inventoryType;
        public CropHarvestedEvent(ItemSO cropItem, InventorySlotType inventoryType)
        {
            _cropItem = cropItem;
            _inventoryType = inventoryType;
        }
        public ItemSO CropItem => _cropItem;
        public InventorySlotType InventoryType => _inventoryType;
    }

 
}