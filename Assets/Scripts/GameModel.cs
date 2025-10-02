using Player;
using Player.Mutation;

public class GameModel
{
    public GameModel(PlayerPreset playerPreset, BatPreset batPreset, SpiderPreset spiderPreset)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentPlayer = new PlayerModel(playerPreset);
        BatPreset = batPreset;
        SpiderPreset = spiderPreset;
    }
    public SubscriptionProperty<GameState> CurrentState { get; }
    public PlayerModel CurrentPlayer { get; }
    public BatPreset BatPreset { get; }
    public SpiderPreset SpiderPreset { get; }
}