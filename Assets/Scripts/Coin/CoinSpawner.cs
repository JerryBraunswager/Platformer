using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _timeToWait;

    private WaitForSeconds _seconds;

    private void Start()
    {
        _seconds = new WaitForSeconds(_timeToWait);
        StartCoroutine(SpawnCoins());
    }

    private IEnumerator SpawnCoins()
    {
        while (true)
        {
            yield return _seconds;
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        int indexSpawnPoint = Random.Range(0, _spawnPoints.Count);

        if (_spawnPoints[indexSpawnPoint].childCount == 0)
        {
            Instantiate(_coinPrefab, _spawnPoints[indexSpawnPoint]);
        }
    }
}
