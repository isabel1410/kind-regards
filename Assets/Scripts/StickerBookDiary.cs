using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StickerBookDiary : StickerBook
{
    [SerializeField] private StickerBook stickerBook;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Animator animator;


    // Start is called before the first frame update
    public override void Start()
    {
        currentPage = 0;
    }

    public void Show()
    {
        currentPage = 0;

        foreach (StickerBookPage page in stickerBookPages)
        {
            DestroyImmediate(page.gameObject);
        }
        stickerBookPages.Clear();

        // Regenerate the pages.
        List<List<DataSticker>> pages = stickerBook.GeneratePages();

        // Go through all the stickers.
        foreach (List<DataSticker> page in pages)
        {
            // Create the sticker page in the game world.
            StickerBookPage p = uiSticker.CreatePage();
            if (!p.gameObject.activeSelf)
            {
                p.gameObject.SetActive(true);
            }
            foreach (DataSticker sticker in page)
            {
                // Add the sticker object to the page.
                GameObject stickerObj = p.SetSticker(sticker);
                stickerObj.AddComponent<DragAndDrop>().canvas = canvas;
            }

            // Add the sticker page to the cache (used for deleting the pages).
            stickerBookPages.Add(p);
        }

        // Load the current page.
        LoadPage(currentPage);
    }

    /// <summary>
    /// Toggles seeing the stickerbook in diary.
    /// </summary>
    public void ToggleButton()
    {
        if (!APIManager.Instance || APIManager.Instance.DataStickers.Count == 0) return;
        if (animator.GetBool("Hidden"))
        {
            Show();
            animator.SetBool("Hidden", false);
            return;
        }
        animator.SetBool("Hidden", true);
    }

    public override void Exit()
    {

    }
}
