using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDecorateSticker : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    #region Visuals
    /// <summary>
    /// Toggles seeing the stickerbook in diary.
    /// </summary>
    public void ToggleStickerBook()
    {
        if (animator.GetBool("Hidden"))
        {
            animator.SetBool("Hidden", false);
            return;
        }
        animator.SetBool("Hidden", true);
    }
    #endregion
}
