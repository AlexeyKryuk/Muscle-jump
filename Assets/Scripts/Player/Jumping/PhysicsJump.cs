using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsJump : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _layerMask;
    [Header("Jump speed regulation")]
    [SerializeField] private float _fallSpeed;
    [SerializeField] private float _takeoff;
    [SerializeField] private float _jumpStartBoundary;
    [SerializeField] private float _jumpForce;

    private Rigidbody _rigidbody;
    private Collider _currentPlatform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _currentPlatform = GetCurrentPlatform();
    }

    private void Update()
    {
        JumpSpeedRegulation();

        if (_currentPlatform != null && CheckDistance())
            Jump();
    }

    private void JumpSpeedRegulation()
    {
        if (_rigidbody.velocity.y < 0)
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (_fallSpeed - 1) * Time.deltaTime;
        else if (_rigidbody.velocity.y > 0)
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (_takeoff - 1) * Time.deltaTime;
    }

    private void Jump()
    {
        Debug.Log("Junp");
        _rigidbody.velocity = Vector3.up * _jumpForce;

        _currentPlatform = null;
        _animator.SetBool("Jump", true);
    }

    private bool CheckDistance()
    {
        Vector3 closestPoint = _currentPlatform.ClosestPoint(transform.position);

        Vector2 xzPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 xzPosClosestPoint = new Vector2(closestPoint.x, closestPoint.z);

        float distance = Vector2.Distance(xzPos, xzPosClosestPoint);

        if (distance > _jumpStartBoundary)
            return true;
        else
            return false;
    }

    private Collider GetCurrentPlatform()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        float maxDistance = 10f;

        if (Physics.Raycast(ray, out hit, maxDistance, _layerMask))
        {
            return hit.collider;
        }
        else
            return null;
    }

    public void SetCurrentPlatform(Collider platform)
    {
        _currentPlatform = platform;
    }
}
