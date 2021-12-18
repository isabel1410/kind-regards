using UnityEngine;
using UnityEngine.Events;

public class InputProcessor : MonoBehaviour
{
    public ScrollEvent OnScroll;

    public enum ScrollDirection
    {
        None,
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        Left,
        UpLeft
    }
}

[System.Serializable]
public class ScrollEvent : UnityEvent<InputProcessor.ScrollDirection> { }