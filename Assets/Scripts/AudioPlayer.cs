using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;


    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 1f;
    [SerializeField] AudioClip destroyClip;
    [SerializeField] [Range(0f, 1f)] float destroyVolume = 1f;
 
    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    public void PlayDestroyClip()
    {
        PlayClip(destroyClip, destroyVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            var point = Camera.main.transform.position;

            AudioSource.PlayClipAtPoint(clip, point, volume);
        }
    }
}
