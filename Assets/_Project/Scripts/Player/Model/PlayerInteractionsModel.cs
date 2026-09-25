using Assets.Scripts.Interfaces;
using Farm.Core.DesignPatterns.EventSystem;

namespace Farm.Player.Model
{
    public class PlayerInteractionsModel
    {
        private IInteractable currentInteractable;
        public IInteractable CurrentInteractable  => currentInteractable;

        private EventSystem _eventSystem;

        public PlayerInteractionsModel(EventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

        public void FocusOnInteractable(IInteractable interactable)
        {
            if(currentInteractable != null && currentInteractable != interactable)
            {
                currentInteractable.OnDefocus();
            }
            currentInteractable = interactable;
            if(currentInteractable != null)
            {
                currentInteractable.OnFocus();
            }
        }

        public void InteractWithCurrentInteractable()
        {
            if(currentInteractable != null)
            {
                currentInteractable.OnInteract();
            }
        }
    }
}