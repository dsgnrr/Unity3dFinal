using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    private Toggle compassToggle;
    [SerializeField]
    private Toggle radarToggle;
    [SerializeField]
    private Toggle hintsToggle;
    [SerializeField]
    private GameObject content;

    void Start()
    {
        OnCompassVisibleChanged(compassToggle.isOn);
        OnRadarVisibleChanged(radarToggle.isOn);
        OnHintsVisibleChanged(hintsToggle.isOn);
        ChangePause(content.activeSelf);
        if (GameState.CoinCost == 0f)
        {
            GameState.UpdateCoinCost();
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ChangePause(!content.activeSelf);
        }
    }

    private void ChangePause(bool pause)
    {
        content.SetActive(pause);
        Time.timeScale = pause ? 0.0f : 1.0f;
        Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void OnCompassVisibleChanged(bool value)
    {
        GameState.isCompassVisible = value;
    }

    public void OnRadarVisibleChanged(bool value)
    {
        GameState.isRadarVisible = value;
    }

    public void OnHintsVisibleChanged(bool value)
    {
        GameState.isHintsVisible = value;
    }
    
    public void OnResumePressed()
    {
        ChangePause(false);
    }
}
