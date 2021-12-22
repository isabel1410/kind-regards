using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to instantiate <see cref="DataRequest"/> in a UI.
/// </summary>
public class UIMailInstantiator : MonoBehaviour
{
    [SerializeField]
    private Mailbox Mailbox;
    [SerializeField]
    private UIMailbox UIMailbox;

    [SerializeField]
    private Button DeleteButton;
    [SerializeField]
    private Button OpenButton;

    /// <summary>
    /// Instantiates the <paramref name="replyInput"/> by adding the event listeners and setting the <see cref="DataMessage"/>, after which this script is destroyed.
    /// </summary>
    /// <param name="replyInput"><see cref="DataMessage"/> to instantiate.</param>
    public void Instantiate(DataMessage replyInput)
    {
        Mailbox = GetComponentInParent<Transform>().GetComponentInParent<Transform>().GetComponentInParent<Mailbox>();
        UIMailbox = GetComponentInParent<Transform>().GetComponentInParent<Transform>().GetComponentInParent<UIMailbox>();
        GameObject mailboxGameObject = GetComponentInParent<Transform>().gameObject;
        //DataHolder dataContainer = gameObject.AddComponent<DataHolder>();
        //dataContainer.Data = replyInput;

        //Set the reply date in the UI
        OpenButton.GetComponentInChildren<Text>().text = replyInput.ReceivedAt.ToString("yyyy-MM-dd HH:mm");

        //Event listeners
        OpenButton.onClick.AddListener(delegate { Mailbox.OpenMail(replyInput); });
        OpenButton.onClick.AddListener(delegate { UIMailbox.MarkAsRead(mailboxGameObject); });
        DeleteButton.onClick.AddListener(delegate { Mailbox.DeleteMail(replyInput, mailboxGameObject); });
        
        Destroy(this);
    }
}
