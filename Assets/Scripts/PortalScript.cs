using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField]
    private GameObject destination;
    void Start()
    {

    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        CharacterController controller = other.gameObject.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            other.gameObject.transform.position =
            destination.transform.position;
            controller.enabled = true;
        }
    }
}
