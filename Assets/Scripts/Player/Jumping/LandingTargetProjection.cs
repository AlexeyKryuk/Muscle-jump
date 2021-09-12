using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LandingTargetProjection : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Landing _landing;
    [SerializeField] private LayerMask _layerMask;

    private float _yAngle;
    private Image _targetImage;

    private void Awake()
    {
        _targetImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (_playerAnimator.GetBool("Jump"))
        {
            _targetImage.enabled = true;

            ProjectionTargetImage();
            RotateAroundYAxis();
            FitRadius(_landing.TargetRadius);
        }
        else
        {
            _targetImage.enabled = false;
        }
    }

    private void ProjectionTargetImage()
    {
        RaycastHit hit;
        Ray ray = new Ray(_landing.transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 100f, _layerMask))
        {
            Vector3 posTarget = transform.position;
            transform.position = new Vector3(posTarget.x, hit.point.y + 0.1f, posTarget.z);
        }
    }

    private void RotateAroundYAxis()
    {
        _yAngle += 20f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(90, _yAngle, 0);
    }

    private void FitRadius(float radius)
    {
        _targetImage.rectTransform.sizeDelta = new Vector2(radius * 2, radius * 2);
    }
}
