using UnityEngine;

public enum CropState
{
   Seed,
   Seeding,
   Harvestable
}
public class CropBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject seed;
    private GameObject _seeding;
    private GameObject _harvestable;

    private SeedSO _seedToGrow;
    private CropState _currentCropState;  

    private int _growthDaysInMinutes = 0;
    private int _maxGrowthDaysInMinutes = 0;
    public void Plant(SeedSO seed)
   {
        _seedToGrow = seed;

        _seeding = Instantiate(_seedToGrow.seeding, transform);
        _seeding.transform.localPosition = Vector3.zero;

        ItemSO cropItem = _seedToGrow.cropItem;

       _harvestable = Instantiate(cropItem.itemPrefab, transform);
        _harvestable.transform.localPosition = Vector3.zero;
        int maxGrowthDays = _seedToGrow.daysToGrow;
        int maxGrowthHours = GameTimeStamp.DaysToHours(maxGrowthDays);
        _maxGrowthDaysInMinutes = GameTimeStamp.HoursToMinutes(maxGrowthHours);

        SwitchState(CropState.Seed);
        _currentCropState = CropState.Seed;
    }
    public void Grow()
    {

        _growthDaysInMinutes++;

        if(_growthDaysInMinutes >= _maxGrowthDaysInMinutes/2 && _currentCropState == CropState.Seed)
            SwitchState(CropState.Seeding);

        if (_growthDaysInMinutes >= _maxGrowthDaysInMinutes && _currentCropState == CropState.Seeding)
            SwitchState(CropState.Harvestable);

    }
    private void SwitchState(CropState newState)
    {
        seed.SetActive(false);
        _seeding.SetActive(false);
        _harvestable.SetActive(false);

        switch (newState)
        {
            case CropState.Seed:
                seed.SetActive(true);
                break;
            case CropState.Seeding:
                _seeding.SetActive(true);
                break;
            case CropState.Harvestable:
                _harvestable.SetActive(true);
                _harvestable.transform.parent = null;
                Destroy(gameObject);
                break;
        }
        _currentCropState = newState;
    }
}
