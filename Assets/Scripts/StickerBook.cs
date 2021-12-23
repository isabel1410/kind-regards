using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class StickerBook : MonoBehaviour
{
    private string STICKERSTORAGEPATH => $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}Stickers.json";
    private List<int> unlockedStickerIDs = new List<int>();
    private const int stickerAmountPerPage = 2;
    private List<StickerBookPage> stickerBookPages = new List<StickerBookPage>();

    public NavigationController NavigationController;

    private int currentPage;

    [SerializeField]
    private UISticker UISticker;

    /// <summary>
    /// Sticker book opens the first page by default.
    /// </summary>
    private void Start()
    {
        LoadStickers();
        currentPage = 0;
    }

    private void LoadStickers()
    {
        if(!File.Exists(STICKERSTORAGEPATH))
        {
            SaveStickerFile();
            return;
        }
        unlockedStickerIDs = JsonConvert.DeserializeObject<List<int>>(File.ReadAllText(STICKERSTORAGEPATH));
    }

    private void SaveStickerFile()
    {
        File.WriteAllText(STICKERSTORAGEPATH, JsonConvert.SerializeObject(unlockedStickerIDs));
    }    

    public bool UnlockSticker(DataSticker sticker)
    {
        if (unlockedStickerIDs.Contains(sticker.Id))
        {
            return false;
        }
        unlockedStickerIDs.Add(sticker.Id);
        SaveStickerFile();
        return true;
    }

    /// <summary>
    /// Makes the pages based on the amount of stickers in the player's inventory.
    /// </summary>
    public List<List<GameObject>> GeneratePages()
    {
        // Not implemented yet.
        // First get all stickers from the player's inventory
        // Go through stickers and get prefab of that sticker
        // Generate one page per x amount of stickers for the player to go through

        if(!APIManager.Instance)
        {
            return null;
        }
        List<List<GameObject>> pages = new List<List<GameObject>>();
        pages.Add(new List<GameObject>());
        List<GameObject> curPage = pages[0];
        for (int i = 0; i < unlockedStickerIDs.Count; i++)
        {
            curPage.Add(APIManager.Instance.DataStickers.Find(s => s.Id == unlockedStickerIDs[i]).GetStickerObject());
            if ((i + 1) % stickerAmountPerPage == 0 && i + 1 != unlockedStickerIDs.Count)
            {
                // Make page
                pages.Add(new List<GameObject>());
                curPage = pages[pages.Count - 1];
            }
        }
        return pages;
    }

    #region Visuals

    /// <summary>
    /// Transitions from the home screen to the sticker screen.
    /// </summary>
    public void Show()
    {
        foreach (StickerBookPage page in stickerBookPages)
        {
            DestroyImmediate(page.gameObject);
        }
        stickerBookPages.Clear();

        List<List<GameObject>> pages = GeneratePages();
        foreach (List<GameObject> page in pages)
        {
            StickerBookPage p = UISticker.CreatePage();
            foreach (GameObject sticker in page)
            {
                p.SetSticker(sticker);
            }
            stickerBookPages.Add(p);
        }
        LoadPage(currentPage);
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
        UISticker.ToggleButtons(stickerBookPages, index);
        UISticker.ShowPage(stickerBookPages, index);
    }
    #endregion
}
