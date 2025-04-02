using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Roguelike.Player;
using Roguelike.Weapon;
using Roguelike.PowerUp;

namespace Roguelike.UI
{
    public class HealingButtonView : MonoBehaviour
    {
        private PowerUpSelectionUIController owner;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnHealingButtonClicked);
        }

        public void SetOwner(PowerUpSelectionUIController owner) => this.owner = owner;

        public void InitializeView()
        {
            SetHealingButtonData();
        }

        public void SetHealingButtonData()
        {
        }

        private void OnHealingButtonClicked() 
        {
            owner.OnHealingSelected();
        }

        public void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveListener(OnHealingButtonClicked);
            Destroy(gameObject);
        }
    }
}
