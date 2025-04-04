using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Level;

namespace Roguelike.UI
{
    public class LevelButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNameText;
        [SerializeField] private TextMeshProUGUI _levelDescriptionText;
        [SerializeField] private Image _levelImage;
        [SerializeField] private List<Image> _enemyImages;

        private LevelSelectionUIController owner;
        private int _levelId;
        
        private void Start() => GetComponent<Button>().onClick.AddListener(OnLevelButtonClicked);

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
        }
    }
}
