using Player;

public class GameModel
{
    public GameModel(PlayerPreset playerPreset)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentPlayer = new PlayerModel(playerPreset);                                      
    }
    public SubscriptionProperty<GameState> CurrentState { get; }
    public PlayerModel CurrentPlayer { get; }
}