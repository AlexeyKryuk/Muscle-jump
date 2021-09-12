using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour
{
    private Collider _collider;

    public UnityAction Died;
    public UnityAction Landed;

    public Collider Collider => _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
}
