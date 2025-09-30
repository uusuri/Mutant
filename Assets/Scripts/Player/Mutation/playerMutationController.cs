using UnityEngine;

namespace Player.Mutation
{
    public class PlayerMutationController : BaseController
    {
        private readonly PlayerView _view;
        private readonly PlayerModel _playerModel;
        private readonly GameModel _gameModel;

        private bool _isBat;

        public PlayerMutationController(PlayerView playerView, GameModel gameModel)
        {
            _view = playerView;
            _playerModel = gameModel.CurrentPlayer;
            _gameModel = gameModel;

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
            if (_playerModel.CurrentMutationState.Value != MutationState.Bat)
                return;

            var xAxisInput = Input.GetAxis("Horizontal");
            var yAxisInput = Input.GetAxis("Vertical");

            var isGoSideWay = Mathf.Abs(xAxisInput) > _playerModel.MoveThreshold;
            var isGoUpDown = Mathf.Abs(yAxisInput) > _playerModel.MoveThreshold;

            if (isGoSideWay)
                _view.SpriteRenderer.flipX = xAxisInput > 0;

            var newVelocityX = 0f;
            var newVelocityY = 0f;

            if (isGoSideWay)
                newVelocityX = _gameModel.BatPreset.FlySpeed * (xAxisInput < 0 ? -1 : 1);

            if (isGoUpDown)
                newVelocityY = _gameModel.BatPreset.FlySpeed * (yAxisInput < 0 ? -1 : 1);

            _view.Rigidbody.gravityScale = 0f;
            _view.Rigidbody.linearVelocity = new Vector2(newVelocityX, newVelocityY);
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