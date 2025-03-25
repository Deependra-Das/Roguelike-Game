using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Roguelike.UI;

public class PowerUpSelectionUIView : MonoBehaviour, IUIView
{
    private PowerUpSelectionUIController _controller;
    [SerializeField] private Transform _powerUpButtonContainer;

    public void SetController(IUIController controllerToSet) => _controller = controllerToSet as PowerUpSelectionUIController;

    public void DisableView() => gameObject.SetActive(false);

    public void EnableView() => gameObject.SetActive(true);

    public PowerUpButtonView AddButton(PowerUpButtonView powerUpButtonPrefab) => Instantiate(powerUpButtonPrefab, _powerUpButtonContainer);

}
