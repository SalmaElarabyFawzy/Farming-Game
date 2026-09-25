using Assets.Scripts.Interfaces;
using Farm.Enums;
using UnityEngine;


public enum FarmLandStatus
{
    Land,
    Plowed,
    Watered
}
public class FarmLand : MonoBehaviour, IInteractable, ITimeTracker
{
    [Header("Land Appearance")]
    [SerializeField] private Material dryLandMaterial;
    [SerializeField] private Material plowedLandMaterial;
    [SerializeField] private Material wateredLandMaterial;
    [SerializeField] private float highlightIntensity = 1.03f;


    [Header("Crop")]
    [SerializeField] private GameObject cropPrefab;

    private FarmLandStatus _currentStatus = FarmLandStatus.Land;
    private MeshRenderer _landRenderer;
    private Material _highLightMaterial;
    private const string _highlightProperty = "_Scale";
    private GameTimeStamp _timeLandWatered;
    private CropBehaviour _currentCrop = null;
    private Collider _collider;
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        TimeManager.instance.RemoveTracker(this);   
    }
    void Start()
    {
        _landRenderer = GetComponent<MeshRenderer>();
        _highLightMaterial = _landRenderer.materials[1];
        _collider = GetComponent<Collider>();
        TimeManager.instance.RegisterTracker(this);

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
       /*ItemSO equipedTool = InventoryManager.Instance.EquipedTool;

        if (equipedTool == null)
            return;

        EquipmentSO equipedEquipment = equipedTool as EquipmentSO;
        if (equipedEquipment != null)
        {
            EquipmentType toolType = equipedEquipment.equipmentType;
            switch (toolType)
            {
                case EquipmentType.Hoe:
                    if (_currentStatus == FarmLandStatus.Land)
                        SwitchLandStatus(FarmLandStatus.Plowed);
                    break;
                case EquipmentType.WateringCan:
                    if (_currentStatus == FarmLandStatus.Plowed)
                        SwitchLandStatus(FarmLandStatus.Watered);
                    break;
                case EquipmentType.Axe:
                case EquipmentType.Pickaxe:
                    // No interaction for these tools
                    break;
            }
            return;
        }

        SeedSO equipedSeed = equipedTool as SeedSO;
        if (equipedSeed != null)
        {
            if (_currentStatus != FarmLandStatus.Land && _currentCrop == null)
            {
               GameObject cropInstance = Instantiate(cropPrefab, transform);
                Vector3 plantPos = _collider.bounds.extents;
                cropInstance.transform.localPosition = new Vector3(0, plantPos.y,0);
                _currentCrop = cropInstance.GetComponent<CropBehaviour>();
                _currentCrop.Plant(equipedSeed);

            }
        }*/

    }
    private void SwitchLandStatus(FarmLandStatus newStatus)
    {
        _currentStatus = newStatus;
        UpdateLandAppearance();
    }
    private void UpdateLandAppearance()
    {
        Material[] materials = _landRenderer.materials;
        switch (_currentStatus)
        {
            case FarmLandStatus.Land:
                materials[0] = dryLandMaterial;
                break;
            case FarmLandStatus.Plowed:
                materials[0] = plowedLandMaterial;
                break;
            case FarmLandStatus.Watered:
                materials[0] = wateredLandMaterial;
                _timeLandWatered = TimeManager.instance.GetTimeStamp();
                break;
        }
        _landRenderer.materials = materials;
    }

    public void ClockUpdated(GameTimeStamp timeStamp)
    {
        if (_currentStatus == FarmLandStatus.Watered)
        {
            int elapsedHours = GameTimeStamp.CompareTwoTimeStamps(timeStamp, _timeLandWatered);
            if(elapsedHours > 24)
                SwitchLandStatus(FarmLandStatus.Plowed);

            if(_currentCrop != null)
                _currentCrop.Grow();
        }
    }
}
