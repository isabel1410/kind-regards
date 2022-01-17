using System.Collections;
using UnityEngine;

/// <summary>
/// Used to customize, setup and send a <see cref="DataGift"/>.
/// </summary>
public class Gift : MonoBehaviour
{
    [SerializeField]
    private NavigationController navigationController;
    [SerializeField]
    private UIGift uiGift;
    [SerializeField]
    private UIError uiError;
    [SerializeField]
    private StickerBook stickerBook;
    [SerializeField]
    private Companion Companion;
    private DataRequest dataRequest;
    private DataText dataText;
    private DataSticker dataSticker;
    private DataCustomization dataGiftCustomization = new DataCustomization();
    //public DataSticker DataSticker;

    private void Start()
    {
        dataGiftCustomization.Color = uiGift.GiftStartColor;
    }

    /// <summary>
    /// Changes the color of the gift box.
    /// </summary>
    /// <param name="sender">Button which invoked the event.</param>
    public void ChangeColor(GameObject sender)
    {
        Color color = sender.GetComponent<UnityEngine.UI.Image>().color;
        dataGiftCustomization.Color = color;
        uiGift.ChangeColor(color);
    }

    /// <summary>
    /// Adds a sticker to the gift.
    /// </summary>
    public void AddSticker()
    {
        if(stickerBook.UnlockedStickerIDs.Count == 0)
        {
            uiError.Show("You don't have any unlocked stickers!");
            return;
        }

        stickerBook.OnStickerSelected.AddListener(sticker => {
            stickerBook.Exit();
            dataSticker = sticker;
            navigationController.HomeToReply();
            navigationController.ReplyToGift();
        });
        navigationController.GiftToHome();
        stickerBook.Show(false);
    }

    /// <summary>
    /// Sends the gift to the receiver.
    /// </summary>
    public void Send()
    {
        // Send the API request to send a gift/message.
        if (dataSticker == null) APIManager.Instance.SendMessage(dataRequest, dataText);
        else APIManager.Instance.SendMessage(dataRequest, dataText, dataGiftCustomization, dataSticker);

        //Animations, Navigate back to home
        navigationController.GiftToHome();
        Companion.FlyAway();
    }

    #region Visuals

    /// <summary>
    /// Transitions from the mailbox screen to the mail screen.
    /// </summary>
    public void Show(DataRequest request, DataText text)
    {
        dataRequest = request;
        dataText = text;
        dataGiftCustomization = new DataCustomization() { Color = uiGift.GiftStartColor };
        dataSticker = null;
        uiGift.ChangeColor(uiGift.GiftStartColor);
        uiGift.ShowMessage(text);
        navigationController.ReplyToGift();
    }

    /// <summary>
    /// Exits to the mailbox screen.
    /// </summary>
    public void Exit()
    {
        navigationController.GiftToReply();
    }

    #endregion
}
