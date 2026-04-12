using UnityEngine;
using System;

public static class InventoryEvents 
{
   public static Action<ItemSO[], ItemSO[]> OnInventoryStart;
   public static Action<int ,ItemSO, InventorySlotType> OnSlotClicked;
   public static Action<InventorySlotType> OnHandClicked;
    public static Action<int, ItemSO, InventorySlotType> OnItemAdded;
}
