using System;
using Assets.Scripts.Interfaces;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Crop.Presenter;
using Farm.Crops.Enums;
using Farm.Crops.Events;
using Farm.Crops.Model;
using Farm.FarmLand.Interfaces;
using Farm.HarvestableCrop.Factory;
using Farm.HarvestableCrop.View;
using Farm.TimeManager.Event;
using UnityEngine;
using VContainer;

namespace Farm.Crops.View
{
    public class CropView : MonoBehaviour, IDisposable
    {
        [SerializeField] private GameObject seed;
        private GameObject _seeding;
        private GameObject _harvestable;

        private EventSystem _eventSystem;
        private CropPresenter _cropPresenter;

        private IFarmTile _farmTile;
        private IHarvestableCropFactory _harvestableCropFactory;

        [Inject]
        public void Construct(EventSystem eventSystem, IHarvestableCropFactory harvestableCropFactory)
        {
            Debug.Log("CropView Construct");
            _eventSystem = eventSystem;
            _harvestableCropFactory = harvestableCropFactory;
            CropModel model = new CropModel();
            _cropPresenter = new CropPresenter(model, this, _eventSystem);
            _cropPresenter.Start();

        }
        public void Start()
        {
            _eventSystem.Subscribe<ClockUpdatedEvent>(ClockUpdated);
            Debug.Log("CropView Start");
        }

        public void Dispose()
        {
            _cropPresenter.Dispose();
        }

        public void InstantiateVisuals(GameObject seeding, GameObject harvestable)
        {
            Debug.Log("CropView InstantiateVisuals");
            _seeding = Instantiate(seeding, transform);
            _seeding.transform.localPosition = Vector3.zero;
            var harvestableCrop = _harvestableCropFactory.CreateHarvestableCrop(harvestable.GetComponent<HarvestableCropView>(), transform, Vector3.zero);
            _harvestable = harvestableCrop.gameObject;
            if (harvestableCrop != null)
            {
                harvestableCrop.SetFarmTile(_farmTile);
            }
        }
        public void Plant(SeedSO seedData, IFarmTile farmTile)
        {
            Debug.Log("CropView Plant");
            _farmTile = farmTile;
            _cropPresenter.Plant(seedData);
        }

        public void ClockUpdated(ClockUpdatedEvent eventData)
        {
            _cropPresenter.UpdateClock(eventData.GameTimeStamp);
        }
        public void WaterCrop()
        {
            _cropPresenter.WaterCrop();
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