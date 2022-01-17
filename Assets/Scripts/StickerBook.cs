using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public class StickerBook : MonoBehaviour
{
    private string STICKERSTORAGEPATH => $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}Stickers.json";
    private List<int> unlockedStickerIDs = new List<int>();
    private const int stickerAmountPerPage = 2;
    protected List<StickerBookPage> stickerBookPages = new List<StickerBookPage>();

    [SerializeField]
    private NavigationController navigationController;
    public UnityEvent<DataSticker> OnStickerSelected;

    public IReadOnlyList<int> UnlockedStickerIDs => unlockedStickerIDs;

    protected int currentPage;

    [SerializeField]
    protected UISticker uiSticker;

    /// <summary>
    /// Sticker book opens the first page by default.
    /// </summary>
    public virtual void Start()
    {
        LoadStickers();
        currentPage = 0;
    }

    /// <summary>
    /// Load the unlocked sticker ids from storage.
    /// </summary>
    private void LoadStickers()
    {
        // If the file doesn't exist create it.
        if(!File.Exists(STICKERSTORAGEPATH))
        {
            if (APIManager.Instance && APIManager.Instance.DataStickers.Count > 0) unlockedStickerIDs.Add(APIManager.Instance.DataStickers.Random().Id);
            SaveStickerFile();
            return;
        }
        unlockedStickerIDs = JsonConvert.DeserializeObject<List<int>>(File.ReadAllText(STICKERSTORAGEPATH));
    }

    /// <summary>
    /// Save the unlocked sticker ids to storage.
    /// </summary>
    private void SaveStickerFile()
    {
        File.WriteAllText(STICKERSTORAGEPATH, JsonConvert.SerializeObject(unlockedStickerIDs));
    }    

    /// <summary>
    /// Unlock a sticker if it hasn't been unlocked yet.
    /// </summary>
    /// <param name="sticker">The sticker you want to unlock</param>
    /// <returns>If you unlocked the sticker aka if the sticker is not new</returns>
    public bool UnlockSticker(DataSticker sticker)
    {
        // Check if the sticker is unlocked.
        if (unlockedStickerIDs.Contains(sticker.Id))
        {
            return false;
        }

        // Unlock the sticker.
        unlockedStickerIDs.Add(sticker.Id);

        // Update the unlocked stickers file.
        SaveStickerFile();
        return true;
    }

    /// <summary>
    /// Makes the pages based on the stickers in the player's inventory.
    /// </summary>
    /// <returns>All the generated pages</returns>
    public List<List<DataSticker>> GeneratePages()
    {
        // First we check if the API exists.
        if(!APIManager.Instance)
        {
            return null;
        }

        // Then we create a list of pages where there is 1 page by default.
        List<List<DataSticker>> pages = new List<List<DataSticker>>
        {
            new List<DataSticker>()
        };

        // Set the current page for the loop below.
        List<DataSticker> curPage = pages[0];

        // Go through the unlocked sticker ids.
        for (int i = 0; i < unlockedStickerIDs.Count; i++)
        {
            // For each sticker it adds the sticker to the page.
            curPage.Add(APIManager.Instance.DataStickers.Find(s => s.Id == unlockedStickerIDs[i]));

            // If the amount of stickers on the page is bigger then what is allowed on the page generate a new page and set that one to the current page.
            if ((i + 1) % stickerAmountPerPage == 0 && i + 1 != unlockedStickerIDs.Count)
            {
                // Make page
                pages.Add(new List<DataSticker>());
                curPage = pages[pages.Count - 1];
            }
        }

        // Return the generated pages.
        return pages;
    }

    #region Visuals

    /// <summary>
    /// Transitions from the home screen to the sticker screen.
    /// </summary>
    public void Show(bool clearListeners = true)
    {
        if (clearListeners) OnStickerSelected?.RemoveAllListeners();

        // On show clear all the pages that exist.
        foreach (StickerBookPage page in stickerBookPages)
        {
            DestroyImmediate(page.gameObject);
        }
        stickerBookPages.Clear();

        // Regenerate the pages.
        List<List<DataSticker>> pages = GeneratePages();
        
        // Go through all the stickers.
        foreach (List<DataSticker> page in pages)
        {
            // Create the sticker page in the game world.
            StickerBookPage p = uiSticker.CreatePage();
            foreach (DataSticker sticker in page)
            {
                // Add the sticker object to the page.
                p.SetSticker(sticker);
            }
            // Listen to sticker selected event
            p.OnStickerSelect.AddListener(sticker => OnStickerSelected?.Invoke(sticker));

            // Add the sticker page to the cache (used for deleting the pages).
            stickerBookPages.Add(p);
        }

        // Load the current page.
        LoadPage(currentPage);

        // Navigate to stickers.
        navigationController.HomeToSticker();
    }

    /// <summary>
    /// Transitions from the sticker screen to the home screen.
    /// </summary>
    public virtual void Exit()
    {
        navigationController.StickerToHome();
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
        uiSticker.ToggleButtons(stickerBookPages, index);
        uiSticker.ShowPage(stickerBookPages, index);
    }

    #endregion
}
