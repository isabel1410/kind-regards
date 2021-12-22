using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Redirects calls for the stickerbook.
/// </summary>
public class Sticker : MonoBehaviour
{
    public NavigationController NavigationController;

    [SerializeField]
    private List<GameObject> pages;
    private int currentPage;

    [SerializeField]
    private UISticker UISticker;

    /// <summary>
    /// Sticker book opens the first page by default.
    /// </summary>
    private void Start()
    {
        currentPage = 0;
        LoadPage(currentPage);
    }

    #region Visuals

    /// <summary>
    /// Transitions from the home screen to the sticker screen.
    /// </summary>
    public void Show()
    {
        NavigationController.HomeToSticker();
    }

    /// <summary>
    /// Transitions from the sticker screen to the home screen.
    /// </summary>
    public void Exit()
    {
        NavigationController.StickerToHome();
    }

    /// <summary>
    /// Goes to the next page of the sticker book.
    /// </summary>
    public void NextPage()
    {
        int nextPage = currentPage + 1;
        LoadPage(nextPage);
        currentPage = nextPage;
    }

    /// <summary>
    /// Goes to the previous page of the sticker book.
    /// </summary>
    public void PreviousPage()
    {
        int previousPage = currentPage - 1;
        LoadPage(previousPage);
        currentPage = previousPage;
    }

    /// <summary>
    /// Shows the page based on <paramref name="index"/> and disables / enables next / previous buttons.
    /// </summary>
    /// <param name="index">Index of the page to load.</param>
    public void LoadPage(int index)
    {
        UISticker.ToggleButtons(pages, index);
        UISticker.ShowPage(pages, index);
    }

    #endregion
}
