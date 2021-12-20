using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Shows the request messages and allows the player to send out requests.
/// </summary>
public class Request : MonoBehaviour
{
    private DataText[] requestMessages;

    public NavigationController NavigationController;
    public UIRequest UIRequest;

    /// <summary>
    /// Load all diaries and shows the <see cref="DiaryEntry"/> of today.
    /// </summary>
    /// <returns>True if loading was succesful.</returns>
    /// <exception cref="NotImplementedException">Api call not implemented.</exception>
    public bool LoadMessages()
    {
        try
        {
            requestMessages = APIManager.Instance.DataTexts.FindAll(t => t.Category.Name == "REQUEST").ToArray();
            return true;
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            return false;
        }
    }

    /// <summary>
    /// Send a request.
    /// </summary>
    /// <param name="requestText">Message to send.</param>
    public void SendRequestMessage(DataText requestText)
    {
        print("Message sent: " + requestText.Text);
        APIManager.Instance.SendGiftRequest(requestText);
    }

    #region Visuals

    /// <summary>
    /// Transitions from the home screen to the requests screen.
    /// </summary>
    public void Show()
    {
        if (LoadMessages())
        {
            UIRequest.ShowRequestMessages(requestMessages);
            NavigationController.HomeToRequests();
        }
    }

    /// <summary>
    /// Exits to the home screen.
    /// </summary>
    public void Exit()
    {
        NavigationController.RequestsToHome();
    }

    #endregion
}
