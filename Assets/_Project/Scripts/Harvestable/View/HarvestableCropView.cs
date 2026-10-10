using Assets.Scripts.Interfaces;
using Farm.Core.DesignPatterns.EventSystem;
using Farm.FarmLand.Interfaces;
using Farm.HarvestableCrop.Model;
using Farm.HarvestableCrop.Presenter;
using Farm.Inventory.Presenter;
using UnityEngine;
using VContainer;

namespace Farm.HarvestableCrop.View
{
    public class HarvestableCropView : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemSO cropItem;
        [SerializeField] private float highlightIntensity = 1.03f;
        [SerializeField] private MeshRenderer cropRenderer;

        private Material _highLightMaterial;
        private const string _highlightProperty = "_Scale";

        private HarvestableCropPresenter _presenter;

        [Inject]
        public void Construct(EventSystem eventSystem)
        {
            var model = new HarvestableCropModel(cropItem, eventSystem);
            _presenter = new HarvestableCropPresenter(model, this);
            _presenter.Start();
        }

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
        public void SetFarmTile(IFarmTile farmTile)
        {
           _presenter.SetFarmTile(farmTile);
        }

        public void OnInteract()
        {
            _presenter.HarvestCrop();
        }
        public void DestroyHarvestableCrop()
        {
            Destroy(gameObject);
        }

    }
}