using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] public int enemyCount = 20;
    [SerializeField] public bool _canSpawn = true;

    void Start()
    {
        _canSpawn = true;
        StartCoroutine(SpawnEnemyRoutine());
    }

    private void Update()
    {
        if (enemyCount == 50)
        {
            _canSpawn = false;
        }
    }


    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(40f);

        while (_canSpawn == true)
        {
            enemyCount = enemyCount + 1;
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            // Debug.Log("Enemy Spawned");
            yield return new WaitForSeconds(Random.Range(2f, 3.0f));
        }
    }
}