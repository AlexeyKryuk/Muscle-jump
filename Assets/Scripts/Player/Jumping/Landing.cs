using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Landing : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _landingEffect;
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private PhysicsJump _physicsJump;
    [Header("Slowing")]
    [SerializeField] private float _slowingRatio;
    [SerializeField] private float _timeRatio;
    [Space]
    [SerializeField] private float _targetRadius;

    public float TargetRadius { get => _targetRadius; private set => _targetRadius = value; }

    public UnityAction<Collider> Landed;

    private void OnCollisionEnter(Collision collision)
    {
        if (_animator.GetBool("Jump"))
        {
            _animator.SetBool("Jump", false);
            _landingEffect.Play();

            StartCoroutine(SlowMovement(_timeRatio, _slowingRatio));
            _physicsJump.SetCurrentPlatform(collision.collider);

            Landed?.Invoke(collision.collider);
        }
    }

    private IEnumerator SlowMovement(float time, float slowSpeed)
    {
        if (slowSpeed != 0)
        {
            _movement.DecreaseSpeed(slowSpeed);

            yield return new WaitForSeconds(time);

            _movement.IncreaseSpeed(slowSpeed);
        }
    }
}
