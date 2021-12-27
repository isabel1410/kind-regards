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
        if (currentPage == 0 && pages.Count - 1 == 0)
        {
            uiPrevious.gameObject.SetActive(false);
            uiNext.gameObject.SetActive(false);
        }
        else if (currentPage == 0)
        {
            uiPrevious.gameObject.SetActive(false);
            uiNext.gameObject.SetActive(true);
        }
        else if (currentPage == pages.Count - 1)
        {
            uiPrevious.gameObject.SetActive(true);
            uiNext.gameObject.SetActive(false);
        }
        else
        {
            uiPrevious.gameObject.SetActive(true);
            uiNext.gameObject.SetActive(true);
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
            if (i == currentPage)
            {
                pages[currentPage].gameObject.SetActive(true);
            }
            else
            {
                pages[i].gameObject.SetActive(false);
            }
        }
    }
}
