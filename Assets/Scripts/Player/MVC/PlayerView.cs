using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerView : MonoBehaviour
    {
        public Rigidbody playerRigidbody { get; private set; }

        public PlayerController _controller { get; private set; }

        private void Start() => playerRigidbody = GetComponent<Rigidbody>();

        public void SetController(PlayerController controllerToSet) => _controller = controllerToSet;

        private void Update() => _controller?.UpdatePlayer();

        private void FixedUpdate() => _controller?.FixedUpdatePlayer();

        public void TakeDamage(int damage) => _controller.TakeDamage(damage);
    }
}
