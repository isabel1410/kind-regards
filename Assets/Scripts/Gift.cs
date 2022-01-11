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
    private Companion Companion;
    private DataRequest dataRequest;
    private DataText dataText;
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
        try
        {
            //api call
            throw new System.NotImplementedException();
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            uiError.Show("Make sure you have an internet connection");
        }
    }

    /// <summary>
    /// Sends the gift to the receiver.
    /// </summary>
    public void Send()
    {
        APIManager.Instance.SendMessage(dataRequest, dataText, dataGiftCustomization);
        //Animation, then navigate back
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
