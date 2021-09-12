using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private SurfaceSlider _surfaceSlider;

    private Rigidbody _rigidbody;

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

    public void ChangeSpeed(float value)
    {
        _moveSpeed = value;
    }
}
