using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to instantiate <see cref="DataMail"/> in a UI.
/// </summary>
public class UIMailInstantiator : MonoBehaviour
{
    public Mailbox Mailbox;
    public UIMailbox UIMailbox;

    public Button DeleteButton;
    public Button OpenButton;

    /// <summary>
    /// Instantiates the <paramref name="replyInput"/> by adding the event listeners and setting the <see cref="DataReply"/>, after which this script is destroyed.
    /// </summary>
    /// <param name="replyInput"><see cref="DataReply"/> to instantiate.</param>
    public void Instantiate(DataReply replyInput)
    {
        Mailbox = GetComponentInParent<Transform>().GetComponentInParent<Transform>().GetComponentInParent<Mailbox>();
        UIMailbox = GetComponentInParent<Transform>().GetComponentInParent<Transform>().GetComponentInParent<UIMailbox>();
        GameObject mailboxGameObject = GetComponentInParent<Transform>().gameObject;
        DataReply reply = gameObject.AddComponent<DataReply>();
        reply = replyInput;

        //Set the reply date in the UI
        OpenButton.GetComponentInChildren<Text>().text = reply.DateTime.ToString("yyyy-MM-dd HH:mm");

        //Event listeners
        OpenButton.onClick.AddListener(delegate { Mailbox.OpenMail(reply); });
        OpenButton.onClick.AddListener(delegate { UIMailbox.MarkAsRead(mailboxGameObject); });
        DeleteButton.onClick.AddListener(delegate { Mailbox.DeleteMail(reply, mailboxGameObject); });
        
        Destroy(this);
    }
}
