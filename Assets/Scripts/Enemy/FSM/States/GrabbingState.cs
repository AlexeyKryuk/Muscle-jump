using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Rigidbody))]
public class GrabbingState : State
{
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private float _Movespeed;
    [SerializeField] private float _distanceToTarget;
    [SerializeField] private float _slowingAmount;

    private Transform _target;
    private Rigidbody _rigidbody;
    private Collider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Grab();

        Animator.SetTrigger("Grab");
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Target.Grabs.Remove(transform);
        ReturnSpeed();
    }

    private void Update()
    {
        MoveToTarget(_target.position, _Movespeed);
        RotateToTarget(_target.position);
    }

    private void Grab()
    {
        _target = GetTarget();

        MakePhysical();
        Slowdown();
    }

    private void MoveToTarget(Vector3 target, float speed)
    {
        if (Vector3.Distance(transform.position, target) > _distanceToTarget)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            transform.position = newPos;
        }
    }

    private void RotateToTarget(Vector3 target)
    {
        Vector3 targetDirection = (target - transform.position).normalized;

        if (targetDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private Transform GetTarget()
    {
        System.Random random = new System.Random();
        Transform target;
        
        if (Target.Grabs.Count > 2)
        {
            int index = random.Next(Target.Grabs.Count - 3, Target.Grabs.Count - 1);
            target = Target.Grabs[index];
        }
        else
        {
            target = Target.transform;
        }

        Target.Grabs.Add(transform);
        return target;
    }

    private void Slowdown()
    {
        _movement.DecreaseSpeed(_slowingAmount);
    }

    private void ReturnSpeed()
    {
        _movement.IncreaseSpeed(_slowingAmount);
    }

    private void MakePhysical()
    {
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
    }
}
