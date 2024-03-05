using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Manage { get; private set; }

    /// <summary>
    /// Invokes upon the start of the game.
    /// </summary>
    public UnityEvent OnGameStart = new UnityEvent();
    public UnityEvent OnGameEnd = new UnityEvent();
    public UnityEvent OnGameContinue = new UnityEvent();

    private int _gamesPlayed = 0;

    private Coroutine _endGameCoroutine;

    private void Awake()
    {
        if (Manage == null)
            Manage = this;
        else
            Destroy(this);
    }

    public void Start()
    {
        OnGameStart.AddListener(ScoreManager.Manage.RestartScore);
        OnGameStart.AddListener(SpawnManager.Manage.ResetLevels);
        OnGameStart.AddListener(UIManager.Manage.HideUI);
        OnGameStart.AddListener(GameTime.Timer.ResetTimer);

        OnGameContinue.AddListener(UIManager.Manage.HideUI);
        OnGameContinue.AddListener(SpawnManager.Manage.SpawnPlayer);
        OnGameContinue.AddListener(GameTime.Timer.ResetTimer);

        OnGameEnd.AddListener(UIManager.Manage.ShowUI);
        OnGameEnd.AddListener(ScoreManager.Manage.StopRoutine);
        OnGameEnd.AddListener(ScoreManager.Manage.UpdateHighScore);

        StartGame();
        Application.targetFrameRate = Mathf.RoundToInt((float) Screen.currentResolution.refreshRateRatio.value);
    }
    public void StartGame()
    {
        OnGameStart.Invoke();
    }

    public void EndGame()
    {
        if (_endGameCoroutine != null)
        {
            StopCoroutine(_endGameCoroutine);
            _endGameCoroutine = null;   
        }

        if (_endGameCoroutine == null)
        {
            _endGameCoroutine = StartCoroutine(EndGameRoutine());
        }
    }

    private IEnumerator EndGameRoutine()
    {
        GameTime.Timer.StopTimer();
        yield return new WaitForSeconds(1.5f);
        OnGameEnd.Invoke();
        _gamesPlayed++;
    }
}
