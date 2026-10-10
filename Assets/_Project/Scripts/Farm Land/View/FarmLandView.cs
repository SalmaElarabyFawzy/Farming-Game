using System;
using Assets.Scripts.Interfaces;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Crops.Factory;
using Farm.Crops.View;
using Farm.FarmLand.Enums;
using Farm.FarmLand.Interfaces;
using Farm.FarmLand.Model;
using Farm.FarmLand.Presenter;
using Farm.Inventory.Presenter;
using Farm.TimeManager.Interfaces;
using Farm.TimeManager.Event;
using UnityEngine;
using VContainer;

namespace Farm.FarmLand.View
{
    public class FarmLandView : MonoBehaviour, IInteractable, IDisposable, IFarmTile
    {
        [Header("Land Appearance")]
        [SerializeField] private Material dryLandMaterial;
        [SerializeField] private Material plowedLandMaterial;
        [SerializeField] private Material wateredLandMaterial;
        [SerializeField] private float highlightIntensity = 1.03f;


        private MeshRenderer _landRenderer;
        private Material _highLightMaterial;
        private Collider _collider;
        private const string _highlightProperty = "_Scale";
        public Collider Collider => _collider;

        private FarmLandPresenter _farmPresenter;
        private ICropFactory _cropFactory;
        private CropView _currentCrop;
        private EventSystem _eventSystem;

        [Inject]
        public void Construct(IInventoryProvider inventoryProvider, ITimeManager timeManager, EventSystem eventSystem, ICropFactory cropFactory)
        {

            _cropFactory = cropFactory;
            _eventSystem = eventSystem;
            FarmLandModel model = new FarmLandModel(inventoryProvider, timeManager);
            _farmPresenter = new FarmLandPresenter(model, this);
        }
        private void Awake()
        {
            _landRenderer = GetComponent<MeshRenderer>();
            _highLightMaterial = _landRenderer.materials[1];
            _collider = GetComponent<Collider>();
        }
        private void Start()
        {
            _eventSystem.Subscribe<ClockUpdatedEvent>(ClockUpdated);
            _farmPresenter.Start();
        }
        public void Dispose()
        {
            _farmPresenter.Dispose();
        }

        public void OnFocus()
        {
            _highLightMaterial.SetFloat(_highlightProperty, highlightIntensity);
        }

        public void OnDefocus()
        {
            _highLightMaterial.SetFloat(_highlightProperty, 0f);
        }

        public void OnInteract()
        {
            _farmPresenter.Interact();
        }

        public void InstantiateCrop(SeedSO seedData)
        {
            var landColiderExtents = _collider.bounds.extents;
            var seedPosition = new Vector3(0f, landColiderExtents.y, 0f);
            _currentCrop = _cropFactory.CreateCrop(seedData, transform, seedPosition, this);

        }
        public void WaterCrop()
        {
            if (_currentCrop != null)
            {
                _currentCrop.WaterCrop();
            }
        }

        public void HarvestCrop()
        {
            if (_currentCrop != null)
            {
                _farmPresenter.HarvestCrop();
                _currentCrop = null;
            }
        }

        public void UpdateLandAppearance(FarmLandStatus _currentStatus)
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
                    break;
            }
            _landRenderer.materials = materials;
        }

        public void ClockUpdated(ClockUpdatedEvent eventData)
        {
            _farmPresenter.ClockUpdated(eventData.GameTimeStamp);
        }
    }
}