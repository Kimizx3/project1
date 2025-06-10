using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerManager : MonoBehaviour
{
    [Header("Timer Buttons")]
    public Button StartButton;
    public Button StopButton;
    public Button ResetButton;
    
    [Header("Add To Clock Time")]
    public Button AddTimeButton;
    public Button MinusTimeButton;
    
    [Header("Add to Time Break")]
    public Button AddBreakTimeButton;
    public Button MinusBreakTimeButton;

    public static event Action OnStartClicked;
    public static event Action OnStopClicked;
    public static event Action OnResetClicked;

    public static event Action OnAddTimeClicked;
    public static event Action OnMinusTimeClicked;
    public static event Action OnAddBreakTimeClicked;
    public static event Action OnMinusBreakTimeClicked;

    private void Start()
    {
        // Play and Pause
        StartButton.onClick.AddListener(() => OnStartClicked?.Invoke());
        StopButton.onClick.AddListener(() => OnStopClicked?.Invoke());
        ResetButton.onClick.AddListener(() => OnResetClicked?.Invoke());
        
        // Add Time and Minus Time
        AddTimeButton.onClick.AddListener(() => OnAddTimeClicked?.Invoke());
        MinusTimeButton.onClick.AddListener(() => OnMinusTimeClicked?.Invoke());
        AddBreakTimeButton.onClick.AddListener(() => OnAddBreakTimeClicked?.Invoke());
        MinusBreakTimeButton.onClick.AddListener(() => OnMinusBreakTimeClicked?.Invoke());
    }
}