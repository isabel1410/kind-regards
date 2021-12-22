using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains functions to update the UI of the request screen.
/// </summary>
public class UIRequest : MonoBehaviour
{
    [SerializeField]
    private Button RequestMessageButtonPrefab;
    [SerializeField]
    private Request Request;
    [SerializeField]
    private Transform RequestsContainerTransform;

    private float height;

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    private void Start()
    {
        height = RequestsContainerTransform.GetComponentInParent<RectTransform>().rect.height;
    }

    /// <summary>
    /// Creates a <see cref="Button"/> for each request message.
    /// </summary>
    /// <param name="requestMessages">Request messages to show.</param>
    public void ShowRequestMessages(DataText[] requestMessages)
    {
        //Create buttons and add the necessary properties
        float positionY = 0;
        foreach (DataText requestMessage in requestMessages)
        {
            Button requestMessageButton = Instantiate(RequestMessageButtonPrefab);
            RectTransform rectTransform = requestMessageButton.gameObject.GetComponent<RectTransform>();

            requestMessageButton.transform.SetParent(RequestsContainerTransform, false);
            rectTransform.anchoredPosition = new Vector2(0, positionY);

            requestMessageButton.onClick.AddListener(() => Request.SendRequestMessage(requestMessage));
            requestMessageButton.GetComponentInChildren<Text>().text = requestMessage.Text;

            positionY -= rectTransform.rect.height;
        }

        //Set the height of the container based on the amount of messages and the height of the prefab
        RectTransform containerRectTransform = RequestsContainerTransform.GetComponent<RectTransform>();
        containerRectTransform.sizeDelta = new Vector2(
            containerRectTransform.sizeDelta.x,
            Mathf.Abs(positionY) - height);
    }

    /// <summary>
    /// Destroys all the buttons containing the request messages.
    /// </summary>
    public void DestroyRequestButtons()
    {
        foreach (Transform buttonTransform in RequestsContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }
    }
}
