using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Animator _animator;
    const float minSpawnOffset = 50f;
    float minSpawnDistance = 20f;
    float maxSpawnDistance = 30f;
    float maxHeightFactor = 1.5f;
    float minHeightFactor = 0.7f;
    float initialCoinHeight;

    void Start()
    {
        _animator = GetComponent<Animator>();
        initialCoinHeight = this.transform.position.y -
          Terrain.activeTerrain.SampleHeight(this.transform.position);
        GameState.Subscribe(OnGameStateChange);
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
        GameState.Score += GameState.CoinCost;
        Respawn();
        _animator.SetInteger("State", 0);
    }
    private void Respawn()
    {
        Vector3 newPosition;
        float distance;
        int lim = 100;
        do
        {
            newPosition = transform.position +
                Vector3.forward * Random.Range(-maxSpawnDistance, maxSpawnDistance) +
                Vector3.left * Random.Range(-maxSpawnDistance, maxSpawnDistance);

            newPosition.y = initialCoinHeight * Random.Range(minHeightFactor, maxHeightFactor) +
                Terrain.activeTerrain.SampleHeight(newPosition);

            distance = Vector3.Distance(newPosition, transform.position);
            lim--;
        } while (lim > 0 && (distance < minSpawnDistance ||
        distance > minSpawnDistance ||
        newPosition.x < minSpawnOffset ||
        newPosition.x > 1000 - minSpawnOffset ||
        newPosition.y < minSpawnOffset ||
        newPosition.y > 1000 - minSpawnOffset));
        transform.position = newPosition;
    }

    private void OnGameStateChange(string propName)
    {
        if (propName == nameof(GameState.CoinCost))
        {
            Respawn();
        }
    }
    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameStateChange);
    }
}
