using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly GameModel _model;
    private readonly MainMenuView _view;
    
    public MainMenuController(GameModel model, MainMenuView mainMenuView, Transform placeForUI)
    {
        _model = model;
        _view = CreateView(mainMenuView, placeForUI);
        _view.ButtonStart.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        _model.CurrentState.Value = GameState.Game;
    }

    protected override void OnDispose()
    {
        _view.ButtonStart.onClick.RemoveAllListeners();
    }

    private MainMenuView CreateView(MainMenuView mainMenuView, Transform placeForUI)
    {
        var mainMenuViewInstance = Object.Instantiate(mainMenuView, placeForUI);
        AddGameObject(mainMenuViewInstance.gameObject);

        return mainMenuViewInstance;
    }
}