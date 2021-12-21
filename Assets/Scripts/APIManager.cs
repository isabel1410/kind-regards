using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public static APIManager Instance;

    [SerializeField] private WebRequest getUserDataRequest;
    [SerializeField] private WebRequest getCategoriesRequest;
    [SerializeField] private WebRequest getTextsRequest;
    [SerializeField] private WebRequest postGiftRequest;
    [SerializeField] private WebRequest postRegisterUserRequest;
    [SerializeField] private WebRequest postMessageThankRequest;

    public List<DataTextCategory> DataTextCategories;
    public List<DataText> DataTexts;
    public DataUser DataUser;

    public void Awake()
    {
        if (Instance && Instance != this) DestroyImmediate(gameObject);
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        getUserDataRequest.OnRequestFinished.AddListener(OnUserDataReceived);
        getUserDataRequest.Execute();

        getCategoriesRequest.OnRequestFinished.AddListener(OnCategoriesReceived);
        getCategoriesRequest.Execute();
    }

    private void OnUserDataReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success)
        {
            if (request.responseCode == 404)
            {
                postRegisterUserRequest.OnRequestFinished.AddListener(OnUserDataReceived);

                WWWForm data = new WWWForm();
                data.AddField("device_id", SystemInfo.deviceUniqueIdentifier);
                postRegisterUserRequest.Execute(data: data);
            }
            else throw new Exception("[API Exception] User could not be retrieved.");
        }

        DataUser = JsonConvert.DeserializeObject<DataUser>(request.downloadHandler.text);
    }

    private void OnCategoriesReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success) throw new Exception("[API Exception] Text categories could not be retrieved.");
        DataTextCategories = JsonConvert.DeserializeObject<List<DataTextCategory>>(request.downloadHandler.text);

        getTextsRequest.OnRequestFinished.AddListener(OnTextsReceived);
        foreach(DataTextCategory category in DataTextCategories)
        {
            getTextsRequest.Execute(new Dictionary<string, string>()
            {
                { ":id", category.Id.ToString() }
            });
        }
    }

    private void OnTextsReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success) throw new Exception("[API Exception] Texts could not be retrieved.");
        List<DataText> newTexts = JsonConvert.DeserializeObject<List<DataText>>(request.downloadHandler.text);

        for(int i = 0; i < newTexts.Count; i++)
        {   
            newTexts[i].Category = DataTextCategories.Find(cat => cat.Id == newTexts[i].CategoryId);
        }

        DataTexts.AddRange(newTexts);
    }

    public void SendGiftRequest(DataText requestText)
    {
        WWWForm data = new WWWForm();
        data.AddField("text_id", requestText.Id);
        postGiftRequest.Execute(data: data);
    }

    public void SendMessageThanks(DataMail dataMail)
    {
        postMessageThankRequest.Execute(new Dictionary<string, string>() { { ":id", dataMail.Id.ToString() } });
    }
}
