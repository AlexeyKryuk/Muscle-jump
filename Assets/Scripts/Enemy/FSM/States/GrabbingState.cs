using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class GrabbingState : State
{
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _slowingAmount;

    private Rigidbody _rigidbody;
    private Rigidbody _target;
    private Joint _joint;
    private Collider[] _allColliders;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _joint = GetComponent<CharacterJoint>();
        _allColliders = GetComponentsInChildren<Collider>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        EnableColliders();
        _target = Target.Collider.attachedRigidbody;
        _rigidbody.isKinematic = false;
        _collider.enabled = true;

        Animator.SetTrigger("Grab");

        Grab();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _collider.enabled = false;
        Destroy(_joint);
        ReturnSpeed();
    }

    private void Grab()
    {
        ConnectJoint();
        Slowdown();
    }

    private void ConnectJoint()
    {
        if (TryChangeTarget(ref _target))
            _joint.connectedBody = _target;
        else
            _joint.connectedBody = Target.Collider.attachedRigidbody;
    }

    private bool TryChangeTarget(ref Rigidbody target)
    {
        Ray ray = new Ray(transform.position, target.transform.position);
        RaycastHit hit;
        float distance = (target.transform.position - transform.position).magnitude;

        if (Physics.SphereCast(transform.position, 0.5f, target.transform.position, out hit, distance, _layerMask))
        {
            target = hit.collider.attachedRigidbody;
            return true;
        }
        else return false;
    }

    private void Slowdown()
    {
        if (_movement.MoveSpeed > 5)
            _movement.ChangeSpeed(_movement.MoveSpeed - _slowingAmount);
        else
            _slowingAmount = 0;
    }

    private void ReturnSpeed()
    {
        _movement.ChangeSpeed(_movement.MoveSpeed + _slowingAmount);
    }

    private void EnableColliders()
    {
        for (int i = 0; i < _allColliders.Length; i++)
        {
            _allColliders[i].enabled = false;
        }
    }
}
