using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;


    private bool _stopSpawning = false;
    // Start is called before the first frame update
    public void SpawnStart()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }


    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(3f);
            Vector3 posToSpawnEnemy = new Vector3(Random.Range(-8f, 8f), 8.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawnEnemy, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(3f);
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 8.5f, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }


    public void DeactivateSpawn()
    {
        _stopSpawning = true;
    }

}
