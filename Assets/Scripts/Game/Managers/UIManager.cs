using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Manage { get; private set; }

    [SerializeField] public GameObject _gameOverUI;
    [SerializeField] private TextMeshProUGUI _tipDisplay;

    private void Awake()
    {
        InitializeSingleton();
    }

    public void ShowUI()
    {
        _gameOverUI.SetActive(true);
    }

    public void HideUI()
    {
        _gameOverUI.SetActive(false);
    }

    private void InitializeSingleton()
    {
        if (Manage == null)
            Manage = this;
        else
            Destroy(this);
    }

    public void HideTipDisplay()
    {
        _tipDisplay.enabled = false;
    }
}
