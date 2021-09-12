using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossingState : State
{
    [SerializeField] private float _force;

    private Rigidbody[] _allRigidbodies;
    private Collider[] _allColliders;

    private void Awake()
    {
        _allRigidbodies = GetComponentsInChildren<Rigidbody>();
        _allColliders = GetComponentsInChildren<Collider>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        MakePhysical(true);
        AddForce(_force);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        MakePhysical(false);
    }

    private void MakePhysical(bool value)
    {
        Animator.enabled = !value;

        for (int i = 0; i < _allRigidbodies.Length; i++)
        {
            _allRigidbodies[i].isKinematic = !value;
        }
        for (int i = 0; i < _allColliders.Length; i++)
        {
            _allColliders[i].enabled = value;
        }
    }

    private void AddForce(float force)
    {
        Vector3 ownDirection = (Target.transform.position - transform.position).normalized;
        Vector3 direction = -ownDirection / 3 + Vector3.up;

        foreach (var rigidbody in _allRigidbodies)
        {
            rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}
