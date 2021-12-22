using System;
using UnityEngine;

/// <summary>
/// Shows the request messages and allows the player to send out requests.
/// </summary>
public class Request : MonoBehaviour
{
    private DataText[] requestMessages;

    [SerializeField]
    private NavigationController NavigationController;
    [SerializeField]
    private UIRequest UIRequest;
    [SerializeField]
    private UIError UIError;

    /// <summary>
    /// Load all diaries and shows the <see cref="DiaryEntry"/> of today.
    /// </summary>
    /// <returns>True if loading was succesful.</returns>
    /// <exception cref="NotImplementedException">Api call not implemented.</exception>
    public bool LoadMessages()
    {
        try
        {
#if UNITY_EDITOR
            if (APIManager.Instance) requestMessages = APIManager.Instance.DataTexts.FindAll(t => t.Category.Name == "REQUEST").ToArray();
            else requestMessages = new DataText[]
            {
                new DataText() {Text = "Message 1"},
                new DataText() {Text = "Message 2"},
                new DataText() {Text = "Message 3"}
            };
#else
            if (APIManager.Instance) requestMessages = APIManager.Instance.DataTexts.FindAll(t => t.Category.Name == "REQUEST").ToArray();
            else return false;
#endif
            return true;
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            UIError.Show("Make sure you have an internet connection");
            return false;
        }
    }

    /// <summary>
    /// Send a request.
    /// </summary>
    /// <param name="requestText">Message to send.</param>
    public void SendRequestMessage(DataText requestText)
    {
        try
        {
            if (APIManager.Instance) APIManager.Instance.SendGiftRequest(requestText);
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            UIError.Show("Make sure you have an internet connection");
        }
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
