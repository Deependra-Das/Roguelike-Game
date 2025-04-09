using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Roguelike.UI;

public class LevelSelectionUIView : MonoBehaviour
{
    private LevelSelectionUIController _controller;
    [SerializeField] private Transform _levelButtonContainer;

    public void SetController(IUIController controllerToSet) => _controller = controllerToSet as LevelSelectionUIController;

    public void DisableView() => gameObject.SetActive(false);

    public void EnableView() => gameObject.SetActive(true);

    public LevelButtonView AddButton(LevelButtonView levelButtonPrefab) => Instantiate(levelButtonPrefab, _levelButtonContainer);

}
