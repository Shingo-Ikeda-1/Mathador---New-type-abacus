using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonReference : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public Button additionGameButton;
    public Button subtractionGameButton;
    public Button multiplicationGameButton;
    public Button divisionGameButton;
    public GameObject difficultySelectionPanel;
    public Button difficultyButton1;
    public Button difficultyButton2;
    public Button difficultyButton3;
    public GameObject settingsPanel;
    public Button settingsButton;
    public Button quitAppButton;

    public static Action<MainMenuButtonReference> ButtonRefsLoaded;

    private void OnEnable()
    {
        Debug.Log($"MainMenuButtonReference OnEnable");
        ButtonRefsLoaded?.Invoke(this);
    }
}