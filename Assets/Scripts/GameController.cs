using UnityEngine;

public class GameController : BaseController
{
    public GameController(GameModel model, PlayerView playerView,
        Transform playerSpawnPoint)
    {
        var playerController = new PlayerController(playerView, model, playerSpawnPoint);
        AddController(playerController);
    }
}