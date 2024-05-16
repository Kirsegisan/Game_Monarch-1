using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechTrigger : MonoBehaviour
{
    public List<AudioClip> speechClips;
    public float delayBetweenSpeech = 1.0f;

    private bool isPlaying = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlaying)
        {
            StartCoroutine(PlaySpeech());
        }
    }

    IEnumerator PlaySpeech()
    {
        isPlaying = true;

        foreach (AudioClip clip in speechClips)
        {
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length + delayBetweenSpeech);
        }

        isPlaying = false;
    }
}
