public class DevelopmentServiceScope : ServiceScopeBase
{
    // Register services specific to the development environment
    protected override void Configure(IServiceLocator locator)
    {
        // Caution: Register with interface type if there is one.
        SceneLoader sceneLoader = new SceneLoader();
        locator.Register(sceneLoader);
        locator.Register<IStageLoader>(new StageLoader());
        new MainMenuManager().Initialize(locator);
        locator.Register(new MathProblemManager());

        sceneLoader.LoadScene("MainMenu");
    }
}
