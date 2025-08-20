using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MathProblemManager
{
    private GameMenuButtonReference gameMenuButtonReference;
    // "+" "-" "×" "÷" are expected operators.
    public (string @operator, List<int> problemNumbers) CurrentProblem;
    private int _correctAnswer;
    public event Action<int> OnAnswerEvaluated;
    private int _problemCounter = 5;

    private readonly List<(int min, int max)> minMaxPairs = new()
        {
            (0, 3),
            (3, 6),
            (6, 9)
        };
    private readonly Random random = new();

    private IStageLoader stageLoader;

    public float elapsedTime;
    private bool isPaused;

    public MathProblemManager()
    {
        GameMenuButtonReference.ButtonRefsLoaded += MathSceneRefsLoaded; // Don't unsubscribe this, as MathProblemManager is a singleton and should always be listening for the reference to be loaded in the lifetime of game.
    }

    private void MathSceneRefsLoaded(GameMenuButtonReference buttonRefs)
    {
        ServiceScopeProvider.Instance.Scope.TryGetService<IStageLoader>(out stageLoader); // get the stage loader for later use
        gameMenuButtonReference = buttonRefs;
        Debug.Log($"gameMenuButtonReference: {gameMenuButtonReference}");
        gameMenuButtonReference.PlayerAnswer.OnAggregateNumberChanged += EvaluateAnswer;
        StartMathProblem();
    }

    private void StartMathProblem()
    {
        string arithmeticSymbol = StageInfo.GetSymbol(stageLoader.CurrentStageInfo.CalculationType);
        int difficulty = (int)stageLoader.CurrentStageInfo.Difficulty;
        GenerateAndStoreMathProblem(arithmeticSymbol, difficulty, 2);
        _correctAnswer = CalculateNumbers(CurrentProblem.@operator, CurrentProblem.problemNumbers);
        Debug.Log($"correct answer is: {_correctAnswer}");
        ShowMathProblem();
    }

    private void ShowMathProblem()
    {
        gameMenuButtonReference.MathProblemText.text = string.Format("{0} {1} {2}", CurrentProblem.problemNumbers[0], CurrentProblem.@operator, CurrentProblem.problemNumbers[1]);
    }


    private void GenerateAndStoreMathProblem(string @operator, int difficulty, int amountOfNumbers)
    {
        CurrentProblem.@operator = @operator;
        CurrentProblem.problemNumbers = GenerateAdditionMathProblem(difficulty, amountOfNumbers);
    }

    /// <summary>
    /// Make an addition math problem based on the difficulty and amount of numbers used in the problem.
    /// </summary>
    /// <param name="difficulty"></param>
    /// <param name="amountOfNumbers"></param>
    /// <returns></returns>
    public List<int> GenerateAdditionMathProblem(int difficulty, int amountOfNumbers)
    {
        List<int> problemNumbers = new();
        while (amountOfNumbers-- > 0)
        {
            int randomDifficulty = random.Next(difficulty * 50, difficulty * 50 + 50);
            problemNumbers.Add(randomDifficulty);
        }
        return problemNumbers;
    }

    /// <summary>
    /// Makes one number based on the difficulty. difficulty 0 - 2 returns 0-9, difficulty 3 - 8 returns 10-99, difficulty 9 - 26 returns 100-999, etc.
    /// </summary>
    /// <param name="difficulty">the higher it is, the harder. starts from 0.</param>
    /// <returns></returns>
    private int GenerateNumberFromDifficulty(int difficulty)
    {
        int i = 0;
        int result = 0;
        do
        {
            // Generates a number from last digit based on the difficulty.
            int lastDigitDifficultyPhase = difficulty % minMaxPairs.Count;
            Debug.Log($"Difficulty: {difficulty}, Last Digit Difficulty Phase: {lastDigitDifficultyPhase}");
            int digit = random.Next(minMaxPairs[lastDigitDifficultyPhase].min, minMaxPairs[lastDigitDifficultyPhase].max);
            result += digit * (int)Mathf.Pow(10, i);
            difficulty /= minMaxPairs.Count;
            ++i;
        } while (difficulty > 0);
        return result;
    }

    private void EvaluateAnswer(int submittedAnswer)
    {
        int gap = _correctAnswer - submittedAnswer;
        if (gap == 0)
        {
            Debug.Log($"Correct answer! Answer is {_correctAnswer}");
            if (_problemCounter-- > 0)
            {
                StartMathProblem();
            }
            else
            {
                Debug.Log("All problems solved! Proceeding to score scene.");
                stageLoader.LoadScoreScene();
            }
        }
        else
        {
        }
        OnAnswerEvaluated?.Invoke(gap);
    }

    public static int CalculateNumbers(string @operator, List<int> operands)
    {
        return CalculateNumbers(@operator, operands, 0);
    }

    private static int CalculateNumbers(string @operator, List<int> operands, int recursiveCounter)
    {
        Dictionary<string, Func<int, int, int>> operators = new()
        {
            { "+", (a,b) => a + b },
            { "-", (a,b) => a - b },
            { "×", (a,b) => a * b },
            { "÷", (a,b) => a / b }
        };
        var operation = operators[@operator];
        int result = operation(operands[recursiveCounter], operands[recursiveCounter + 1]);
        if (recursiveCounter + 2 < operands.Count)
        {
            operands[recursiveCounter + 1] = result;
            return CalculateNumbers(@operator, operands, ++recursiveCounter);
        }
        else
        {
            return result;
        }
    }
    #region Stopwatch
    public void StartStopwatch()
    {
        elapsedTime = 0f;
        isPaused = false;
        UpdateTimerDisplay();
    }

    public void UpdateStopwatch()
    {
        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        gameMenuButtonReference.StopwatchText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PauseStopwatch()
    {
        isPaused = true;
    }
    #endregion Stopwatch
}
