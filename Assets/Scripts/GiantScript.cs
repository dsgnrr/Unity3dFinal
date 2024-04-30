using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiantScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private List<Transform> points = new();
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Transform pointsObject = GameObject.FindGameObjectWithTag("Points").transform;
        foreach (Transform t in pointsObject)
        {
            points.Add(t);
        }
        agent.SetDestination(points[Random.Range(0, points.Count)].position);
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(points[Random.Range(0, points.Count)].position);
        }
    }
    public void OnDead()
    {
        GameState.Score += 1;
        GameState.GiantCount--;
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetInteger("State", 2);
        }
    }
}
