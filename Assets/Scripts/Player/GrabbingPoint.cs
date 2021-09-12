using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrabbingPoint : MonoBehaviour
{
    public Enemy GrabbingEnemy { get; set; }
    public Rigidbody Rigidbody { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
