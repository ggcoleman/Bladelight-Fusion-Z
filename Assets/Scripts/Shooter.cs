using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;


    [Header("AI")]
    [SerializeField] float minimumFiringRate = 0.1f;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] bool useAI = false;

    [HideInInspector] public bool isFiring = false;
    Coroutine fireCoroutine;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }


    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFiring && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuosly());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }


    IEnumerator FireContinuosly()
    {

        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            var rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifetime);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(GetRandomProjectileSpawnTime());
        }


    }

    float GetRandomProjectileSpawnTime()
    {
        float spawnTime = UnityEngine.Random.Range(baseFiringRate - firingRateVariance,
        baseFiringRate + firingRateVariance);

        return Mathf.Clamp(spawnTime, minimumFiringRate, float.MaxValue);
    }
}
