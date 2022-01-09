using UnityEngine;

/// <summary>
/// <see cref="InputProcessor"/> for mobile input.
/// </summary>
/// <remarks>This has not been tested.</remarks>
public class TouchInputProcessor : InputProcessor
{
    private Vector2 firstPosition;
    private Vector2 lastPosition;
    private float minimumDragDistance;

    /// <summary>
    /// Called before the first frame update. Sets <see cref="minimumDragDistance"/>.
    /// </summary>
    private void Start()
    {
        minimumDragDistance = Screen.height * 15 / 100;//15% of screen height
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        //If player is touching the screen
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    firstPosition = lastPosition = touch.position;
                    break;
                case TouchPhase.Moved:
                    lastPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    lastPosition = touch.position;
                    GetDragDistance4(firstPosition, lastPosition, out ScrollDirection dragDirection);
                    OnScroll?.Invoke(dragDirection);
                    break;
            }
        }
    }

    /// <summary>
    /// Get the drag distance of the scroll by the player.
    /// </summary>
    /// <param name="firstPosition">First location where the player initiated the drag.</param>
    /// <param name="lastPosition">Last location where the player initiated the drag.</param>
    /// <param name="dragDirection">The direction the player scrolled in.</param>
    /// <returns>The drag distance.</returns>
    private float GetDragDistance4(Vector2 firstPosition, Vector2 lastPosition, out ScrollDirection dragDirection)
    {
        float dragDistanceHorizontal = firstPosition.x - lastPosition.x;
        float dragDistanceVertical = firstPosition.y - lastPosition.y;
        float dragDistance;

        //Determine if the drag counts as horizontal or vertical
        if (dragDistanceHorizontal > dragDistanceVertical)
        {
            dragDirection = dragDistanceHorizontal < 0 ? ScrollDirection.Left : ScrollDirection.Right;
            dragDistance = Mathf.Abs(dragDistanceHorizontal);
        }
        else
        {
            dragDirection = dragDistanceVertical < 0 ? ScrollDirection.Up : ScrollDirection.Down;
            dragDistance = Mathf.Abs(dragDistanceVertical);
        }

        //Is the drag long enough to count
        if (dragDistance < minimumDragDistance)
        {
            dragDirection = ScrollDirection.None;
            return 0;
        }
        else
        {
            return dragDistanceVertical;
        }
    }
}
