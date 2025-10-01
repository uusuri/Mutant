using UnityEngine;

namespace Player.Mutation
{
    public class PlayerMutationController : BaseController
    {
        private readonly PlayerView _view;
        private readonly PlayerModel _playerModel;
        private readonly GameModel _gameModel;
        private readonly BatMovementController _batMovementController;

        private bool _isBat;

        public PlayerMutationController(PlayerView playerView, GameModel gameModel)
        {
            _view = playerView;
            _playerModel = gameModel.CurrentPlayer;
            _gameModel = gameModel;
            _batMovementController = new BatMovementController(_view, _gameModel);

            _isBat = _playerModel.CurrentMutationState.Value == MutationState.Bat;
            _playerModel.CurrentMutationState.SubscribeOnChange(OnMutationChanged);
        }
        
        protected override void OnDispose()
        {
            _playerModel.CurrentMutationState.UnSubscribeOnChange(OnMutationChanged);
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                ChangeMutationState(MutationState.Bat);
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                ChangeMutationState(MutationState.Snake);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                ChangeMutationState(MutationState.Spider);
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                ChangeMutationState(MutationState.Slime);
            }
        }

        public void FixedUpdate()
        {
            if (_playerModel.CurrentMutationState.Value == MutationState.Bat)
            {
                _batMovementController.FixedUpdate();
            }
        }
        
        private void ChangeMutationState(MutationState newState)
        {
            if (_playerModel.CurrentMutationState.Value == newState) return;
            _playerModel.CurrentMutationState.Value = newState;
            ApplyMutationVisuals(newState);
        }

        private void ApplyMutationVisuals(MutationState mutationState)
        {
            _view.SetMutationSprite(mutationState);
        }

        private void OnMutationChanged(MutationState newState)
        {
            _isBat = newState == MutationState.Bat;
            if (_isBat)
            {
                _view.Rigidbody.gravityScale = 0f;
            }
        }
    }
}