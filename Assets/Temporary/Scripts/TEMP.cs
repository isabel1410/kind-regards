using UnityEngine;

public class TEMP : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<UnityEngine.UI.RawImage>().color = Color.green;
    }

    public string[] GetRequestMessages()
    {
        System.Collections.Generic.List<string> requestMessages = new System.Collections.Generic.List<string>();
        for (byte counter = 1; counter <= 50; counter++)
        {
            requestMessages.Add("Request message " + counter);
        }
        return requestMessages.ToArray();
    }
}
