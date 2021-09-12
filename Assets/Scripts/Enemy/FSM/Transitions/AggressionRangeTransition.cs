using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComparisonType
{
    Less = -1,
    Equal,
    More
}

public class AggressionRangeTransition : Transition
{
    [SerializeField] private float _range;
    [SerializeField] private ComparisonType _comparisonType;

    private Collider _platform;

    public float Range { get => _range; private set => _range = value; }

    protected override void OnEnable()
    {
        base.OnEnable();
        _platform = GetCurrentPlatform();
    }

    private void Update()
    {
        if (TryGetDistance(out float distance))
        {
            switch (_comparisonType)
            {
                case ComparisonType.Less:
                    if (distance < Range) NeedTransit = true;
                    break;

                case ComparisonType.More:
                    if (distance > Range) NeedTransit = true;
                    break;

                case ComparisonType.Equal:
                    if (distance == Range) NeedTransit = true;
                    break;

                default:
                    NeedTransit = false;
                    break;
            }
        }
        else
        {
            NeedTransit = false;
        }
    }

    private bool TryGetDistance(out float distance)
    {
        if (_platform != null)
        {
            Vector3 closestPoint = _platform.ClosestPoint(Target.transform.position);
            Vector2 targetPosXZ = new Vector2(Target.transform.position.x, Target.transform.position.z);
            Vector2 closestPosXZ = new Vector2(closestPoint.x, closestPoint.z);

            distance = Vector2.Distance(targetPosXZ, closestPosXZ);

            return true;
        }
        else
        {
            distance = 0;
            return false;
        }
    }
}
