using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VoiceAssistant : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private AudioSource audioSource;

    [SerializeField] private AudioClip[] lowAmmoClips;
    [SerializeField] private AudioClip[] lowHealthClips;
    [SerializeField] private AudioClip[] highEnemyAmountClips;

    [SerializeField] private float clipDelay = 1f;
    [SerializeField] private float enemyDetectionRadius = 10f;
    [SerializeField] private int enemyThreshold = 3;

    private bool isPlayingClip = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LowAmmo(int remainAmmo)
    {
        if (!isPlayingClip && playerData.ammo <= remainAmmo)
        {
            StartCoroutine(PlayClipWithDelay(lowAmmoClips));
        }
    }

    public void LowHealth(float percentHealth)
    {
        if (!isPlayingClip && playerData.health / playerData.maxHealth < percentHealth)
        {
            StartCoroutine(PlayClipWithDelay(lowHealthClips));
        }
    }

    private IEnumerator PlayClipWithDelay(AudioClip[] clips)
    {
        isPlayingClip = true;

        if (clips.Length == 0)
        {
            isPlayingClip = false;
            yield break;
        }

        AudioClip clipToPlay = clips[Random.Range(0, clips.Length)];

        audioSource.PlayOneShot(clipToPlay);

        yield return new WaitForSeconds(clipToPlay.length + clipDelay);

        isPlayingClip = false;
    }

    private void Update()
    {
        CheckForEnemies();
    }

    private void CheckForEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemyDetectionRadius);
        int enemyCount = 0;

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                enemyCount++;
            }
        }

        if (enemyCount > enemyThreshold && !isPlayingClip)
        {
            StartCoroutine(PlayClipWithDelay(highEnemyAmountClips));
        }
    }
}
