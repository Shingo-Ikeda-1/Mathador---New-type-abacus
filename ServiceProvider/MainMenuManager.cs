using UnityEngine;

public class MainMenuManager : IMenuManager
{
    private MainMenuButtonReference _mainMenuRef;
    private IStageLoader stageLoader;
    private GameObject _currentPanel;
    private IServiceLocator _serviceLocator;

    public MainMenuManager()
    {
        MainMenuButtonReference.ButtonRefsLoaded += OnButtonRefsLoaded;
    }

    public void Initialize(IServiceLocator serviceLocator)
    {
        _serviceLocator = serviceLocator;
        if (_serviceLocator.IsRegistered<IMenuManager>())
            _serviceLocator.Unregister<IMenuManager>();
        _serviceLocator.Register<IMenuManager>(this);
        Debug.Log("MainMenuManager initialized");
    }

    public void Dispose()
    {
        MainMenuButtonReference.ButtonRefsLoaded -= OnButtonRefsLoaded;
        _serviceLocator.Unregister<IMenuManager>();
    }

    public void SwitchToGameMenuManager()
    {
        Dispose();
        new GameMenuManager().Initialize(_serviceLocator);
    }

    public void OnButtonRefsLoaded(MainMenuButtonReference mainMenuRef)
    {
        _mainMenuRef = mainMenuRef;
        _mainMenuRef.mainMenuPanel.SetActive(true);
        _currentPanel = _mainMenuRef.mainMenuPanel;
        _mainMenuRef.difficultySelectionPanel.SetActive(false);

        ServiceScopeProvider.Instance.Scope.TryGetService(out stageLoader);
        mainMenuRef.additionGameButton.onClick.AddListener(
            () => CalculationTypeSelected(CalculationType.Addition));
        mainMenuRef.subtractionGameButton.onClick.AddListener(
            () => CalculationTypeSelected(CalculationType.Subtraction));
        mainMenuRef.multiplicationGameButton.onClick.AddListener(
            () => CalculationTypeSelected(CalculationType.Multiplication));
        //mainMenuInfo.divisionGameButton.onClick.AddListener(
        //    () => CalculationTypeSelected(CalculationType.Division));

        mainMenuRef.difficultyButton1.onClick.AddListener(
            () => DifficultySelected(Difficulty.Easy));
        mainMenuRef.difficultyButton2.onClick.AddListener(
            () => DifficultySelected(Difficulty.Medium));
        mainMenuRef.difficultyButton3.onClick.AddListener(
            () => DifficultySelected(Difficulty.Hard));
        mainMenuRef.settingsButton.onClick.AddListener(
            () => SwitchPanelTo(_mainMenuRef.settingsPanel));
        mainMenuRef.quitAppButton.onClick.AddListener(
            () => QuitGame());
    }


    private void CalculationTypeSelected(CalculationType calculationType)
    {
        stageLoader.NextGameCalculationType = calculationType;
        SwitchPanelTo(_mainMenuRef.difficultySelectionPanel);
    }

    private void DifficultySelected(Difficulty difficulty)
    {
        SwitchToGameMenuManager(); // has to be called before stageLoader.LoadStage(), otherwise IMenuManager instance will not exist when GameMenuManager tries to load it
        stageLoader.NextGameDifficulty = difficulty;
        stageLoader.LoadStage();
    }

    public void SwitchPanelTo(GameObject newPage)
    {
        _currentPanel.SetActive(false);
        newPage.SetActive(true);
        _currentPanel = newPage;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}