using Roguelike.Main;
using Roguelike.Event;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.DamageNumber
{
    public class DamageNumberService : IService
    {
        private GameObject _damageNumberPrefab;
        private DamageNumberPool _damageNumberPoolObj;
        private GameState _currentGameState;
        private Transform dmgCanvasTransform;
        public DamageNumberService(GameObject damageNumberPrefab)
        {
            _damageNumberPrefab = damageNumberPrefab;
        }

        ~DamageNumberService() => UnsubscribeToEvents();

        public void Initialize(params object[] dependencies)
        {
            _damageNumberPoolObj = new DamageNumberPool(_damageNumberPrefab);
            dmgCanvasTransform = GameService.Instance.GetService<UIService>().GetDamageCanvasTransform;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
            EventService.Instance.OnMainMenu.AddListener(ResetPool);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
            EventService.Instance.OnMainMenu.RemoveListener(ResetPool);
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public void SpawnDamageNumber(Vector3 position, int damageValue)
        {
            DamageNumberController damageNumber = _damageNumberPoolObj.GetDamageNumber<DamageNumberController>(_damageNumberPrefab);
            damageNumber.Configure(position, dmgCanvasTransform, damageValue);
        }
        public void ReturnDamageNumberToPool(DamageNumberController damageNumberToReturn) => _damageNumberPoolObj.ReturnItem(damageNumberToReturn);

        private void ResetPool()
        {
            _damageNumberPoolObj.ResetPool();
        }
    }
}
