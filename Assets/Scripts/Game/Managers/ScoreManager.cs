using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Instance
    public static ScoreManager Manage { get; private set; }

    // UnityEvents
    private UnityEvent OnScoreUpdate = new UnityEvent();
    private UnityEvent OnMultiplierActivation = new UnityEvent();
    private UnityEvent OnMultiplierDeactivation = new UnityEvent();

    // Game Stats
    public float MultiplierDuration = 7f;
    private int _gameScore = 0;
    private int _currentMultiplier = 1;

    private Coroutine _multiplierCoroutine;

    private void Awake()
    {
        SetupSingleton();
    }

    private void Start()
    {
        OnScoreUpdate.AddListener(GameObject.Find("ScoreTMP").GetComponent<ScoreDisplay>().UpdateText);

        OnMultiplierActivation.AddListener(GameObject.Find("MultiplierTimerIMG").GetComponent<MultiplierTimer>().ActivateTimer);
        OnMultiplierDeactivation.AddListener(GameObject.Find("MultiplierTimerIMG").GetComponent<MultiplierTimer>().StopTimer);
    }

    public void RestartScore()
    {
        _gameScore = 0;
        OnScoreUpdate.Invoke();

        StopRoutine();
    }

    public void AddScore()
    {
        _gameScore += _currentMultiplier;
        OnScoreUpdate.Invoke();
    }

    public int GetScore() { return _gameScore; }

    public void StartMultiplierRoutine()
    {
        StopRoutine();
        
        if (_multiplierCoroutine == null)
            _multiplierCoroutine = StartCoroutine(MultiplierRoutine());
    }

    public void StopRoutine()
    {
        if (_multiplierCoroutine != null)
        {
            StopCoroutine(_multiplierCoroutine);
            _multiplierCoroutine = null;
            _currentMultiplier = 1;
            OnMultiplierDeactivation.Invoke();
        }
    }

    private IEnumerator MultiplierRoutine()
    {
        OnMultiplierActivation.Invoke();
        _currentMultiplier = 2;
        yield return new WaitForSeconds(MultiplierDuration);
        _currentMultiplier = 1;
    }

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("Highscore");
    }

    public void UpdateHighScore()
    {
        if (_gameScore > PlayerPrefs.GetInt("Highscore"))
            PlayerPrefs.SetInt("Highscore", _gameScore);
    }

    private void SetupSingleton()
    {
        if (Manage == null)
            Manage = this;
        else
            Destroy(this);
    }
}
