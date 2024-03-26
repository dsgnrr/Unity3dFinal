using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MessagesScript : MonoBehaviour
{
    private const int maxLines = 9;
    private TMPro.TextMeshProUGUI messagesPanel;
    private void Start()
    {
        messagesPanel = GameObject
            .Find("Text")
            .GetComponent<TMPro.TextMeshProUGUI>();
        messagesPanel.text = string.Empty;
        GameState.Subscribe(OnGameStateChanged);
    }
    private void Update()
    {

    }
    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameStateChanged);
    }
    private void OnGameStateChanged(string propName)
    {
        if (propName == nameof(GameState.GameMessages))
        {
            StringBuilder sb = new();
            foreach (var message in GameState.GameMessages.TakeLast(maxLines))
            {
                sb.Append($"{message.Moment.ToShortTimeString()} {message.Text}\n");
            }
            messagesPanel.text = sb.ToString();
        }
    }
}
