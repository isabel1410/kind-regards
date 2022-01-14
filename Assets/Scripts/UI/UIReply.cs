using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains functions to update the UI of the reply screen.
/// </summary>
public class UIReply : MonoBehaviour
{
    [SerializeField]
    private Text uiMessage;
    [SerializeField]
    private Button replyButtonPrefab;
    [SerializeField]
    private Reply reply;
    [SerializeField]
    private Transform repliesContainerTransform;

    private float originalHeight;

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    private void Start()
    {
        originalHeight = repliesContainerTransform.GetComponentInParent<RectTransform>().rect.height;
    }

    /// <summary>
    /// Show the sent message in the UI.
    /// </summary>
    /// <param name="message">Sent message to show.</param>
    public void ShowMessage(DataRequest message)
    {
        if (message == null) return;
        uiMessage.text = message.DataText.Text;
    }

    /// <summary>
    /// Creates a <see cref="Button"/> for each reply.
    /// </summary>
    /// <param name="replies">Replies to show.</param>
    public void ShowReplies(DataText[] replies)
    {
        if (replies == null) return;
        //Create buttons and add the necessary properties
        float positionY = 0;
        foreach (DataText reply in replies)
        {
            Button requestMessageButton = Instantiate(replyButtonPrefab);
            RectTransform rectTransform = requestMessageButton.gameObject.GetComponent<RectTransform>();

            requestMessageButton.transform.SetParent(repliesContainerTransform, false);
            rectTransform.anchoredPosition = new Vector2(0, positionY);

            requestMessageButton.onClick.AddListener(delegate { this.reply.SendReply(reply); });
            requestMessageButton.GetComponentInChildren<Text>().text = reply.Text;

            positionY -= rectTransform.rect.height;
        }
        SetContainerHeight(Mathf.Abs(positionY));
    }

    /// <summary>
    /// Sets the height of <see cref="repliesContainerTransform"/> based on the amount of reply messages.
    /// </summary>
    /// <param name="positionY">Cumulative height of all request messages.</param>
    private void SetContainerHeight(float positionY)
    {
        //Set the height of the container based on the amount of messages and the height of the prefab
        RectTransform containerRectTransform = repliesContainerTransform.GetComponent<RectTransform>();
        containerRectTransform.sizeDelta = new Vector2(
            containerRectTransform.sizeDelta.x,
            positionY - originalHeight);
    }

    /// <summary>
    /// Destroys all the buttons containing the replies.
    /// </summary>
    public void DestroyReplyButtons()
    {
        foreach (Transform buttonTransform in repliesContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }
    }
}
