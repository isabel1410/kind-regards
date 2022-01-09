using UnityEngine;

/// <summary>
/// <see cref="InputProcessor"/> for desktop. Currently for testing purposes only.
/// </summary>
public class DesktopInputProcessor : InputProcessor
{
    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        float scrollY = Input.mouseScrollDelta.y;
        if (scrollY!= 0)
        {
            OnScroll?.Invoke(scrollY > 0 ? ScrollDirection.Up : ScrollDirection.Down);
        }
    }
}
