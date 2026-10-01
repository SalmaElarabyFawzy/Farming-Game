

using System;
using Assets.Scripts.Interfaces;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.Crops.View;
using Farm.FarmLand.Enums;
using Farm.FarmLand.Model;
using Farm.FarmLand.Presenter;
using Farm.Inventory.Presenter;
using UnityEngine;
using VContainer;

namespace Farm.FarmLand.View
{
    public class FarmLandView : MonoBehaviour, IInteractable, IDisposable   
    {
        [Header("Land Appearance")]
        [SerializeField] private Material dryLandMaterial;
        [SerializeField] private Material plowedLandMaterial;
        [SerializeField] private Material wateredLandMaterial;
        [SerializeField] private float highlightIntensity = 1.03f;

        [Header("Crop")]
        [SerializeField] private GameObject cropPrefab;

        private MeshRenderer _landRenderer;
        private Material _highLightMaterial;
        private Collider _collider;
        private const string _highlightProperty = "_Scale";
        public Collider Collider => _collider;

        private FarmLandPresenter _farmPresenter;


        [Inject]
        public void Construct(IInventoryProvider inventoryProvider, EventSystem eventSystem)
        {
            FarmLandModel model = new FarmLandModel(inventoryProvider);
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
           // _farmPresenter.Interact();
        }

        public void InstantiateCrop(SeedSO seedData)
        {
            GameObject cropInstance = Instantiate(cropPrefab, transform);
            Vector3 plantPos = _collider.bounds.extents;
            cropInstance.transform.localPosition = new Vector3(0, plantPos.y, 0);
            cropInstance.GetComponent<CropView>().Plant(seedData);
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
                    // _timeLandWatered = TimeManager.instance.GetTimeStamp();
                    break;
            }
            _landRenderer.materials = materials;
        }
    }
}