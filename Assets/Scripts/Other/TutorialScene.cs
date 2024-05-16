using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TutorialScene : MonoBehaviour
{
    [Header("Targets fire")]
    [SerializeField] private GameObject[] targetsList;

    [Header("Weapon pickup")]
    [SerializeField] private GameObject weaponToTake;
    [SerializeField] private GameObject pointingToWeapon;

    [Header("TV activation")]
    [SerializeField] private GameObject TV;
    [SerializeField] private Material TVMaterial;

    [Header("Platform")]
    [SerializeField] private GameObject platform;
    [SerializeField] private Material platformHighlight;
    [SerializeField] private GameObject pointingToPlatform;

    [Header("Healing station")]
    [SerializeField] private GameObject healingStation;

    [Header("Healing station")]
    [SerializeField] private AudioClip[] tutorialAudioClipsN1;
    [SerializeField] private AudioClip[] tutorialAudioClipsN2;
    [SerializeField] private AudioClip[] tutorialAudioClipsN3;
    [SerializeField] private AudioClip[] tutorialAudioClipsN4;

    public bool tutorialStarted = false;
    public bool gunPicked = false;
    public bool targetsSetupped = false;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (weaponToTake == null && tutorialStarted && !gunPicked)
        {
            gunPicked = true;
            platform.GetComponent<Renderer>().material = platformHighlight;
            pointingToWeapon.SetActive(false);
            Debug.Log("Speech2");
            StartCoroutine(PlayTutorialAudioClips(tutorialAudioClipsN2, ActivatePointingToPlatform));
        }
        if (targetsSetupped)
        {
            if (AllTargetsInactive())
            {
                Debug.Log("Speech4");
                targetsSetupped = false;
                StartCoroutine(PlayTutorialAudioClips(tutorialAudioClipsN4, null));
            }
        }

    }

    public void StartTutorial()
    {
        tutorialStarted = true;
        Debug.Log("Training1");
        TV.GetComponent<Renderer>().material = TVMaterial;
        Debug.Log("Speech1");
        StartCoroutine(PlayTutorialAudioClips(tutorialAudioClipsN1, ActivatePointingToWeapon));
    }

    IEnumerator PlayTutorialAudioClips(AudioClip[] clips, Action callback)
    {
        foreach (AudioClip clip in clips)
        {
            audioSource.clip = clip;
            audioSource.Play();
            yield return new WaitForSeconds(clip.length + 0.1f);
        }

        callback?.Invoke();
    }

    public void ShootTraining()
    {
        Debug.Log("Training2");
        pointingToPlatform.SetActive(false);
        Debug.Log("Speech3");
        StartCoroutine(PlayTutorialAudioClips(tutorialAudioClipsN3, SetupTargets));
    }

    public void SetupTargets()
    {
        foreach (GameObject target in targetsList)
        {
            target.GetComponent<Target>().StartTarget();
        }
        targetsSetupped = true;
    }

    private void ActivatePointingToWeapon()
    {
        pointingToWeapon.SetActive(true);
    }

    private void ActivatePointingToPlatform()
    {
        pointingToPlatform.SetActive(true);
    }
    private bool AllTargetsInactive()
    {
        foreach (GameObject target in targetsList)
        {
            if (target.GetComponent<Target>().isActive)
            {
                return false;
            }
        }
        return true;
    }

}
