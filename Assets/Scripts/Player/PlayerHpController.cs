using UnityEngine;

namespace Player
{
    public class PlayerHpController : BaseController
    {
        private readonly GameModel _gameModel;
        private readonly PlayerTriggerHandler _triggerHandler;

        private bool _isDead;
        
        public PlayerHpController(GameModel gameModel, PlayerTriggerHandler triggerHandler)
        {
            _gameModel = gameModel;
            _triggerHandler = triggerHandler;

            _triggerHandler.OnTriggerEnter += OnTriggerEnter;
        }

        protected override void OnDispose()
        {
            _triggerHandler.OnTriggerEnter -= OnTriggerEnter;
        }

        private void OnTriggerEnter(Collider2D other)
        {
            if (_isDead) return;

            if (other.CompareTag("DeathZone"))
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            _gameModel.CurrentState.Value = GameState.Start;
        }
    }
}