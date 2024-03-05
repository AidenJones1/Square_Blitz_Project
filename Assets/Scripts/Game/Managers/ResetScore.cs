using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScore : MonoBehaviour
{
    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("Highscore", 0);
        GameObject.Find("HighScoreTMP").GetComponent<HighScoreDisplay>().UpdateText();
    }
}
