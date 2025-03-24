using System.Collections.Generic;
using UnityEngine;
using Roguelike.Main;
using Roguelike.Player;
using Roguelike.Utilities;
using Roguelike.UI;
using Roguelike.Level;

public class UIService : MonoBehaviour,IService
{
    [Header("Main Menu UI")]
    private MainMenuUIController _mainMenuUIController;
    [SerializeField] private MainMenuUIView _mainMenuUIView;

    [Header("Level Selection UI")]
    private LevelSelectionUIController _levelSelectionController;
    [SerializeField] private LevelSelectionUIView _levelSelectionView;
    [SerializeField] private LevelButtonView _levelButtonPrefab;
    [SerializeField] private List<LevelScriptableObject> _level_SO;

    [Header("Character Selection UI")]
    private CharacterSelectionUIController _characterSelectionController;
    [SerializeField] private CharacterSelectionUIView _characterSelectionView;
    [SerializeField] private CharacterButtonView _characterButtonPrefab;
    [SerializeField] private List<PlayerScriptableObject> _player_SO;

    private void Awake()
    {
        _mainMenuUIController = new MainMenuUIController(_mainMenuUIView);
        _levelSelectionController = new LevelSelectionUIController(_levelSelectionView, _levelButtonPrefab, _level_SO);
        _characterSelectionController = new CharacterSelectionUIController(_characterSelectionView, _characterButtonPrefab, _player_SO);
    }

    public void Initialize(params object[] dependencies)
    {
        _mainMenuUIController.InitializeController();
        _levelSelectionController.InitializeController();
        _characterSelectionController.InitializeController();
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
