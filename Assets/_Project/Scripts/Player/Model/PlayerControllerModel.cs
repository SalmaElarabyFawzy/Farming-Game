using Farm.Core.DesignPatterns.EventSystem;
using UnityEngine;

namespace Farm.Player.Model
{
    public class PlayerControllerModel
    {
        private Vector2 _moveInput;
        private bool _canMove = true;

        private readonly EventSystem _eventSystem;

        public PlayerControllerModel(EventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

        public bool CanMove
        {
            get => _canMove;
            set => _canMove = value;
        }
        public Vector2 MoveInput
        {
            get => _moveInput;
            set => _moveInput = value;
        }

        public void MoveAndRotate(Transform cameraTransform, Rigidbody rb, float moveSpeed, float rotationSpeed)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 moveDirection = forward * _moveInput.y + right * _moveInput.x;

            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

                rb.rotation = Quaternion.Slerp(
                    rb.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime
                );
            }
        }
        public void Jump(Rigidbody rb, float jumpForce)
        {
            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}