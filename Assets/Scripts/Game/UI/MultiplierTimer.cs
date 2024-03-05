using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierTimer : MonoBehaviour
{
    private Image _image;
    private float _timeRemaining;
    private float _duration;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        _duration = ScoreManager.Manage.MultiplierDuration;
        _image.fillAmount = 0;
    }

    private void Update()
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;

            _image.fillAmount = _timeRemaining / _duration;
        }
    }

    public void ActivateTimer()
    {
        _timeRemaining = _duration;
        _image.fillAmount = 1;
    }

    public void StopTimer()
    {
        _timeRemaining = 0;
        _image.fillAmount = 0;
    }
}
