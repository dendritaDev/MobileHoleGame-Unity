using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerTimer : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int timerDuration;
    private int timer;
    private bool timerIsOn;

    [Header(" Events ")]
    public static Action onTimerOver;

    [Header(" Settings ")]
    [SerializeField] private int additionTimePerLevel;

    private void Awake()
    {
        UpgradesManager.onDataLoaded += UpgradesDataLoadedCallback;

    }
    void Start()
    {
        Initialize();

        GameManager.onStateChanged += GameStateChangedCallback;
        UpgradesManager.onTimerPurchased += TimerPurchasedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onStateChanged -= GameStateChangedCallback;
        UpgradesManager.onTimerPurchased -= TimerPurchasedCallback;
        UpgradesManager.onDataLoaded -= UpgradesDataLoadedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                break;
            case GameState.GAME:
                StartTimer();
                break;
            case GameState.LEVELCOMPLETE:
                break;
            case GameState.GAMEOVER:
                break;
        }
    }

    public void Initialize()
    {
        timer = timerDuration;
        timerText.text = FormatSeconds(timer);
    }

    public void StartTimer()
    {
        if(timerIsOn)
        {
            Debug.Log("Timer is already on");
            return;
        }

        Initialize();

        timerIsOn = true;

        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        while (timerIsOn)
        {
            yield return new WaitForSeconds(1);

            timer--;

            timerText.text = FormatSeconds(timer);

            if(timer == 0)
            {
                timerIsOn = false;
                StopTimer();
            }
        }
    }

    public void StopTimer() 
    {
        onTimerOver.Invoke();
    }

    private void TimerPurchasedCallback()
    {
        timerDuration += additionTimePerLevel;
        Initialize();
    }

    private string FormatSeconds(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }

    private void UpgradesDataLoadedCallback(int timerLevel, int sizeLevel, int powerLevel)
    {
        timerDuration += additionTimePerLevel * timerLevel;
        Initialize();
    }



}
