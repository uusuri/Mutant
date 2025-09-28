using JoostenProductions;
using Player;
using Player.Mutation;
using UnityEngine;

public class MutationController : BaseController
{
    private readonly PlayerView _playerView;
    private readonly PlayerModel _playerModel;
    private readonly GameModel _gameModel;

    public MutationController(PlayerView playerView, GameModel gameModel)
    {
        _playerView = playerView;
        _gameModel = gameModel;
        _playerModel = gameModel.CurrentPlayer;
        
        UpdateManager.SubscribeToUpdate(Update);
    }

    protected override void OnDispose()
    {
        UpdateManager.UnsubscribeFromUpdate(Update);
    }

    private void Update()
    {
        HandleMutationInput();
    }

    private void HandleMutationInput()
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

    private void ChangeMutationState(MutationState newState)
    {
        if (_playerModel.CurrentMutationState.Value != newState)
        {
            _playerModel.CurrentMutationState.Value = newState;
            ApplyMutationVisuals(newState);
        }
    }

    private void ApplyMutationVisuals(MutationState mutationState)
    {
        _playerView.SetMutationSprite(mutationState);
    }
}
