using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameTime : MonoBehaviour
{
    public static GameTime Timer {  get; private set; }

    private bool isTimeRunning = false;

    [SerializeField] private float _timeToClear = 3.5f;
    private float _timeRemaining = 0f;

    private UnityEvent onTimeExpire = new UnityEvent();

    private TextMeshProUGUI _timerDisplay;

    private void Awake()
    {
        SetupSingleton();  
        
        _timerDisplay = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (isTimeRunning)
        {
            _timeRemaining -= Time.deltaTime;

            if (_timeRemaining <= 0)
            {
                GameObject.Find("Player").GetComponent<MovePlayer>().DeletePlayerObject();
                AudioManager.Instance.Play("PlayerDestroyed_1");
                isTimeRunning = false;
            }

            _timerDisplay.text = string.Format("{0:0.0}", _timeRemaining);
        }

    }

    public void ResetTimer()
    {
        _timeRemaining = _timeToClear;
    }

    public void StartTimer()
    {
        isTimeRunning = true;
    }

    public void StopTimer()
    {
        isTimeRunning = false;
    }

    private void SetupSingleton()
    {
        if (Timer == null)
            Timer = this;
        else
            Destroy(this);
    }
}
