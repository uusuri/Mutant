using JoostenProductions;
using Player.Mutation;
using UnityEngine;

namespace Player
{
    public class PlayerController : BaseController
    {
        private readonly ContactsPoller _contactsPoller;
        private readonly PlayerMoveController _playerMoveController;
        private readonly PlayerJumpController _playerJumpController;
        private readonly PlayerMutationController _playerPlayerMutationController;
        private readonly PlayerHpController _playerHpController;

        public PlayerController(PlayerView playerView, GameModel model,
            Transform placeSpawnPlayer)
        {
            var playerViewInstance = CreateView(playerView, placeSpawnPlayer);
            
            _contactsPoller = new ContactsPoller(playerViewInstance.Collider);
            
            var triggerHandler = playerViewInstance.gameObject.AddComponent<PlayerTriggerHandler>();

            _playerMoveController = new PlayerMoveController(playerViewInstance, model, _contactsPoller);
            _playerJumpController = new PlayerJumpController(playerViewInstance, model, _contactsPoller);
            _playerPlayerMutationController = new PlayerMutationController(playerViewInstance, model);
            _playerHpController = new PlayerHpController(model, triggerHandler); 
        
            AddController(_playerMoveController);
            AddController(_playerJumpController);
            AddController(_playerPlayerMutationController);
            AddController(_playerHpController);

            UpdateManager.SubscribeToFixedUpdate(FixedUpdate);
            UpdateManager.SubscribeToUpdate(Update);
        }

        protected override void OnDispose()
        {
            UpdateManager.UnsubscribeFromFixedUpdate(FixedUpdate);
            UpdateManager.UnsubscribeFromLateUpdate(Update);
        }

        private void Update()
        {
            _playerPlayerMutationController.Update();
            _playerJumpController.Update();
        }

        private void FixedUpdate()
        {
            _contactsPoller.FixedUpdate();
            _playerMoveController.FixedUpdate();
            _playerJumpController.FixedUpdate();
            _playerPlayerMutationController.FixedUpdate();
        }

        private PlayerView CreateView(PlayerView playerView, Transform placeSpawnPlayer)
        {
            var playerViewInstance = Object.Instantiate(playerView, placeSpawnPlayer);
            AddGameObject(playerViewInstance.gameObject);

            return playerViewInstance;
        }
    }
}