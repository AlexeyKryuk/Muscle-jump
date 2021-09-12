using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLandingTransition : Transition
{
    [SerializeField] private Landing _landing;

    protected override void OnEnable()
    {
        base.OnEnable();
        _landing.Landed += OnLanded;
    }

    private void OnDisable()
    {
        _landing.Landed -= OnLanded;
    }

    private void OnLanded(Collider playerPlatform)
    {
        if (GetCurrentPlatform() == playerPlatform)
        {
            float distance = Vector3.Distance(Target.transform.position, transform.position);

            if (distance < _landing.TargetRadius)
            {
                NeedTransit = true;
            }
            else
            {
                NeedTransit = false;
            }
        }
    }
}
