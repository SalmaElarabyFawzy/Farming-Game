using UnityEngine;
using Farm.Player.Events;

namespace Farm.Player.View
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerControllerView : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float rotationSpeed = 10f;

        private Rigidbody rb;
        public bool IsGrounded { get; set; } = true;
        public Transform CameraTransform => cameraTransform;
        public float MoveSpeed => moveSpeed;
        public float JumpForce => jumpForce;
        public float RotationSpeed => rotationSpeed;
        public Rigidbody Rb => rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.contacts.Length > 0)
            {
                if (collision.contacts[0].normal.y > 0.5f)
                {
                    IsGrounded = true;
                }
            }
        }
    }
}
