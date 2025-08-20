using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuButtonReference : MonoBehaviour
{
    public Button GoBackButton;
    public TextMeshProUGUI MathProblemText;
    public TotalNumberHandler PlayerAnswer;
    public TextMeshProUGUI StopwatchText;

    public static Action<GameMenuButtonReference> ButtonRefsLoaded;
    private MathProblemManager _mathProblemManager;

    private void OnEnable()
    {
        ButtonRefsLoaded?.Invoke(this);
        ServiceScopeProvider.Instance.Scope.TryGetService(out _mathProblemManager);
    }

    private void OnDisable()
    {
        _mathProblemManager.PauseStopwatch();
    }

    private void Start()
    {
        _mathProblemManager.StartStopwatch();
    }

    private void Update()
    {
        _mathProblemManager.UpdateStopwatch();
    }
}