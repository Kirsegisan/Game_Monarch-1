﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactive : MonoBehaviour
{
    [SerializeField] public string objectName = "Ты какого хрена имя не поставил!?";

    public virtual void Interact()
    {
        Debug.Log("Взаимодействие с" + objectName);
    }
}
