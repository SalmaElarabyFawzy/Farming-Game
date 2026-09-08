using UnityEngine;

[CreateAssetMenu(menuName = "Items/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string description;
    public GameObject itemPrefab;
}
