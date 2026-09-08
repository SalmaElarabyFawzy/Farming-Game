using UnityEngine;

[CreateAssetMenu(menuName = "Items/SeedSO")]
public class SeedSO : ItemSO
{
   public int daysToGrow;
   public ItemSO cropItem;
   public GameObject seeding;
}
