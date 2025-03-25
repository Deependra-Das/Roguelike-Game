using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _player_RB;
        [SerializeField] private Animator _playerAnimator;

        public PlayerController _controller { get; private set; }
        public Vector3 playerMoveDirection;

        public void SetController(PlayerController controllerToSet) => _controller = controllerToSet;

        private void Update()
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            playerMoveDirection = new Vector3(inputX, inputY).normalized;

            _playerAnimator.SetFloat("moveX", inputX);
            _playerAnimator.SetFloat("moveY", inputY);

            if(playerMoveDirection == Vector3.zero)
            {
                _playerAnimator.SetBool("moving",false);
            }
            else
            {
                _playerAnimator.SetBool("moving", true);
            }

            _controller?.UpdatePlayer();
        }

        private void FixedUpdate()
        {
            _player_RB.linearVelocity = new Vector2(playerMoveDirection.x * _controller.PlayerModel.MovementSpeed,
                playerMoveDirection.y * _controller.PlayerModel.MovementSpeed);
            _controller?.FixedUpdatePlayer();
        }

        public void TakeDamage(int damage) => _controller.TakeDamage(damage);
    }
}
