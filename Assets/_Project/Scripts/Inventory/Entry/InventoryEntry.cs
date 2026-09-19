using Farm.Entry;
using UnityEngine;

namespace Farm.Inventory.Entry
{
    [System.Serializable]
    public class InventoryEntry : ItemEntry
    {
        [SerializeField]
        private int quantity;
        public int Quantity => quantity;
        public InventoryEntry(ItemSO item, int quantity) : base(item)
        {
            this.quantity = quantity;
        }

        public void UpdateQuantity(int newQuantity)
        {
            quantity = newQuantity;
        }
    }
}
