using UnityEngine.SceneManagement;

public class StageLoader : IStageLoader
{
    public StageLoader()
    {
        _currentStageInfo = new StageInfo(0, CalculationType.Addition, Difficulty.Easy);
    }

    private StageInfo _currentStageInfo;
    public StageInfo CurrentStageInfo
    {
        get => _currentStageInfo;
        set => _currentStageInfo = value;
    }
    public CalculationType NextGameCalculationType { get; set; }
    public Difficulty NextGameDifficulty { get; set; }

    public StageInfo GetStageInfo(int id)
    {
        // Not implemented yet.
        return new StageInfo(id, CalculationType.Addition, Difficulty.Easy);
    }

    /// <summary>
    /// Loads the specified stage.
    /// </summary>
    /// <param name="calculationType"> The type of calculation for the stage.</param>
    /// <param name="difficulty"> The difficulty level of the stage.</param>
    public void LoadStage(CalculationType calculationType, Difficulty difficulty)
    {
        // Not implemented yet. Create Id based on calculationType and difficulty.
        CurrentStageInfo = new StageInfo(0, calculationType, difficulty);
        ServiceScopeProvider.Instance.Scope.TryGetService(out SceneLoader sceneLoader);
        sceneLoader.LoadScene("Stage");
    }

    /// <summary>
    /// Loads the stage with the current calculation type and difficulty.
    /// </summary>
    public void LoadStage()
    {
        LoadStage(NextGameCalculationType, NextGameDifficulty);
    }

    /// <summary>
    /// Unloads the currently loaded stage.
    /// </summary>
    public void UnloadStage()
    {
        ServiceScopeProvider.Instance.Scope.TryGetService(out SceneLoader sceneLoader);
        // Unload the currently loaded stage
        sceneLoader.UnloadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScoreScene()
    {
        ServiceScopeProvider.Instance.Scope.TryGetService(out SceneLoader sceneLoader);
        sceneLoader.LoadScene("ScoreScene");
    }
}