using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D player_RB;

        public PlayerController _controller { get; private set; }
        public Vector3 playerMoveDirection;

        public void SetController(PlayerController controllerToSet) => _controller = controllerToSet;

        private void Update()
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            playerMoveDirection = new Vector2(inputX, inputY).normalized;

            _controller?.UpdatePlayer();
        }

        private void FixedUpdate()
        {
            player_RB.linearVelocity = new Vector2(playerMoveDirection.x * _controller.PlayerModel.MovementSpeed,
                playerMoveDirection.y * _controller.PlayerModel.MovementSpeed);
            _controller?.FixedUpdatePlayer();
        }

        public void TakeDamage(int damage) => _controller.TakeDamage(damage);
    }
}
