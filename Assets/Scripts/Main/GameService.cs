using UnityEngine;
using Roguelike.Utilities;

namespace Roguelike.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            Debug.Log("GameService Running");
        }

    }

}
