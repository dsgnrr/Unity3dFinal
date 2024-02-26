using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetInteger("State", 1);
        }
    }
    public void OnDisappearFinish()
    {
        Vector3 newPosition =
        transform.position + Vector3.forward * 10f;
        newPosition.y = 2f +
            Terrain.activeTerrain.SampleHeight(newPosition);
        transform.position = newPosition;
        _animator.SetInteger("State", 0);
    }
}
