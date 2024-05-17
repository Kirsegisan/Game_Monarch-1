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
    [SerializeField] private HealingStation healingStation;
    [SerializeField] private GameObject pointingToHealingStation;
    [SerializeField] private GameObject healingStationTrigger;

    [Header("Audio")]
    [SerializeField] private AudioClip[] tutorialAudioClipsN1;
    [SerializeField] private AudioClip[] tutorialAudioClipsN2;
    [SerializeField] private AudioClip[] tutorialAudioClipsN3;
    [SerializeField] private AudioClip[] tutorialAudioClipsN4;
    [SerializeField] private AudioClip[] tutorialAudioClipsN5;
    [SerializeField] private AudioClip[] tutorialAudioClipsN6;

    [Header("OnTutorialEnd")]
    [SerializeField] private GameObject roomButton;
    [SerializeField] private GameObject batya;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private HealsDisplay healsDisplay;
    [SerializeField] private AmmoDisplay ammoDisplay;
    [SerializeField] private VoiceAssistant voiceAssistant;

    public bool tutorialStarted = false;
    public bool gunPicked = false;
    public bool targetsSetupped = false;
    public bool pointingToPlatformActive = false;
    public bool pointingToHealingStationActive = false;
    public bool waitingToUseHealingStation = false;
    public bool tutorialEnded = false;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        weaponToTake.SetActive(false);
        healingStationTrigger.SetActive(false);
        voiceAssistant.gameObject.SetActive(false);
        healingStation.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (weaponToTake == null && tutorialStarted && !gunPicked && !tutorialEnded)
        {
            gunPicked = true;
            pointingToWeapon.SetActive(false);
            Debug.Log("Speech2");
            StartCoroutine(PlayTutorialAudioClips(tutorialAudioClipsN2, ActivatePointingToPlatform));
        }
        if (targetsSetupped && !tutorialEnded)
        {
            if (AllTargetsInactive())
            {
                Debug.Log("Speech4");
                targetsSetupped = false;
                StartCoroutine(PlayTutorialAudioClips(tutorialAudioClipsN4, ActivatePointingToHealingStation));
            }
        }
        if (pointingToHealingStationActive && !tutorialEnded)
        {
            if (healingStationTrigger == null)
            {
                pointingToHealingStation.SetActive(false);
                pointingToHealingStationActive = false;
                Debug.Log("Speech5");
                StartCoroutine(PlayTutorialAudioClips(tutorialAudioClipsN5, ActivateWaitingToUseHealingStation));
            }
        }

        if (waitingToUseHealingStation && !tutorialEnded)
        {
            if (healingStation.restorationsAmount == 0)
            {
                Debug.Log("Speech6");
                StartCoroutine(PlayTutorialAudioClips(tutorialAudioClipsN6, OnTutorialEnd));
            }
        }

    }

    public void StartTutorial()
    {
        tutorialStarted = true;
        Debug.Log("Training1");
        TV.GetComponent<Renderer>().material = TVMaterial;
        Debug.Log("Speech1");
        if (!tutorialEnded)
        {
            StartCoroutine(PlayTutorialAudioClips(tutorialAudioClipsN1, ActivatePointingToWeapon));
        }
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
        if (!tutorialEnded)
        {
            StartCoroutine(PlayTutorialAudioClips(tutorialAudioClipsN3, SetupTargets));
        }
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
        weaponToTake.SetActive(true);
    }

    private void ActivatePointingToPlatform()
    {
        pointingToPlatform.SetActive(true);
        platform.GetComponent<Renderer>().material = platformHighlight;
        pointingToPlatformActive = true;
    }

    private void ActivatePointingToHealingStation()
    {
        pointingToHealingStation.SetActive(true);
        healingStationTrigger.SetActive(true);
        healingStation.gameObject.SetActive(true);
        pointingToHealingStationActive = true;
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

    private void ActivateWaitingToUseHealingStation()
    {
        waitingToUseHealingStation = true;
    }

    private void OnTutorialEnd()
    {
        tutorialEnded = true;
        roomButton.SetActive(true);
        batya.SetActive(true);
        voiceAssistant.gameObject.SetActive(true);
        playerData.ammo = 200;
        playerData.heals = playerData.maxHeals;
        healsDisplay.UpdateHealsDisplay();
        ammoDisplay.UpdateAmmoDisplay();
    }

}
