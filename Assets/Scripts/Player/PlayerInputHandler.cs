using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public void OnMove(InputValue value)
        {
            Vector2 input = value.Get<Vector2>();
            PlayerEvents.OnMove?.Invoke(input);
        }

        public void OnJump(InputValue value)
        {
            if (value.isPressed)
            {
                PlayerEvents.OnJump?.Invoke();
            }
        }
        public void OnInteract(InputValue value)
        {
            if (value.isPressed)
            {
                PlayerEvents.OnInteract?.Invoke();
            }
        }
    }
}
