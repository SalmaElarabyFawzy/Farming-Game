using Assets.Scripts.Interfaces;
using Farm.Enums;
using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Assets.Scripts.Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        [SerializeField] private Transform interactPoint;
        [SerializeField] private float interactDistance = 2f;
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private PlayableDirector director;
        [SerializeField] private PlayableAsset plowCutscene;
        [SerializeField] private PlayableAsset waterCutscene;
        [SerializeField] private PlayableAsset harvestCutscene;
        [SerializeField] private PlayableAsset plantCutscene;
        private IInteractable currentInteractable;

        private bool _canInteract = true;
        private void OnEnable()
        {
            PlayerEvents.OnInteract += HandleInteract;
        }
        private void OnDisable()
        {
            PlayerEvents.OnInteract -= HandleInteract;
        }

        private void HandleInteract()
        {
            if (!_canInteract)
                    return;
            if (currentInteractable == null)
            {
                Debug.Log("null interactable");
                return;
            }
            if(!CanPlayCutScene())
                DoInteraction();

        }
        private bool CanPlayCutScene()
        {
            PlayableAsset playableAsset = null;
            if (currentInteractable is HarvestableCrop)
                playableAsset = harvestCutscene;

            else if (currentInteractable is FarmLand)
            {
                ItemSO equipedTool = InventoryManager.Instance.EquipedTool;
                if (equipedTool != null)
                {
                    EquipmentSO tool = equipedTool as EquipmentSO;
                    SeedSO seed = equipedTool as SeedSO;
                    if (tool != null)
                    {
                        if (tool.equipmentType == EquipmentType.Hoe)
                            playableAsset = plowCutscene;
                        else if (tool.equipmentType == EquipmentType.WateringCan)
                            playableAsset = waterCutscene;
                    }
                    else if(seed != null)
                        playableAsset = plantCutscene;
                }
            }
            if (playableAsset != null)
            {
                director.playableAsset = playableAsset;
                director.Play();
            }
            return playableAsset != null;
        }
        public void DoInteraction()
        {
            currentInteractable.OnInteract();
        }
        private void Update()
        {
            CheckForInteractable();
        }
        private void CheckForInteractable()
        {
            if (Physics.Raycast(interactPoint.position, -interactPoint.up, out RaycastHit hit, interactDistance, interactableLayer))
            {
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    if(currentInteractable != null)
                        currentInteractable.OnDefocus();
                 
                    currentInteractable = interactable;
                    interactable.OnFocus();
                }
            }
            else
            {
                if (currentInteractable != null)
                    currentInteractable.OnDefocus();
                currentInteractable = null;

            }
        }
        public void CanInteract()
        {
            _canInteract = !_canInteract;
        }
    }
}
