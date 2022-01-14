using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains functions to update the UI of the mailbox screen.
/// </summary>
public class UIMailbox : MonoBehaviour
{
    [SerializeField]
    private GameObject mailGameObjectPrefab;
    [SerializeField]
    private Transform mailContainerTransform;

    private float height;

    /// <summary>
    /// Called before the first frame update.
    /// Gets the original height of <see cref="mailContainerTransform"/>.
    /// </summary>
    private void Start()
    {
        height = mailContainerTransform.GetComponentInParent<RectTransform>().rect.height;
    }


    /// <summary>
    /// Creates and instantiates a <see cref="GameObject"/> for each reply.
    /// </summary>
    /// <param name="replies">Replies to show.</param>
    public void ShowReplies(DataMessage[] replies)
    {
        //Create gameObjects and add the necessary properties
        float positionY = 0;
        foreach (DataMessage reply in replies)
        {
            GameObject mailGameObject = Instantiate(mailGameObjectPrefab);
            RectTransform rectTransform = mailGameObject.GetComponent<RectTransform>();

            mailGameObject.transform.SetParent(mailContainerTransform, false);
            rectTransform.anchoredPosition = new Vector2(0, positionY);

            mailGameObject.GetComponent<UIMailInstantiator>().Instantiate(reply);

            if (reply.Seen)
            {
                mailGameObject.GetComponentInChildren<RawImage>().color = Color.gray;
            }

            positionY -= rectTransform.rect.height;
        }

        //Set the height of the container based on the amount of messages and the height of the prefab
        RectTransform containerRectTransform = mailContainerTransform.GetComponent<RectTransform>();
        containerRectTransform.sizeDelta = new Vector2(
            containerRectTransform.sizeDelta.x,
            Mathf.Abs(positionY) - height);
    }

    /// <summary>
    /// Destroys all the gameObjects containing the mails.
    /// </summary>
    public void DestroyMailboxGameObjects()
    {
        foreach (Transform mailTransform in mailContainerTransform)
        {
            Destroy(mailTransform.gameObject);
        }
    }

    /// <summary>
    /// Decreases the container height and destroys <paramref name="mailGameObject"/>.
    /// </summary>
    /// <param name="mailGameObject"><see cref="GameObject"/> the holds the <see cref="DataRequest"/> in the <see cref="UIMailbox"/>.</param>
    public void DestroyMailGameObject(GameObject mailGameObject)
    {
        RectTransform containerRectTransform = mailContainerTransform.GetComponent<RectTransform>();
        containerRectTransform.sizeDelta = new Vector2(
            containerRectTransform.sizeDelta.x,
            containerRectTransform.sizeDelta.y - mailGameObject.GetComponent<RectTransform>().sizeDelta.y);

        //Move other mailGameObjects up if they are below the destroyable mailGameObject
        bool encounteredDestroyable = false;
        foreach (Transform otherMailGameObject in mailContainerTransform.transform)
        {
            if (!encounteredDestroyable)
            {
                if (otherMailGameObject == mailGameObject.transform)
                {
                    encounteredDestroyable = true;
                }
                continue;
            }

            RectTransform rectTransform = otherMailGameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(
                rectTransform.anchoredPosition.x,
                rectTransform.anchoredPosition.y + rectTransform.rect.height);
        }

        Destroy(mailGameObject);
    }

    /// <summary>
    /// Marks the reply as read in the <see cref="UIMailbox"/>.
    /// </summary>
    /// <param name="mailGameObject"><see cref="GameObject"/> to mark as read.</param>
    public void MarkAsRead(GameObject mailGameObject)
    {
        mailGameObject.GetComponent<RawImage>().color = Color.gray;
    }
}
