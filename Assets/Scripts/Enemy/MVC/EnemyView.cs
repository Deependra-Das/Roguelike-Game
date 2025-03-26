using UnityEngine;

namespace Roguelike.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _enemy_RB;
        [SerializeField] private Animator _enemyAnimator;

        public EnemyController _controller { get; private set; }

        public void SetController(EnemyController controllerToSet) => _controller = controllerToSet;
    }
}