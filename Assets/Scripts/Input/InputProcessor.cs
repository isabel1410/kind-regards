using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Used to register control input events.
/// </summary>
public class InputProcessor : MonoBehaviour
{
    protected ScrollEvent OnScroll;

    /// <summary>
    /// Direction the used has scrolled in.
    /// </summary>
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

/// <summary>
/// Used to invoke an input scroll event.
/// </summary>
[System.Serializable]
public class ScrollEvent : UnityEvent<InputProcessor.ScrollDirection> { }