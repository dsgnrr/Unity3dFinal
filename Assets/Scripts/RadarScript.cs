using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarScript : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private GameObject coin;

    private float maxRange = 30f;

    private Image pointer;
    private Rect size;
    private GameObject content;

    void Start()
    {
        pointer = GameObject.Find("RadarContentPointer").GetComponent<Image>();
        size = GameObject.Find("Radar").GetComponent<RectTransform>().rect;
        content = GameObject.Find("RadarContent");
        GameState.Subscribe(OnGameStateChanged);
        OnGameStateChanged(nameof(GameState.isRadarVisible));
    }

    void Update()
    {
        Vector3 toCoin = coin.transform.position - character.transform.position;
        toCoin.y = 0;

        float angle = Vector3.SignedAngle(
            character.transform.forward,
            toCoin,
            Vector3.up) * Mathf.Deg2Rad;

        float range = toCoin.magnitude;

        if (range > maxRange)
        {
            pointer.gameObject.SetActive(false);
        }
        else
        {
            pointer.gameObject.SetActive(true);
            pointer.rectTransform.localPosition = new Vector3(
                range / maxRange * size.width / 2.0f * Mathf.Sin(angle),
                range / maxRange * size.height / 2.0f * Mathf.Cos(angle),
                0);
        }
    }
    private void OnGameStateChanged(string propName)
    {
        if (propName == nameof(GameState.isRadarVisible))
        {
            content.SetActive(GameState.isRadarVisible);
        }
    }
    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameStateChanged);
    }
}
