using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputProcessor : InputProcessor
{
    private Vector2 firstPosition;
    private Vector2 lastPosition;
    private float minimumDragDistance;

    // Start is called before the first frame update
    private void Start()
    {
        minimumDragDistance = Screen.height * 15 / 100;//15% of screen height
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            //Phases
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
                    GetDragDirection4(firstPosition, lastPosition, out ScrollDirection dragDirection);
                    OnScroll.Invoke(dragDirection);
                    break;
            }
        }
    }

    private float GetDragDirection4(Vector2 firstPosition, Vector2 lastPosition, out ScrollDirection dragDirection)
    {
        float dragDistanceHorizontal = firstPosition.x - lastPosition.x;
        float dragDistanceVertical = firstPosition.y - lastPosition.y;

        if (dragDistanceHorizontal > dragDistanceVertical)
        {
            dragDirection = dragDistanceHorizontal < 0 ? ScrollDirection.Left : ScrollDirection.Right;

            dragDistanceHorizontal = Mathf.Abs(dragDistanceHorizontal);
            if (dragDistanceHorizontal < minimumDragDistance)
            {
                dragDirection = ScrollDirection.None;
                return 0;
            }
            else
            {
                return dragDistanceHorizontal;
            }
        }
        else
        {
            dragDirection = dragDistanceVertical < 0 ? ScrollDirection.Up : ScrollDirection.Down;

            dragDistanceVertical = Mathf.Abs(dragDistanceVertical);
            if (dragDistanceVertical < minimumDragDistance)
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
}
