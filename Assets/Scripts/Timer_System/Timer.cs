using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    // TODO: Time Progress Bar UI
    // TODO: Real time translation
    // TODO: Single button to play/stop timer
    
    // Public:
    [Header("Components")] 
    //[SerializeField] public TextMeshProUGUI timeSetterText;
    [SerializeField] public TextMeshProUGUI breakTimeText;
    [SerializeField] public TextMeshProUGUI clockTxt;

    [Header("Time Variables")]
    public float clockTime = 2700;
    public float breakTime = 900;
    public bool countDown;
    
    
    /// public void PlayTimer();
    /// public void StopTimer();
    /// public void ResetTimer();
    /// public void ToggleTimerButton();
    ///

    [Header("Format Setting")] 
    public bool hasFormat;
    public TimerFormats format;

    [Header("Limit Settings")] 
    public float timerLimit;
    public bool hasLimit;
    
    
    // Private:
    // Some basic setups
    private const float AMinute = 60f;
    
    private float _currentTime;
    private float _workTime;
    private float _breakTime;
    private bool _timerStarted;
    private bool _timerStopped;

    private Dictionary<TimerFormats, string> timerFormats = 
        new Dictionary<TimerFormats, string>();

    // Looper setting
    [Header("Loop Settings")]
    [SerializeField] private int totalLaps = 4;
    private int _currentLap = 1;
    private TimerState currentState = TimerState.Study;
    
    /// private void DisplayTimer();
    /// private void DisplayTimeSetter();
    /// private void DisplayBreakTimeSetter();
    /// private void SetTime();
    /// private void GetMinutes();
    /// private void GetSeconds();
    /// private void PlusMinutesWorkTime();
    /// private void PlusMinutesBreakTime();
    /// private void MinusMinutesWorkTime();
    /// private void MinusMinutesBreakTime();
    
    // Protected:
 
    
    private void OnEnable()
    {
        TimerManager.OnStartClicked += PlayTimer;
        TimerManager.OnStopClicked += StopTimer;
        TimerManager.OnResetClicked += ResetTimer;

        TimerManager.OnAddTimeClicked += PlusMinutesWorkTime;
        TimerManager.OnAddBreakTimeClicked += PlusMinutesBreakTime;
        
        TimerManager.OnMinusTimeClicked += MinusMinutesWorkTime;
        TimerManager.OnMinusBreakTimeClicked += MinusMinutesBreakTime;
    }

    private void OnDisable()
    {
        TimerManager.OnStartClicked -= PlayTimer;
        TimerManager.OnStopClicked -= StopTimer;
        TimerManager.OnResetClicked -= ResetTimer;
        
        TimerManager.OnAddTimeClicked -= PlusMinutesWorkTime;
        TimerManager.OnAddBreakTimeClicked -= PlusMinutesBreakTime;
        
        TimerManager.OnMinusTimeClicked -= MinusMinutesWorkTime;
        TimerManager.OnMinusBreakTimeClicked -= MinusMinutesBreakTime;
    }

    private void Start()
    {
        timerFormats.Add(TimerFormats.Whole, "0");
        timerFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timerFormats.Add(TimerFormats.HundredthsDecimal, "0.00");
        InitializeTimer();
    }

    private void Update()
    {
        // _currentTime = countDown ? _currentTime -= Time.deltaTime : _currentTime += Time.deltaTime;
        // if (_timerStarted)
        // {
        //     if (hasLimit && ((countDown && _currentTime <= timerLimit) || (!countDown && _currentTime >= timerLimit)))
        //     {
        //         _currentTime = timerLimit;
        //         DisplayTimer();
        //         DisplayBreakTimeSetter();
        //         clockTxt.color = Color.red;
        //         enabled = false;
        //         _timerStarted = false;
        //         _timerStopped = true;
        //     }
        // }

        if (!_timerStarted) return;

        _currentTime -= Time.deltaTime;
        DisplayTimer();

        if (_currentTime <= 0)
        {
            switch (currentState)
            {
                case TimerState.Study:
                    if (_currentLap < totalLaps)
                    {
                        currentState = TimerState.Break;
                        _currentTime = _breakTime;
                        DisplayBreakTimeSetter();
                    }
                    else
                    {
                        _timerStarted = false;
                        _timerStopped = true;
                        clockTxt.color = Color.green;
                    }
                    break;
                
                case TimerState.Break:
                    _currentLap++;
                    currentState = TimerState.Study;
                    _currentTime = _workTime;
                    break;
            }
        }
    }
    
    
    // TODO: create play and stop functions

    public void PlayTimer()
    {
        _timerStarted = true;
        _timerStopped = false;
        
    }

    public void StopTimer()
    {
        _timerStarted = false;
        _timerStopped = true;
    }

    private void InitializeTimer()
    {
        _currentTime = clockTime;
        _breakTime = breakTime;
        _workTime = _currentTime;
        _timerStarted = false;
        
        DisplayTimer();
        DisplayBreakTimeSetter();
    }
    
    public void ResetTimer()
    {
        _currentTime = clockTime;
        _breakTime = breakTime;
        _workTime = _currentTime;
        DisplayBreakTimeSetter();
        DisplayTimer();
        _timerStarted = false;
    }
    
    
    // TODO: adjust timer functions (plus and minus) for workTime and breakTime

    private void DisplayTimer()
    {
        //clockTxt.text = string.Format("{0:00}:{1:00}", GetMinutes(), GetSeconds());
        clockTxt.text = hasFormat ? _currentTime.ToString(timerFormats[format]) : _currentTime.ToString();
    }
    
    private void DisplayBreakTimeSetter()
    {
        var minuteText = Mathf.FloorToInt(_breakTime / 60);
        breakTimeText.text = string.Format("{00}", minuteText);
    }
    
    public void PlusMinutesWorkTime()
    {
        if (!_timerStarted)
        {
            _currentTime += AMinute;
            _workTime = _currentTime;
        }
    }

    public void MinusMinutesWorkTime()
    {
        if (!_timerStarted)
        {
            _currentTime -= AMinute;
            _workTime = _currentTime;
        }
    }
    
    // TODO: Add breaker time when the clock is ticking, or not ticking
    public void PlusMinutesBreakTime()
    {
        if (!_timerStarted)
        {
            _breakTime += AMinute;
        }
    }

    public void MinusMinutesBreakTime()
    {
        if (!_timerStarted)
        {
            _breakTime -= AMinute;
        }
    }
}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundredthsDecimal
}

public enum TimerState
{
    Study,
    Break
}
