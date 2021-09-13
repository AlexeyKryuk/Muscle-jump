using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour
{
    private Collider _collider;
    private List<Transform> _grabs = new List<Transform>();

    public UnityAction Died;
    public UnityAction Landed;

    public Collider Collider => _collider;
    public List<Transform> Grabs { get => _grabs; set => _grabs = value; }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
}
