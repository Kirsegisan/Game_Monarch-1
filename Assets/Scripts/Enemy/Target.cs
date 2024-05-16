using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : ObjectData
{
    [SerializeField] private Animator animator;
    public bool isActive = false;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void StartTarget()
    {
        animator.SetBool("On", true);
        isActive = true;
    }

    protected override void Die()
    {
        animator.SetBool("Off", true);
        isActive = false;
    }
}
