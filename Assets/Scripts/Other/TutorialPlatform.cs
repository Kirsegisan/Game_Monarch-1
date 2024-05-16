using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlatform : MonoBehaviour
{
    public bool reached = false;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private TutorialScene scene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (scene.tutorialStarted && scene.pointingToPlatformActive)
            {
                gameObject.GetComponent<Renderer>().material = defaultMaterial;
                scene.ShootTraining();
            }
        }
    }
}
