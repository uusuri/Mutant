using Player;
using UnityEngine;

public class MainController : BaseController
{
        private readonly GameModel _model;
        
        private readonly Transform _placeForUI;
        private readonly Transform _playerSpawnPoint;

        private readonly MainMenuView _mainMenuView;
        private readonly PlayerView _playerView;
        
        private MainMenuController _mainMenuController;
        private GameController _gameController;

        public MainController(Transform placeForUI, Transform playerSpawnPoint, GameModel model, MainMenuView mainMenuView,
                PlayerView playerView)
        {
                _model = model;
                _placeForUI = placeForUI;
                _playerSpawnPoint = playerSpawnPoint;
                _mainMenuView = mainMenuView;
                _playerView = playerView;

                OnChangeGameState(_model.CurrentState.Value);
                _model.CurrentState.SubscribeOnChange(OnChangeGameState);
        }

        private void OnChangeGameState(GameState state)
        {
                switch (state)
                {
                        case GameState.Start:
                                _mainMenuController = new MainMenuController(_model, _mainMenuView, _placeForUI);
                                _gameController?.Dispose();
                                break;
                        
                        case GameState.Game:
                                _gameController = new GameController(_model, _playerView, _playerSpawnPoint);
                                _mainMenuController?.Dispose();
                                break;

                        case GameState.None:
                                break;
                        
                        default:
                                AllDispose();
                                break;
                }
        }

        private void AllDispose()
        {
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
        }

        protected override void OnDispose()
        {
                AllDispose();
                _model.CurrentState.UnSubscribeOnChange(OnChangeGameState);
        }
}