// using ObjectPool;

using Player;
using Player.Mutation;
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
    [SerializeField]
    private BatPreset _batPreset;
    [SerializeField]
    private SpiderPreset _spiderPreset;

    private MainController _mainController;

    private void Awake()
    {
        if (_playerView == null)
        {
            Debug.LogError("PlayerView is not assigned in Root component! Please assign the Slime prefab to the _playerView field in the inspector.");
            return;
        }
        
        if (_playerPreset == null)
        {
            Debug.LogError("PlayerPreset is not assigned in Root component! Please assign a PlayerPreset to the _playerPreset field in the inspector.");
            return;
        }

        var gameModel = new GameModel(_playerPreset, _batPreset, _spiderPreset)
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