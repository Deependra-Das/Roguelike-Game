using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using TMPro;

namespace Roguelike.UI
{
    public class GameOverUIView : MonoBehaviour, IUIView
    {
        private GameOverUIController _controller;
        [SerializeField] private Button _backToMainMenuButtonPrefab;
        [SerializeField] private TextMeshProUGUI _playerScore;
        [SerializeField] private TextMeshProUGUI _levelHighScore;

        public void SetController(IUIController controllerToSet)
        {
            _controller = controllerToSet as GameOverUIController;
        }

        private void OnEnable() => SubscribeToEvents();

        private void OnDisable() => UnsubscribeToEvents();

        private void SubscribeToEvents()
        {
            _backToMainMenuButtonPrefab.onClick.AddListener(OnBackToMainMenuButtonClicked);
            
        }

        private void UnsubscribeToEvents()
        {
            _backToMainMenuButtonPrefab.onClick.RemoveListener(OnBackToMainMenuButtonClicked);

        }

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);

        private void OnBackToMainMenuButtonClicked()
        {
            _controller.OnBackButtonClicked();
        }

        public void UpdateScoreData(float score, float highScore)
        {
            float min = Mathf.FloorToInt(score / 60f);
            float sec = Mathf.FloorToInt(score % 60f);
            _playerScore.text = "Your Score : " + min.ToString("00") + ":" + sec.ToString("00");
             min = Mathf.FloorToInt(highScore / 60f);
             sec = Mathf.FloorToInt(highScore % 60f);
            _levelHighScore.text = "Level High Score : " + min.ToString("00") + ":" + sec.ToString("00");
        }
    }
}