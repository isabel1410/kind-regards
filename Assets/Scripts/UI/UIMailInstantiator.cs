using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to instantiate <see cref="DataRequest"/> in a UI.
/// </summary>
public class UIMailInstantiator : MonoBehaviour
{
    private Mailbox mailbox;
    private UIMailbox uiMailbox;

    [SerializeField]
    private Button deleteButton;
    [SerializeField]
    private Button openButton;

    /// <summary>
    /// Instantiates the <paramref name="replyInput"/> by adding the event listeners and setting the <see cref="DataMessage"/>, after which this script is destroyed.
    /// </summary>
    /// <param name="replyInput"><see cref="DataMessage"/> to instantiate.</param>
    public void Instantiate(DataMessage replyInput)
    {
        mailbox = GetComponentInParent<Transform>().GetComponentInParent<Transform>().GetComponentInParent<Mailbox>();
        uiMailbox = GetComponentInParent<Transform>().GetComponentInParent<Transform>().GetComponentInParent<UIMailbox>();
        GameObject mailboxGameObject = GetComponentInParent<Transform>().gameObject;
        //DataHolder dataContainer = gameObject.AddComponent<DataHolder>();
        //dataContainer.Data = replyInput;

        //Set the reply date in the UI
        openButton.GetComponentInChildren<Text>().text = replyInput.ReceivedAt.ToString("yyyy-MM-dd HH:mm");

        //Event listeners
        openButton.onClick.AddListener(delegate { mailbox.OpenMail(replyInput); });
        openButton.onClick.AddListener(delegate { uiMailbox.MarkAsRead(mailboxGameObject); });
        deleteButton.onClick.AddListener(delegate { mailbox.DeleteMail(replyInput, mailboxGameObject); });
        
        Destroy(this);
    }
}
