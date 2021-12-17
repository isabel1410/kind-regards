using UnityEngine;

/// <summary>
/// Handles animation events from "Navigation Animator Controller".
/// </summary>
public class NavigationAnimationHelper : MonoBehaviour
{
    public NavigationController NavigationController;

    /// <summary>
    /// Sets the rendermode of the UI.
    /// </summary>
    /// <param name="renderMode">The render mode to use.</param>
    public void SetRenderMode(RenderMode renderMode)
    {
        NavigationController.SetRenderMode(renderMode);
    }
}
