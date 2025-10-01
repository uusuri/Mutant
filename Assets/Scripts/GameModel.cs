using Player;
using Player.Mutation;

public class GameModel
{
    public GameModel(PlayerPreset playerPreset, BatPreset batPreset)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentPlayer = new PlayerModel(playerPreset);
        BatPreset = batPreset;
    }
    public SubscriptionProperty<GameState> CurrentState { get; }
    public PlayerModel CurrentPlayer { get; }
    public BatPreset BatPreset { get; }
}