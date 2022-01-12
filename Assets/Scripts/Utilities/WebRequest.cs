using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
    public enum RequestMethod
    {
        GET,
        POST,
        PUT,
        DELETE
    };

    public RequestMethod Method;
    public string Uri;
    public UnityEvent<UnityWebRequest> OnRequestFinished = new UnityEvent<UnityWebRequest>();
    public bool IncludeDeviceUniqueIDAsAuthToken;
    public string AuthToken;

    /// <summary>
    /// Execute a webrequest
    /// </summary>
    /// <param name="uriParams">The URL parameters replacer (Replace :id with 1)</param>
    /// <param name="data">The Form data to be provided with a POST request</param>
    /// <param name="bodyData">THe body data required for a PUT request</param>
    public void Execute(Dictionary<string, string> uriParams = null, WWWForm data = null, string bodyData = null)
    {
        // Setup the processed url to be the same as the default URL
        string _processedUri = Uri;

        if(uriParams != null)
        {
            // Go through all the given url parameters provided in the dictionary and replace them with the value.
            for (int i = 0; i < uriParams.Count; i++)
            {
                _processedUri = _processedUri.Replace(uriParams.Keys.ElementAt(i), uriParams.Values.ElementAt(i));
            }
        }       

        // Switch through the methods.
        switch (Method)
        {
            case RequestMethod.GET:
                // Send a GET request to the processed url. 
                StartCoroutine(ExecuteCoro(UnityWebRequest.Get(_processedUri)));
                break;

            case RequestMethod.POST:
                // Send a POST request to the processed url. 
                StartCoroutine(ExecuteCoro(UnityWebRequest.Post(_processedUri, data)));
                break;

            case RequestMethod.PUT:
                // Send a PUT request to the processed url. 
                StartCoroutine(ExecuteCoro(UnityWebRequest.Put(_processedUri, bodyData)));
                break;

            case RequestMethod.DELETE:
                // Send a DELETE request to the processed url. 
                StartCoroutine(ExecuteCoro(UnityWebRequest.Delete(_processedUri)));
                break;
        }
    }

    /// <summary>
    /// The execute coroutine for executing a webrequest.
    /// </summary>
    /// <param name="request">The specified request to be executed</param>
    /// <returns>A coroutine</returns>
    private IEnumerator ExecuteCoro(UnityWebRequest request)
    {
        // If needed include the SystemInfo.deviceUniqueIdentifier as Authorization token in webrequest.
        if (IncludeDeviceUniqueIDAsAuthToken) request.SetRequestHeader("Authorization", "Bearer " + SystemInfo.deviceUniqueIdentifier);

        // If AuthToken is set use this as Authorization token in webrequest.
        if (!string.IsNullOrEmpty(AuthToken)) request.SetRequestHeader("Authorization", "Bearer " + AuthToken);

        // Send the webrequest.
        yield return request.SendWebRequest();

        // If the request is finished call the event.
        OnRequestFinished?.Invoke(request);
    }
}
