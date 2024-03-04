using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintScript : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    private GameObject leftArrow;
    private GameObject rightArrow;

    void Start()
    {
        leftArrow = GameObject.Find("HintsLeftArrow");
        rightArrow = GameObject.Find("HintsRightArrow");
    }

    void Update()
    {
        Vector3 point = Camera.main.
            WorldToViewportPoint(coin.transform.position);

        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        if (point.z >= 0)
        {
            if (point.x < 0)
            {
                leftArrow.SetActive(true);
            }
            else if (point.x > 1)
            {
                rightArrow.SetActive(true);
            }
        }
        else
        {
            float angle = Vector3.SignedAngle(
                Camera.main.transform.forward,
                coin.transform.position -
                Camera.main.transform.position,
                Vector3.down);
            if (angle > 0)
            {
                leftArrow.SetActive(true);
            }
            else
            {
                rightArrow.SetActive(true);
            }

        }
    }
}
