using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float rotationSpeed = 10f;

        private Rigidbody rb;
        private Vector2 moveInput;
        private bool isGrounded = true;
        private bool _canMove = true;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            PlayerEvents.OnMove += HandleMove;
            PlayerEvents.OnJump += HandleJump;
        }

        private void OnDisable()
        {
            PlayerEvents.OnMove -= HandleMove;
            PlayerEvents.OnJump -= HandleJump;
        }

        private void HandleMove(Vector2 input)
        {
            if (!_canMove)
                return;
            moveInput = input;
        }

        private void HandleJump()
        {
            if (!_canMove) 
                return;
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        private void FixedUpdate()
        {
            MoveAndRotate();
        }
        private void MoveAndRotate()
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.contacts.Length > 0)
            {
                if (collision.contacts[0].normal.y > 0.5f)
                {
                    isGrounded = true;
                }
            }
        }

        public void CanMove()
        {
            _canMove = !_canMove;
        }
    }
}
