using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 10;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {

            var damage = damageDealer.GetDamage();
            TakeDamage(damage);
            PlayHitEffect();
            ShakeCamera(damage);
            damageDealer.Hit();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    private void ShakeCamera(float damage)
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play(damage);
        }
    }

    private void TakeDamage(int damage)
    {

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            audioPlayer.PlayDamageClip();
        }
    }

    private void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
            audioPlayer.PlayDestroyClip();
        }
        else
        {
            audioPlayer.PlayPlayerDeathClip();             
            levelManager.LoadGameOver();
        }
        Destroy(gameObject); 
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
}
