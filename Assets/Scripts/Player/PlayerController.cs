using JoostenProductions;
using Player;
using Player.Mutation;
using UnityEngine;

public class PlayerController : BaseController
{
    private readonly ContactsPoller _contactsPoller;
    private readonly PlayerMoveController _playerMoveController;
    private readonly PlayerJumpController _playerJumpController;
    private readonly MutationController _mutationController;

    public PlayerController(PlayerView playerView, GameModel model,
        Transform placeSpawnPlayer)
    {
        var playerViewInstance = CreateView(playerView, placeSpawnPlayer);

        _contactsPoller = new ContactsPoller(playerViewInstance.Collider);

        _playerMoveController = new PlayerMoveController(playerViewInstance, model, _contactsPoller);
        _playerJumpController = new PlayerJumpController(playerViewInstance, model, _contactsPoller);
        _mutationController = new MutationController(playerViewInstance, model);
        
        AddController(_playerMoveController);
        AddController(_playerJumpController);
        AddController(_mutationController);

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
        _mutationController.Update();
        _playerJumpController.Update();
    }

    private void FixedUpdate()
    {
        _contactsPoller.FixedUpdate();
        _playerMoveController.FixedUpdate();
        _playerJumpController.FixedUpdate();
        _mutationController.FixedUpdate();
    }

    private PlayerView CreateView(PlayerView playerView, Transform placeSpawnPlayer)
    {
        var playerViewInstance = Object.Instantiate(playerView, placeSpawnPlayer);
        AddGameObject(playerViewInstance.gameObject);

        return playerViewInstance;
    }
}