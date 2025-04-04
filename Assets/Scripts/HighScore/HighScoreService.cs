using Roguelike.UI;
using Roguelike.Utilities;
using Roguelike.Event;
using Roguelike.Main;
using UnityEngine;
using Roguelike.Level;

namespace Roguelike.HighScore
{
    public class HighScoreService : IService
    {
        public HighScoreService() { }

        ~HighScoreService() => UnsubscribeToEvents();

        public void Initialize(params object[] dependencies) 
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnSaveHighScore.AddListener(SaveHighScore);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnSaveHighScore.RemoveListener(SaveHighScore);
        }

        public void SaveHighScore()
        {
            int levelId = GameService.Instance.GetService<LevelService>().LevelIdSelected;
            float score = GameService.Instance.GameTimer;
            float currentHighScore = GetHighScore(levelId);
            EventService.Instance.OnSetFinalScore.Invoke(score, currentHighScore);
            if (score > currentHighScore)
            {
                PlayerPrefs.SetFloat(levelId + "_HighScore", score);
                PlayerPrefs.Save();
            }
        }

        public float GetHighScore(int levelId)
        {
            return PlayerPrefs.GetFloat(levelId + "_HighScore", 0f);
        }

    }
}