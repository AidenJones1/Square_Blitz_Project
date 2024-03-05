using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        _text.text = $"High Score: {ScoreManager.GetHighScore()}";
    }
}
