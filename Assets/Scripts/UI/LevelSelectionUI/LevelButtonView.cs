using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Roguelike.Level;

namespace Roguelike.UI
{
    public class LevelButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNameText;
        [SerializeField] private TextMeshProUGUI _levelDescriptionText;
        [SerializeField] private TextMeshProUGUI _levelStatusText;

        private LevelSelectionUIController owner;
        private int _levelId;
        
        private void Start() => GetComponent<Button>().onClick.AddListener(OnLevelButtonClicked);

        public void SetOwner(LevelSelectionUIController owner) => this.owner = owner;

        private void OnLevelButtonClicked() => owner.OnLevelSelected(_levelId);

        public void SetLevelButtonData(LevelScriptableObject levelData)
        {
            _levelId = levelData.ID;
            _levelNameText.SetText(levelData.levelName);
            _levelNameText.SetText(levelData.levelDescription);
        }
    }
}
