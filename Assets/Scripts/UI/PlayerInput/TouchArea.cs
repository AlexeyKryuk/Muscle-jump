using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class TouchArea : MonoBehaviour, IPointerMoveHandler, IPointerUpHandler, IPointerDownHandler
{
    public float Horizontal { get; private set; }
    public bool IsMoving { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsMoving = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsMoving = false;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        float input = eventData.position.x;

        Vector2 currentRange = new Vector2(0, Screen.width);
        Vector2 targetRange = new Vector2(-1, 1);

        Horizontal = ConvertToRange(input, currentRange, targetRange);
    }

    private float ConvertToRange(float value, Vector2 from, Vector2 to)
    {
        return (value - from.x) / (from.y - from.x) * (to.y - to.x) + to.x;
    }
}
