using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    private TextMeshProUGUI _fpsDisplay;

    private void Awake()
    {
        _fpsDisplay = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _fpsDisplay.text = string.Format("FPS: {0:0}", (1 / Time.deltaTime));
    }
}
