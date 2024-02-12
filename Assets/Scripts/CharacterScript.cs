using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 100f;
    private CharacterController character;
    void Start()
    {
        character = GetComponent<CharacterController>();
    }
    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        if (Mathf.Abs(dx) > 0 && Mathf.Abs(dy) > 0)
        {
            dx *= 0.707f; 
            dy *= 0.707f;
        }

        character.SimpleMove(speed *
            Time.deltaTime *
            (dx * Camera.main.transform.right +
            dy * Camera.main.transform.forward)
            );
    }
}
