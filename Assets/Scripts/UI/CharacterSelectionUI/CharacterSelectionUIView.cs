using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Roguelike.UI;

public class CharacterSelectionUIView : MonoBehaviour, IUIView
{
    private CharacterSelectionUIController _controller;
    [SerializeField] private Transform _characterButtonContainer;

    public void SetController(IUIController controllerToSet) => _controller = controllerToSet as CharacterSelectionUIController;

    public void DisableView() => gameObject.SetActive(false);

    public void EnableView() => gameObject.SetActive(true);

    public CharacterButtonView AddButton(CharacterButtonView characterButtonPrefab) => Instantiate(characterButtonPrefab, _characterButtonContainer);

}
