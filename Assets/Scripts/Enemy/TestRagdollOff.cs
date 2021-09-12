using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRagdollOff : MonoBehaviour
{
    private Rigidbody[] _allRigidbodies;
    private Collider[] _allColliders;

    private void Awake()
    {
        _allColliders = GetComponentsInChildren<Collider>();
        _allRigidbodies = GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < _allRigidbodies.Length; i++)
        {
            _allRigidbodies[i].isKinematic = true;
            _allColliders[i].enabled = true;
        }
    }
}
