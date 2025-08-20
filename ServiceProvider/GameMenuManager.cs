using UnityEngine;

public class GameMenuManager : IMenuManager
{
    private GameMenuButtonReference _gameMenuRef;
    private IServiceLocator _serviceLocator;

    public GameMenuManager()
    {
    }

    public void Initialize(IServiceLocator serviceLocator)
    {
        _serviceLocator = serviceLocator;
        if (_serviceLocator.IsRegistered<IMenuManager>())
            _serviceLocator.Unregister<IMenuManager>();
        _serviceLocator.Register<IMenuManager>(this);
        GameMenuButtonReference.ButtonRefsLoaded += OnButtonRefsLoaded;
        ScoreButtonReference.ButtonRefsLoaded += OnScoreButtonRefsLoaded;
        Debug.Log("GameMenuManager initialized");
    }

    public void Dispose()
    {
        _serviceLocator.Unregister<IMenuManager>();
    }

    public void SwitchToMainMenuManager()
    {
        Dispose();
        new MainMenuManager().Initialize(_serviceLocator);
        ServiceScopeProvider.Instance.Scope.TryGetService(out SceneLoader sceneLoader);
        sceneLoader.LoadScene("MainMenu");
    }

    // This is used for instanciation
    public void OnButtonRefsLoaded(GameMenuButtonReference gameMenuRef)
    {
        _gameMenuRef = gameMenuRef;
        _gameMenuRef.GoBackButton.onClick.AddListener(
            () => SwitchToMainMenuManager());
    }
    public void OnScoreButtonRefsLoaded(ScoreButtonReference scoreButtonReference)
    {
        scoreButtonReference.GoBackButton.onClick.AddListener(
            () => SwitchToMainMenuManager());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}