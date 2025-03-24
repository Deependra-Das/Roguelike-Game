using System.Collections.Generic;
using UnityEngine;
using Roguelike.Main;
using Roguelike.Utilities;
using Roguelike.UI;

public class UIService : MonoBehaviour,IService
{
    [Header("Main Menu UI")]
    private MainMenuUIController _mainMenuUIController;
    [SerializeField] private MainMenuUIView _mainMenuUIView;

    private void Awake()
    {
        _mainMenuUIController = new MainMenuUIController(_mainMenuUIView);
    }

    public void Initialize(params object[] dependencies)
    {
        _mainMenuUIController.InitializeController();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
    }

    private void UnsubscribeToEvents()
    {
    }

    private void OnDestroy() => UnsubscribeToEvents();
}
