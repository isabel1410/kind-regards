using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public static APIManager Instance;

    [SerializeField] private WebRequest getUserDataRequest;
    [SerializeField] private WebRequest getCategoriesRequest;
    [SerializeField] private WebRequest getTextsRequest;
    [SerializeField] private WebRequest getGiftRequestsRequest;
    [SerializeField] private WebRequest getGiftMessagesRequest;
    [SerializeField] private WebRequest getStickersRequest;
    [SerializeField] private WebRequest postGiftRequest;
    [SerializeField] private WebRequest postRegisterUserRequest;
    [SerializeField] private WebRequest postMessageThankRequest;
    [SerializeField] private WebRequest postMessageSeenRequest;
    [SerializeField] private WebRequest postGiftSendRequest;

    public List<DataTextCategory> DataTextCategories;
    public List<DataText> DataTexts;
    public DataUser DataUser;
    public List<DataRequest> DataRequests;
    public List<DataMessage> DataMessages;
    public List<DataSticker> DataStickers;

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

        getGiftRequestsRequest.OnRequestFinished.AddListener(OnRequestsReceived);
        RefreshRequests();

        getStickersRequest.OnRequestFinished.AddListener(OnStickersReceived);
        getStickersRequest.Execute();
    }

    public void RefreshRequests()
    {
        getGiftRequestsRequest.Execute();
    }

    public void RefreshMessages()
    {
        getGiftMessagesRequest.Execute();
    }

    private void OnStickersReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success) throw new Exception("[API Exception] Stickers could not be retrieved.");
        DataStickers = JsonConvert.DeserializeObject<List<DataSticker>>(request.downloadHandler.text);
    }

    private void OnMessagesReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success) throw new Exception("[API Exception] Messages could not be retrieved.");
        DataMessages = JsonConvert.DeserializeObject<List<DataMessage>>(request.downloadHandler.text);
    }

    private void OnRequestsReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success) throw new Exception("[API Exception] Requests could not be retrieved.");
        DataRequests = JsonConvert.DeserializeObject<List<DataRequest>>(request.downloadHandler.text);
    }

    public void MarkMessageSeen(DataMessage dataMessage)
    {
        postMessageSeenRequest.OnRequestFinished.AddListener(OnMessageMarkedSeen);
        postMessageSeenRequest.Execute(new Dictionary<string, string>() { { ":id", dataMessage.Id.ToString() } });
    }

    private void OnMessageMarkedSeen(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success) throw new Exception("[API Exception] Requests could not be retrieved.");
        var data = JsonConvert.DeserializeObject<DataMessage>(request.downloadHandler.text);

        DataMessages[DataMessages.FindIndex(d => d.Id == data.Id)] = data;
    }

    public void SendMessage(DataRequest request, DataText text, DataCustomization customization)
    {
        WWWForm data = new WWWForm();
        data.AddField("text_id", text.Id);
        data.AddField("customization", JsonConvert.SerializeObject(customization, Formatting.None, new ColorConverter()));
        data.AddField("sticker_id", 1);
        postGiftSendRequest.Execute(new Dictionary<string, string>() { { ":id", request.Id.ToString() } }, data);
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
                return;
            }
            else throw new Exception("[API Exception] User could not be retrieved.");
        }

        DataUser = JsonConvert.DeserializeObject<DataUser>(request.downloadHandler.text);

        getGiftMessagesRequest.OnRequestFinished.AddListener(OnMessagesReceived);
        RefreshMessages();
    }

    private void OnCategoriesReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success) throw new Exception("[API Exception] Text categories could not be retrieved.");
        DataTextCategories = JsonConvert.DeserializeObject<List<DataTextCategory>>(request.downloadHandler.text);

        DataTexts.Clear();
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

        DataTexts.AddRange(newTexts);
    }

    public void SendGiftRequest(DataText requestText)
    {
        WWWForm data = new WWWForm();
        data.AddField("text_id", requestText.Id);
        postGiftRequest.Execute(data: data);
    }

    public void SendMessageThanks(DataMessage dataMessage)
    {
        postMessageThankRequest.Execute(new Dictionary<string, string>() { { ":id", dataMessage.Id.ToString() } });
    }
}
