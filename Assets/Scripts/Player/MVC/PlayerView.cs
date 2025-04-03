using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _player_RB;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] public Transform  playerWeaponTransform;
        private PlayerController _controller;
        public Vector3 playerMoveDirection;
        private GameState _currentGameState;
        private bool _isImmune;
        private float _immunityTimer;

        public void SetController(PlayerController controllerToSet) => _controller = controllerToSet;

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        private void Update()
        {
            if(_currentGameState==GameState.Gameplay)
            {
                float inputX = Input.GetAxisRaw("Horizontal");
                float inputY = Input.GetAxisRaw("Vertical");

                playerMoveDirection = new Vector3(inputX, inputY).normalized;

                _playerAnimator.SetFloat("moveX", inputX);
                _playerAnimator.SetFloat("moveY", inputY);

                if (playerMoveDirection == Vector3.zero)
                {
                    _playerAnimator.SetBool("moving", false);
                }
                else
                {
                    _playerAnimator.SetBool("moving", true);
                }

                if(_immunityTimer>0)
                {
                    _immunityTimer-=Time.deltaTime;
                }
                else
                {
                    _isImmune = false;
                }
                _controller?.UpdatePlayer();
            }         
        }

        private void FixedUpdate()
        {
            if (_currentGameState == GameState.Gameplay)
            {
                _player_RB.linearVelocity = new Vector2(playerMoveDirection.x * _controller.PlayerModel.MovementSpeed,
                playerMoveDirection.y * _controller.PlayerModel.MovementSpeed);
            }
        }

        public void TakeDamage(int damage)
        {
            if(!_isImmune)
            {
                _isImmune = true;
                _immunityTimer=_controller.PlayerModel.ImmunityDuration;
                _controller.TakeDamage(damage);
            }
           
        }

        public void OnPlayerDeath()
        {
            Destroy(this.gameObject);
        }

    }
}
