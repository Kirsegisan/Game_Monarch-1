using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactive : MonoBehaviour
{
    [SerializeField] public string objectName = "Ты какого хрена имя не поставил!?";
    [SerializeField] public string interactionButton = "E)";

    public virtual void Interact()
    {
        Debug.Log("Взаимодействие с: " + objectName);
    }
}
