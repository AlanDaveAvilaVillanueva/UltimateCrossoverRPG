using UnityEngine;

public class MoveUI : MonoBehaviour
{
    public RectTransform target;

    public void SetX(float x)
    {
        Vector2 pos = target.anchoredPosition;
        pos.x = x;
        target.anchoredPosition = pos;
    }

    public void SetY(float y)
    {
        Vector2 pos = target.anchoredPosition;
        pos.y = y;
        target.anchoredPosition = pos;
    }
}
