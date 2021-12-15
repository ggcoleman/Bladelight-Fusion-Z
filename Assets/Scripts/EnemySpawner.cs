using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] float waveTimeVariance = 0f;
    [SerializeField] float minimumWaveTime = 0f;

    [SerializeField] bool isLooping = true;


    WaveConfigSO currentWave;
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (var wave in waveConfigs)
            {
                currentWave = wave;
                for (var index = 0; index < currentWave.GetEnemyCount(); index++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(index), currentWave.GetStartingWaypoint().position, Quaternion.Euler(180,0,0), transform);

                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(GetRandomWaveTime());
            }
        } while (isLooping);
    }

    float GetRandomWaveTime()
    {
        float spawnTime = Random.Range(timeBetweenWaves - waveTimeVariance,
        timeBetweenWaves + waveTimeVariance);

        return Mathf.Clamp(spawnTime, minimumWaveTime, float.MaxValue);
    }
}
