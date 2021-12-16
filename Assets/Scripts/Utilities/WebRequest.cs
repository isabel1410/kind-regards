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
        PATCH,
        DELETE
    };

    public RequestMethod Method;
    public string Uri;
    public UnityEvent<UnityWebRequest.Result> OnRequestFinished = new UnityEvent<UnityWebRequest.Result>();

    public void Execute(Dictionary<string, string> uriParams, WWWForm data = null)
    {
        string _processedUri = Uri;
        for(int i = 0; i < uriParams.Count; i++)
        {
            _processedUri.Replace(uriParams.Keys.ElementAt(i), uriParams.Values.ElementAt(i));
        }

        switch (Method)
        {
            case RequestMethod.GET:
                StartCoroutine(ExecuteCoro(UnityWebRequest.Get(_processedUri)));
                break;

            case RequestMethod.POST:
                StartCoroutine(ExecuteCoro(UnityWebRequest.Post(_processedUri, data)));
                break;
        }
    }

    private IEnumerator ExecuteCoro(UnityWebRequest request)
    {
        yield return request.SendWebRequest();
        OnRequestFinished?.Invoke(request.result);
    }
}
