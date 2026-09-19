

using UnityEngine;

namespace Farm.Entry
{
    [System.Serializable]
    public abstract class ItemEntry
    {
        [SerializeField]
        protected ItemSO item ;
        public ItemSO Item => item;
        public ItemEntry(ItemSO item)
        {
            this.item = item;
        }
    }
}