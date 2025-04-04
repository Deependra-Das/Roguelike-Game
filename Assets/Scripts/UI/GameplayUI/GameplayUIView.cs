using UnityEngine;
using UnityEngine.UI;
using Roguelike.Main;
using Roguelike.Event;
using Roguelike.Player;
using TMPro;

namespace Roguelike.UI
{
    public class GameplayUIView : MonoBehaviour, IUIView
    {
        private GameplayUIController _controller;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _experienceSlider;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _experienceText;
        [SerializeField] private TMP_Text _timerText;
        private GameState _currentGameState;

        public void SetController(IUIController controllerToSet)
        {
            _controller = controllerToSet as GameplayUIController;
        }

        public void InitializeView()
        {
            ResetTimer();
        }

        public void ResetTimer()
        {
            gameTimer = 0;
        }

        private void Update()
        {
            if (_currentGameState == GameState.Gameplay)
            {
                UpdateTimerText();
            }
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public void OnGameStateChange()
        {
            gameTimer = 0;
        }

        public void UpdateCurrentHealthSlider(float currentHealth)
        {
            _healthSlider.value = currentHealth;
            UpdateHealthText();
        }

        public void UpdateMaxHealthSlider(float maxHealth)
        {
            _healthSlider.maxValue = maxHealth;
            UpdateHealthText();
        }

        public void UpdateHealthText()
        {
            _healthText.text = _healthSlider.value + " / " + _healthSlider.maxValue;
        }

        private void UpdateTimerText()
        {
            float gameTimer = GameService.Instance.GameTimer;
            float min = Mathf.FloorToInt(gameTimer / 60f);
            float sec = Mathf.FloorToInt(gameTimer % 60f);
            _timerText.text = min.ToString("00") + ":" + sec.ToString("00");
        }

        public void UpdateCurrentExpSlider(float currentExp)
        {
            _experienceSlider.value = currentExp;
            UpdateExpText();
        }

        public void UpdateMaxExpSlider(float maxExp)
        {
            _experienceSlider.maxValue = maxExp;
            UpdateExpText();
        }

        public void UpdateExpText()
        {
            _experienceText.text = _experienceSlider.value + " / " + _experienceSlider.maxValue;
        }

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);
    }
}
