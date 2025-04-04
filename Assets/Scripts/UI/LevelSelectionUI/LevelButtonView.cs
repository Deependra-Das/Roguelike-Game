using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Level;
using Roguelike.Main;
using Roguelike.HighScore;

namespace Roguelike.UI
{
    public class LevelButtonView : MonoBehaviour
    {
        [SerializeField] private Button _levelButton;
        [SerializeField] private TextMeshProUGUI _levelNameText;
        [SerializeField] private TextMeshProUGUI _levelDescriptionText;
        [SerializeField] private TextMeshProUGUI _levelHighScoreText;
        [SerializeField] private Image _levelImage;
        [SerializeField] private List<Image> _enemyImages;

        private LevelSelectionUIController owner;
        private int _levelId;
        
        private void OnEnable() => _levelButton.onClick.AddListener(OnLevelButtonClicked);

        private void OnDisable() => _levelButton.onClick.RemoveListener(OnLevelButtonClicked);

        public void SetOwner(LevelSelectionUIController owner) => this.owner = owner;

        private void OnLevelButtonClicked() => owner.OnLevelSelected(_levelId);

        public void SetLevelButtonData(LevelScriptableObject levelData)
        {
            _levelId = levelData.ID;
            _levelNameText.SetText(levelData.levelName);
            _levelDescriptionText.SetText(levelData.levelDescription);
            _levelImage.sprite = levelData.levelImage;
            for(int i=0;i<levelData.enemyWaveData.Count;i++)
            {
                _enemyImages[i].sprite = levelData.enemyWaveData[i].enemy_SO.enemyImage;
            }
            SetHighScore();
        }

        public void SetHighScore()
        {
            float highScore = GameService.Instance.GetService<HighScoreService>().GetHighScore(_levelId);
            float min = Mathf.FloorToInt(highScore / 60f);
            float sec = Mathf.FloorToInt(highScore % 60f);
            _levelHighScoreText.text = "High Score : "+min.ToString("00") + ":" + sec.ToString("00");
        }
    }
}
