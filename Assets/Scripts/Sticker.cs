using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sticker : MonoBehaviour
{
    public NavigationController NavigationController;

    [SerializeField]
    private List<GameObject> pages;
    int currentPage;

    public UISticker UISticker;

    /// <summary>
    /// Sticker book opens at the first page by default.
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

    public void LoadPage(int index)
    {
        UISticker.Default(pages, index);
        UISticker.ShowPage(pages, index);
    }
    #endregion
}
