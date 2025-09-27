// using ObjectPool;

using Player;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField]
    private  Transform _placeForUI;
    [SerializeField]
    private  Transform _playerSpawnPoint;

    [SerializeField]
    private  MainMenuView _mainMenuView;
    [SerializeField]
    private  PlayerView _playerView;

    // [SerializeField]
    // private PrefabPool _prefabPool;

    // [SerializeField] 
    // private CoinsSystem _coinsSystem;

    [SerializeField]
    private PlayerPreset _playerPreset;

    private MainController _mainController;

    private void Awake()
    {
        var gameModel = new GameModel(_playerPreset)
        {
            CurrentState =
            {
                Value = GameState.Start
            }
        };

        //_coinsSystem.Init(_prefabPool);
        _mainController = new MainController(_placeForUI, _playerSpawnPoint, gameModel, _mainMenuView, _playerView);
    }

    private void OnDestroy() 
    {
        _mainController?.Dispose();
    }
}