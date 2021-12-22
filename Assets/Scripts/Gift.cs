using UnityEngine;

/// <summary>
/// Used to customize, setup and send a <see cref="DataGift"/>.
/// </summary>
public class Gift : MonoBehaviour
{
    public NavigationController NavigationController;
    public UIGift UIGift;
    public UIError UIError;
    public DataRequest DataRequest;
    public DataText DataText;
    public DataCustomization DataGiftCustomization;
    //public DataSticker DataSticker;

    /// <summary>
    /// Changes the color of the gift box.
    /// </summary>
    /// <param name="sender">Button which invoked the event.</param>
    /// 

    private void Start()
    {
        DataGiftCustomization.Color = UIGift.GiftStartColor;
    }

    public void ChangeColor(GameObject sender)
    {
        Color color = sender.GetComponent<UnityEngine.UI.Image>().color;
        DataGiftCustomization.Color = color;
        UIGift.ChangeColor(color);
    }

    /// <summary>
    /// Adds a sticker to the gift.
    /// </summary>
    public void AddSticker()
    {
        try
        {
            //api call
            throw new System.NotImplementedException();
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            UIError.Show("Make sure you have an internet connection");
        }
    }

    /// <summary>
    /// Sends the gift to the receiver.
    /// </summary>
    public void Send()
    {
        try
        {
            APIManager.Instance.SendMessage(DataRequest, DataText, DataGiftCustomization);
            NavigationController.GiftToReply();
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            UIError.Show("Make sure you have an internet connection");
        }
    }

    #region Visuals

    /// <summary>
    /// Transitions from the mailbox screen to the mail screen.
    /// </summary>
    public void Show(DataRequest request, DataText text)
    {
        DataRequest = request;
        DataText = text;
        UIGift.ShowMessage(text);
        NavigationController.ReplyToGift();
    }

    /// <summary>
    /// Exits to the mailbox screen.
    /// </summary>
    public void Exit()
    {
        NavigationController.GiftToReply();
    }

    #endregion
}
