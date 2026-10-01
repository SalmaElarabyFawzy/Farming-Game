using Farm.Crops.Enums;
using UnityEngine;

namespace Farm.Crops.View
{
    public class CropView : MonoBehaviour
    {
        [SerializeField] private GameObject seed;
        private GameObject _seeding;
        private GameObject _harvestable;

        public void InstantiateVisuals(GameObject seeding, GameObject harvestable)
        {
            _seeding = Instantiate(seeding, transform);
            _seeding.transform.localPosition = Vector3.zero;
            _harvestable = Instantiate(harvestable, transform);
            _harvestable.transform.localPosition = Vector3.zero;
        }
        public void Plant(SeedSO seedData)
        {
            
        }
        public void Grow()
        {

        }

        public void SwitchState(CropState newState)
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
                    break;
            }
        }
    }
}