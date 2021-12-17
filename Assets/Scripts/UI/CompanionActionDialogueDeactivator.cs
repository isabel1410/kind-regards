using UnityEngine;

/// <summary>
/// The only purpose is to set the canvas inactive once the companion action dialogue choices are hidden.
/// </summary>
public class CompanionActionDialogueDeactivator : MonoBehaviour
{
    /// <summary>
    /// Sets the canvas inactive once the companion action dialogue choices are hidden.
    /// </summary>
    public void DeactivateCompanionActions()
    {
        gameObject.SetActive(false);
    }
}
