using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    TMPro.TextMeshProUGUI textScore;
    void Start()
    {
        textScore=GameObject.Find("GameScoreText").GetComponent<TMPro.TextMeshProUGUI>();
        GameState.Subscribe(OnGameStateChanged);
    }
    private void OnGameStateChanged(string propName)
    {
        if (propName == nameof(GameState.Score))
        {
            textScore.text = GameState.Score.ToString("0000");
        }
    }
    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameStateChanged);
    }
}
