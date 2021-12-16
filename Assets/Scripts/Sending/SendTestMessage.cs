using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTestMessage : MonoBehaviour
{
    public WebRequest Request;
    void Start()
    {
        WWWForm data = new WWWForm();
        data.AddField("text_id", 1);

        Request.Execute(new Dictionary<string, string>()
        {
            { ":id", "1" }
        }, data);
    }
}
