using UnityEngine;


public enum EquipmentType
{
    Axe,
    Pickaxe,
    WateringCan,
    Hoe
}


[CreateAssetMenu(menuName = "Items/EquipmentSO")]
public class EquipmentSO : ItemSO
{
  public EquipmentType equipmentType;
}
