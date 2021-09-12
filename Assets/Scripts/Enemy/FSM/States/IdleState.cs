using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private Rigidbody[] _allRigidbodies;
    private Collider[] _allColliders;

    private void Awake()
    {
        _allRigidbodies = GetComponentsInChildren<Rigidbody>();
        _allColliders = GetComponentsInChildren<Collider>();

        for (int i = 0; i < _allRigidbodies.Length; i++)
        {
            _allRigidbodies[i].isKinematic = true;
        }

        for (int i = 0; i < _allColliders.Length; i++)
        {
            _allColliders[i].enabled = false;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Animator.SetBool("Chase", false);
        Animator.SetBool("Grab", false);
    }
}
