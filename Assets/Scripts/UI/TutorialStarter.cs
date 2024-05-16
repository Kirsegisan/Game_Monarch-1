using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStarter : Interactive
{
    [SerializeField] private TutorialScene scene;

    public override void Interact()
    {
        base.Interact();
        if (!scene.tutorialStarted)
        {
            scene.StartTutorial();
            objectName = "Помошник Б.А.Т.Я.";
            interactionButton = "";
        }
    }
}
