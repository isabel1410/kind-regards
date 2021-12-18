using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains functions to update the UI of the request screen.
/// </summary>
public class UIRequest : MonoBehaviour
{
    public Button RequestMessageButtonPrefab;
    public Request Request;
    public Transform RequestsContainerTransform;

    /// <summary>
    /// Creates a <see cref="Button"/> for each request message.
    /// </summary>
    /// <param name="requestMessages">Request messages to show.</param>
    public void ShowRequestMessages(string[] requestMessages)
    {
        //Set the height of the container based on the amount of messages and the height of the prefab
        RectTransform containerRectTransform = RequestsContainerTransform.GetComponent<RectTransform>();
        containerRectTransform.sizeDelta = new Vector2(
            containerRectTransform.sizeDelta.x,
            RequestMessageButtonPrefab.GetComponent<RectTransform>().rect.height * requestMessages.Length + containerRectTransform.sizeDelta.y);

        //Create buttons and add the necessary properties
        float positionY = 0;
        foreach (string requestMessage in requestMessages)
        {
            Button requestMessageButton = Instantiate(RequestMessageButtonPrefab);
            RectTransform rectTransform = requestMessageButton.gameObject.GetComponent<RectTransform>();

            requestMessageButton.transform.SetParent(RequestsContainerTransform, false);
            rectTransform.anchoredPosition = new Vector2(0, positionY);

            requestMessageButton.onClick.AddListener(delegate { Request.SendRequestMessage(requestMessage); });
            requestMessageButton.GetComponentInChildren<Text>().text = requestMessage;

            positionY -= rectTransform.rect.height;
        }
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
