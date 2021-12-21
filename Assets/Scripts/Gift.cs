using UnityEngine;

/// <summary>
/// Used to customize, setup and send a <see cref="DataGift"/>.
/// </summary>
public class Gift : MonoBehaviour
{
    public NavigationController NavigationController;
    public UIGift UIGift;
    public UIError UIError;
    public DataGift DataGift;

    /// <summary>
    /// Changes the color of the gift box.
    /// </summary>
    /// <param name="sender">Button which invoked the event.</param>
    public void ChangeColor(GameObject sender)
    {
        Color color = sender.GetComponent<UnityEngine.UI.Image>().color;
        DataGift.Color = color;
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
            //api call
            throw new System.NotImplementedException();
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
    public void Show(string reply)
    {
        UIGift.ShowMessage(reply);
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
