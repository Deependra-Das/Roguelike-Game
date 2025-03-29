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

        private float gameTimer;
        private bool isGamePaused;
        private bool isGameOver;

        public void SetController(IUIController controllerToSet)
        {
            _controller = controllerToSet as GameplayUIController;
        }

        public void InitializeView()
        {
            isGamePaused = false;
            isGameOver = false;
        }

        public void ResetTimer()
        {
            gameTimer = 0;
        }

        private void Update()
        {
            if (!isGamePaused && !isGameOver)
            {
                gameTimer += Time.deltaTime;
                UpdateHealthSlider();
                UpdateTimerText();
            }
        }

        private void UpdateHealthSlider()
        {
            _healthSlider.value = _controller.GetPlayerCurrentHealth();
            _healthSlider.maxValue = _controller.GetPlayerMaxHealth();
            _healthText.text = _healthSlider.value + " / " + _healthSlider.maxValue;
        }

        private void UpdateTimerText()
        {
            float min = Mathf.FloorToInt(gameTimer / 60f);
            float sec = Mathf.FloorToInt(gameTimer % 60f);
            _timerText.text = min.ToString("00") + ":" + sec.ToString("00");
        }

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);

        public void OnGamePause(bool value) => isGamePaused=value;

        public void OnGameOver(bool value) => isGameOver = value;

    }
}
