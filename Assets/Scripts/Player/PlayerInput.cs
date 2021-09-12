using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private TouchArea _touch;
    [SerializeField] private float _rotateSpeed;

    private void FixedUpdate()
    {
        if (_touch.IsMoving)
            _movement.Move(new Vector3(_touch.Horizontal * _rotateSpeed, 0, 1));

        _animator.SetBool("Move", _touch.IsMoving);
    }
}
