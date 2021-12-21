using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains functions to update the UI of the reply screen.
/// </summary>
public class UIReply : MonoBehaviour
{
    private float height;
    public Text UIMessage;
    public Button ReplyButtonPrefab;
    public Reply Reply;
    public Transform RepliesContainerTransform;

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    private void Start()
    {
        height = RepliesContainerTransform.GetComponentInParent<RectTransform>().rect.height;
    }

    /// <summary>
    /// Show the sent message in the UI.
    /// </summary>
    /// <param name="message">Sent message to show.</param>
    public void ShowMessage(DataMail message)
    {
        UIMessage.text = message.SentMessage;
    }

    /// <summary>
    /// Creates a <see cref="Button"/> for each reply.
    /// </summary>
    /// <param name="replies">Replies to show.</param>
    public void ShowReplies(string[] replies)
    {
        //Create buttons and add the necessary properties
        float positionY = 0;
        foreach (string reply in replies)
        {
            Button requestMessageButton = Instantiate(ReplyButtonPrefab);
            RectTransform rectTransform = requestMessageButton.gameObject.GetComponent<RectTransform>();

            requestMessageButton.transform.SetParent(RepliesContainerTransform, false);
            rectTransform.anchoredPosition = new Vector2(0, positionY);

            requestMessageButton.onClick.AddListener(delegate { Reply.SendReply(reply); });
            requestMessageButton.GetComponentInChildren<Text>().text = reply;

            positionY -= rectTransform.rect.height;
        }

        //Set the height of the container based on the amount of replies and the height of the prefab
        RectTransform containerRectTransform = RepliesContainerTransform.GetComponent<RectTransform>();
        containerRectTransform.sizeDelta = new Vector2(
            containerRectTransform.sizeDelta.x,
            Mathf.Abs(positionY) - height);
    }

    /// <summary>
    /// Destroys all the buttons containing the replies.
    /// </summary>
    public void DestroyReplyButtons()
    {
        foreach (Transform buttonTransform in RepliesContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }
    }
}
