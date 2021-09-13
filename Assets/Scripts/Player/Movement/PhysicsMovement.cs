using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private SurfaceSlider _surfaceSlider;

    private Rigidbody _rigidbody;
    private float _overflowSpeed;

    public float MoveSpeed => _moveSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        Vector3 directionAlongSurface = _surfaceSlider.Project(direction.normalized);
        Vector3 offset = directionAlongSurface * _moveSpeed * Time.deltaTime;

        _rigidbody.MovePosition(_rigidbody.position + offset);
        Rotate(direction);
    }

    private void Rotate(Vector3 direction)
    {
        Vector3 lookDirection = direction + transform.position;
        transform.LookAt(new Vector3(lookDirection.x, transform.position.y, lookDirection.z));
    }

    public void DecreaseSpeed(float value)
    {
        if (_moveSpeed - value >= _minSpeed)
            _moveSpeed -= value;
        else
        {
            _overflowSpeed += _moveSpeed - value - _minSpeed;
            _moveSpeed = _minSpeed;
        }
    }

    public void IncreaseSpeed(float value)
    {
        _overflowSpeed += value;
        
        if (_overflowSpeed > 0)
        {
            _moveSpeed += _overflowSpeed;
            _overflowSpeed = 0;
        }
    }
}
