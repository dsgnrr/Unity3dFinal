using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private float speed = 10f;
    private float playerVelocityY;
    private float gravityValue = -9.80f;
    private float jumpHeight = 1.0f;
    private bool groundedPlayer;

    private CharacterController _characterController;
    private Animator _animator; 
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator=GetComponent<Animator>();
        playerVelocityY = 0f;

    }
    void Update()
    {
        int animatorState = 0;
        groundedPlayer = _characterController.isGrounded;
        if (groundedPlayer && playerVelocityY < 0)
        {
            playerVelocityY = 0f;
        }

        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        if (Mathf.Abs(dx) > 0 && Mathf.Abs(dy) > 0)
        {
            dx *= 0.707f;
            dy *= 0.707f;
        }
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocityY += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }
        if (dy != 0)
        {
            animatorState = 1;
        }
        Vector3 horizontalForward = Camera.main.transform.forward;
        horizontalForward.y = 0;
        horizontalForward = horizontalForward.normalized;
        this.transform.forward = horizontalForward;

        playerVelocityY += gravityValue * Time.deltaTime;

        _characterController.Move(Time.deltaTime *
            (speed * (dx * Camera.main.transform.right + dy * horizontalForward) +
            playerVelocityY * Vector3.up));
        _animator.SetInteger("State", animatorState);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter: " + other.gameObject.name);
        if (other.CompareTag("Floor"))
        {
            groundedPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit: " + other.gameObject.name);
        if (other.CompareTag("Floor"))
        {
            groundedPlayer = false;
        }
    }
}
