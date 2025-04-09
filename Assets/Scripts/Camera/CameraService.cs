using UnityEngine;
using Roguelike.Utilities;
using Roguelike.Event;
using Roguelike.Main;
using Unity.Cinemachine;

namespace Roguelike.Camera
{
    public class CameraService : IService
    {
        private CinemachineCamera _cinemachineCamera;
        private GameState _currentGameState;

        public CameraService(CinemachineCamera cinemachineCamera) 
        {
            _cinemachineCamera = cinemachineCamera;
            SubscribeToEvents();
        }

        ~CameraService() => UnsubscribeToEvents();

        public void Initialize(params object[] dependencies)
        {
            ResetCameraPosition();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public void SetCameraTarget(GameObject targetGameObject)
        {
            if (_cinemachineCamera != null && targetGameObject != null)
            {
                _cinemachineCamera.Follow = targetGameObject.transform;
            }
            else
            {
                Debug.LogWarning("Cinemachine Virtual Camera or Target GameObject is not assigned.");
            }
        }

        public void ResetCameraPosition()
        {
            _cinemachineCamera.PreviousStateIsValid = false;
        }
    }
}

