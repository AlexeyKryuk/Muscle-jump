using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbing : MonoBehaviour
{
    //[SerializeField] private Animator _animator;
    //[SerializeField] private PhysicsMovement _movement;
    //[SerializeField] private CharacterJoint _characterJoint;
    //[SerializeField] private LayerMask _layerMask;
    //[SerializeField] private float _slowingAmount;

    //private Rigidbody _rigidbody;
    //private bool _isGrabbing;

    //private void Awake()
    //{
    //    _rigidbody = GetComponent<Rigidbody>();
    //}

    //private void Update()
    //{
    //    if (_animator.GetBool("Chase"))
    //    {
    //        if (_isGrabbing)
    //        {
    //            _characterJoint.gameObject.SetActive(true);
    //            _characterJoint.connectedBody = _target.attachedRigidbody;
    //            //MoveByTarget(_target.transform.position);
    //        }
    //        else
    //        {
    //            TryChangeTarget(ref _target);

    //            Vector3 closestPoint = _target.ClosestPoint(transform.position);

    //            if (Vector3.Distance(closestPoint, transform.position) < 5f)
    //            {
    //                Grab();
    //            }
    //        }
    //    }
    //}

    //private bool TryChangeTarget(ref Collider target)
    //{
    //    Ray ray = new Ray(transform.position + Vector3.forward, target.transform.position);
    //    RaycastHit hit;
    //    float distance = (target.transform.position - transform.position).magnitude;

    //    if (Physics.Raycast(ray, out hit, distance, _layerMask))
    //    {
    //        target = hit.collider;
    //        return true;
    //    }
    //    else return false;
    //}

    //private void Grab()
    //{
    //    _chase.enabled = false;
    //    _isGrabbing = true;

    //    if (_movement.MoveSpeed > 5)
    //    {
    //        float initialSpeed = _movement.MoveSpeed;
    //        _movement.ChangeSpeed(initialSpeed - _slowingAmount);
    //    }

    //    _animator.SetBool("Grab", true);
    //}

    //private void StopGrabbing()
    //{
    //    _isGrabbing = false;
    //    _animator.SetBool("Grab", false);
    //    _tossing.Toss(10f);
    //}

    //private void MoveByTarget(Vector3 target)
    //{
    //    Vector3 newPos = Vector3.Lerp(_rigidbody.position, target, 10f * Time.deltaTime);
    //    _rigidbody.MovePosition(newPos);

    //    Vector3 targetDirection = (target - transform.position).normalized;

    //    if (targetDirection != Vector3.zero)
    //    {
    //        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
    //        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    //    }
    //}
}
