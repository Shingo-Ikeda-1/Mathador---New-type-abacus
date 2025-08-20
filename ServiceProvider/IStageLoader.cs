public interface IStageLoader
{
    StageInfo CurrentStageInfo { get; set; }
    CalculationType NextGameCalculationType { get; set; }
    Difficulty NextGameDifficulty { get; set; }

    public StageInfo GetStageInfo(int id);

    void LoadStage();
    /// <summary>
    /// Loads the specified stage.
    /// </summary>
    /// <param name="stageName">The name of the stage to load.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    void LoadStage(CalculationType calculationType, Difficulty difficulty);

    /// <summary>
    /// Unloads the currently loaded stage.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    void UnloadStage();

    void LoadScoreScene();
}