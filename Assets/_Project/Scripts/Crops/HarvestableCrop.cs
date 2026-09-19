using Assets.Scripts.Interfaces;
using Farm.Enums;
using UnityEngine;

public class HarvestableCrop : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSO cropItem;
    [SerializeField] private float highlightIntensity = 1.03f;
    [SerializeField] private MeshRenderer cropRenderer;

    private Material _highLightMaterial;
    private const string _highlightProperty = "_Scale";

  

    private void Start()
    {
        _highLightMaterial = cropRenderer.materials[1];
    }
    public void OnDefocus()
    {
        _highLightMaterial.SetFloat(_highlightProperty, 0f);
    }

    public void OnFocus()
    {
        _highLightMaterial.SetFloat(_highlightProperty, highlightIntensity);
    }

    public void OnInteract()
    {
        Debug.Log("done!!!");
        InventoryEvents.OnItemAdded(-1 , cropItem , InventorySlotType.Item);
        Destroy(gameObject);
    }

}
