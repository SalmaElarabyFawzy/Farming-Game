using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void OnEnable()
        {
            PlayerEvents.OnMove += UpdateMoveAnimation;
            PlayerEvents.OnJump += PlayJumpAnimation;
        }

        private void OnDisable()
        {
            PlayerEvents.OnMove -= UpdateMoveAnimation;
            PlayerEvents.OnJump -= PlayJumpAnimation;
        }

        private void UpdateMoveAnimation(Vector2 input)
        {
            float speed = input.magnitude;
            animator.SetFloat("Speed", speed);
        }

        private void PlayJumpAnimation()
        {
            animator.SetTrigger("Jump");
        }
    }
}
