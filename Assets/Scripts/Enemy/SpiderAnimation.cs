using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimation : MonoBehaviour
{

    private Animator spiderAnimator;

    // Start is called before the first frame update
    void Start()
    {
        spiderAnimator = GetComponent<Animator>();
    }

    public void Run()
    {
        spiderAnimator.SetBool("Run", true);
        spiderAnimator.SetBool("isShoot", false);

    }

    public void Shoot()
    {
        spiderAnimator.SetBool("Shoot", true);
        spiderAnimator.SetBool("isShoot", true);

    }

    public void Stop()
    {
        spiderAnimator.SetBool("Stop", true);
        spiderAnimator.SetBool("isShoot", false);

    }
}
