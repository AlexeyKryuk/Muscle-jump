using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetReachedTransition : Transition
{
    [SerializeField] private Landing _landing;
    [SerializeField] private float _range;

    private Collider _playerCurrentPlatform;

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
        _playerCurrentPlatform = playerPlatform;
    }

    private void Update()
    {
        if (GetCurrentPlatform() == _playerCurrentPlatform)
        {
            float distance = Vector3.Distance(Target.transform.position, transform.position);

            if (distance < _range)
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
