using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles UI interactions for the stickerbook.
/// </summary>
public class UISticker : MonoBehaviour
{
    [SerializeField]
    private Button uiPrevious;
    [SerializeField]
    private Button uiNext;
    [SerializeField]
    private GameObject uiPagePrefab;
    [SerializeField]
    private GameObject uiPageHolder;

    public StickerBookPage CreatePage()
    {
        return Instantiate(uiPagePrefab, uiPageHolder.transform).GetComponent<StickerBookPage>();
    }

    /// <summary>
    /// Enables / disables buttons based on the shown page.
    /// </summary>
    /// <param name="pages">Pages of the stickerbook.</param>
    /// <param name="currentPage">Index of the page which is shown.</param>
    public void ToggleButtons(List<StickerBookPage> pages, int currentPage)
    {
        if (currentPage == 0)
        {
            uiPrevious.gameObject.SetActive(false);
            uiNext.gameObject.SetActive(pages.Count - 1 != 0);
        }
        else
        {
            uiPrevious.gameObject.SetActive(true);
            uiNext.gameObject.SetActive(currentPage != pages.Count - 1);
        }
    }

    /// <summary>
    /// Hides all the pages and shows the correct page.
    /// </summary>
    /// <param name="pages">Pages of the stickerbook.</param>
    /// <param name="currentPage">Index of the page to show.</param>
    public void ShowPage(List<StickerBookPage> pages, int currentPage)
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].gameObject.SetActive(i == currentPage);
        }
    }
}
