using UnityEngine;
using UnityEngine.UI;
using Roguelike.Main;
using Roguelike.Event;

namespace Roguelike.UI
{
    public class GameplayUIView : MonoBehaviour, IUIView
    {
        private GameplayUIController controller;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _experienceSlider;

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as GameplayUIController;
        }

        public void InitializeView()
        {
        }

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);

    }
}
