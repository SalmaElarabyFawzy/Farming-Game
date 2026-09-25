using UnityEngine;
using UnityEngine.InputSystem;
using Farm.Player.Events;
using VContainer;
using Farm.Core.DesignPatterns.EventSystem;

namespace Farm.Player.View
{
    public class PlayerInputHandlerView : MonoBehaviour
    {
        private EventSystem _eventSystem;
        [Inject]
        public void Construct(EventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

        public void OnMove(InputValue value)
        {
            Vector2 input = value.Get<Vector2>();
            _eventSystem.Publish(new PlayerMoveEvent(input));
        }

        public void OnJump(InputValue value)
        {
            if (value.isPressed)
            {
                _eventSystem.Publish(new PlayerJumpEvent());
            }
        }
        public void OnInteract(InputValue value)
        {
            if (value.isPressed)
            {
                _eventSystem.Publish(new PlayerInteractEvent());
            }
        }
    }
}
