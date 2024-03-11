using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour
{
    private Image indicator;
    void Start()
    {
        indicator = GameObject
            .Find("StaminaIndicator")
            .GetComponent<Image>();
        GameState.Subscribe(OnGameStateChange);
    }

    private void OnGameStateChange(string propName)
    {
        if (propName == nameof(GameState.CharacterStamina))
        {
            indicator.fillAmount = GameState.CharacterStamina;
        }
    }
    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameStateChange);
    }
}
