using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static List<Transform> GetAllChilds(this Transform me)
    {
        List<Transform> result = new List<Transform>();

        foreach (Transform child in me)
        {
            result.Add(child);

            result.AddRange(child.GetAllChilds());
        }

        return result;
    }

    public static List<GameObject> GetAllChilds(this GameObject me)
    {
        List<GameObject> result = new List<GameObject>();

        foreach (var inside in me.transform.GetAllChilds())
        {
            result.Add(inside.gameObject);
        }

        return result;
    }
}
