using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI _display;

    private void Awake()
    {
        _display = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText()
    {
        _display.text = string.Format("Score \n{0}", ScoreManager.Manage.GetScore());
    }
}
