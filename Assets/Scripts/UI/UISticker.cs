using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles UI interactions for the stickerbook.
/// </summary>
public class UISticker : MonoBehaviour
{
    [SerializeField]
    private Button UIPrevious;
    [SerializeField]
    private Button UINext;
    [SerializeField]
    private GameObject UIPagePrefab;
    [SerializeField]
    private GameObject UIPageHolder;

    public StickerBookPage CreatePage()
    {
        return Instantiate(UIPagePrefab, UIPageHolder.transform).GetComponent<StickerBookPage>();
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
            UIPrevious.gameObject.SetActive(false);
            UINext.gameObject.SetActive(false);
        }
        else if (currentPage == 0)
        {
            UIPrevious.gameObject.SetActive(false);
            UINext.gameObject.SetActive(true);
        }
        else if (currentPage == pages.Count - 1)
        {
            UIPrevious.gameObject.SetActive(true);
            UINext.gameObject.SetActive(false);
        }
        else
        {
            UIPrevious.gameObject.SetActive(true);
            UINext.gameObject.SetActive(true);
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
