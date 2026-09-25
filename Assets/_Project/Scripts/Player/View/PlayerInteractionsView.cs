using Assets.Scripts.Interfaces;
using Farm.Enums;
using System;
using UnityEngine;
using UnityEngine.Playables;
using Farm.Player.Events;
using Farm.Core.DesignPatterns.EventSystem;

namespace Farm.Player.View
{
    public class PlayerInteractionsView : MonoBehaviour
    {
        [SerializeField] private Transform interactPoint;
        [SerializeField] private float interactDistance = 2f;
        [SerializeField] private LayerMask interactableLayer;  
        
        public IInteractable CheckForInteractable()
        {
            if (Physics.Raycast(interactPoint.position, -interactPoint.up, out RaycastHit hit, interactDistance, interactableLayer))
            {
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                return interactable;
            }
            return null;
        }
    }
}
