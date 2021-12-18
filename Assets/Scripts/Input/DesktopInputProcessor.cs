using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInputProcessor : InputProcessor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scrollY = Input.mouseScrollDelta.y;
        if (scrollY!= 0)
        {
            OnScroll.Invoke(scrollY > 0 ? ScrollDirection.Up : ScrollDirection.Down);
        }
    }
}
