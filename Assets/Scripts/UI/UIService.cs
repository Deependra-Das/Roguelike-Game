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

    [Header("Level Selection UI")]
    private LevelSelectionUIController _levelSelectionController;
    [SerializeField] private LevelSelectionUIView _levelSelectionView;
    [SerializeField] private LevelButtonView _levelButtonPrefab;
    [SerializeField] private List<LevelButtonScriptableObject> _levelButton_SO;

    private void Awake()
    {
        _mainMenuUIController = new MainMenuUIController(_mainMenuUIView);
        _levelSelectionController = new LevelSelectionUIController(_levelSelectionView, _levelButtonPrefab, _levelButton_SO);
    }

    public void Initialize(params object[] dependencies)
    {
        _mainMenuUIController.InitializeController();
        _levelSelectionController.InitializeController();
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
