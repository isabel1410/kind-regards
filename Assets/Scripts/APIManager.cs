using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public static APIManager Instance;

    [SerializeField] private UIError uiError;
    [SerializeField] private WebRequest getUserDataRequest;
    [SerializeField] private WebRequest getCategoriesRequest;
    [SerializeField] private WebRequest getTextsRequest;
    [SerializeField] private WebRequest getGiftRequestsRequest;
    [SerializeField] private WebRequest getGiftMessagesRequest;
    [SerializeField] private WebRequest getSentMessagesRequest;
    [SerializeField] private WebRequest getStickersRequest;
    [SerializeField] private WebRequest postGiftRequest;
    [SerializeField] private WebRequest postRegisterUserRequest;
    [SerializeField] private WebRequest postMessageThankRequest;
    [SerializeField] private WebRequest postMessageSeenRequest;
    [SerializeField] private WebRequest postThankedSeenRequest;
    [SerializeField] private WebRequest postGiftSendRequest;

    public List<DataTextCategory> DataTextCategories;
    public List<DataText> DataTexts;
    public DataUser DataUser;
    public List<DataRequest> DataRequests;
    public List<DataMessage> DataReceivedMessages;
    public List<DataMessage> DataSentMessages;
    public List<DataSticker> DataStickers;

    public UnityEvent OnMessagesRefreshed;
    public UnityEvent OnRequestsRefreshed;
    public UnityEvent<Exception> OnAPIError;

    public void Awake()
    {
        if (Instance && Instance != this) DestroyImmediate(gameObject);
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        OnAPIError.AddListener(ex =>
        {
            Debug.LogException(ex);
            uiError.Show("Make sure you have an internet connection");
        });

        getUserDataRequest.OnRequestFinished.AddListener(OnUserDataReceived);
        getUserDataRequest.Execute();

        getCategoriesRequest.OnRequestFinished.AddListener(OnCategoriesReceived);
        getCategoriesRequest.Execute();

        getGiftRequestsRequest.OnRequestFinished.AddListener(OnRequestsReceived);
        RefreshRequests();

        getStickersRequest.OnRequestFinished.AddListener(OnStickersReceived);
        getStickersRequest.Execute();
    }

    public void MarkThankedSeen(DataMessage message)
    {
        postThankedSeenRequest.OnRequestFinished.AddListener(OnThanksMarkedSeen);
        WWWForm form = new WWWForm();
        form.AddField("message_id", message.Id);
        postThankedSeenRequest.Execute(new Dictionary<string, string>() { { ":id", message.Thanks.Id.ToString() } }, form);
    }

    private void OnThanksMarkedSeen(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success)
        {
            OnAPIError?.Invoke(new Exception("[API Exception] Message could not be marked as read."));
            return;
        }
        var data = JsonConvert.DeserializeObject<DataThanked>(request.downloadHandler.text);
        DataSentMessages[DataSentMessages.FindIndex(d => d.Thanks != null && d.Thanks.Id == data.Id)].Thanks = data;
    }

    public void RefreshRequests()
    {
        getGiftRequestsRequest.Execute();
    }

    public void RefreshMessages()
    {
        getSentMessagesRequest.Execute();
        getGiftMessagesRequest.Execute();
    }

    private void OnStickersReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success)
        {
            OnAPIError?.Invoke(new Exception("[API Exception] Stickers could not be retrieved."));
            return;
        }
        DataStickers = JsonConvert.DeserializeObject<List<DataSticker>>(request.downloadHandler.text);
    }

    private void OnMessagesReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success)
        {
            OnMessagesRefreshed?.Invoke();
            OnAPIError?.Invoke(new Exception("[API Exception] Messages could not be retrieved."));
            return;
        }
        DataReceivedMessages = JsonConvert.DeserializeObject<List<DataMessage>>(request.downloadHandler.text);
        OnMessagesRefreshed?.Invoke();
    }

    private void OnRequestsReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success)
        {
            OnRequestsRefreshed?.Invoke();
            OnAPIError?.Invoke(new Exception("[API Exception] Requests could not be retrieved."));
            return;
        }
        DataRequests = JsonConvert.DeserializeObject<List<DataRequest>>(request.downloadHandler.text);
        OnRequestsRefreshed?.Invoke();
    }

    public void MarkMessageSeen(DataMessage dataMessage)
    {
        postMessageSeenRequest.OnRequestFinished.AddListener(OnMessageMarkedSeen);
        postMessageSeenRequest.Execute(new Dictionary<string, string>() { { ":id", dataMessage.Id.ToString() } });
    }

    private void OnMessageMarkedSeen(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success) 
        {
            OnAPIError?.Invoke(new Exception("[API Exception] Message could not be marked as read."));
            return;
        }
        var data = JsonConvert.DeserializeObject<DataMessage>(request.downloadHandler.text);
        DataReceivedMessages[DataReceivedMessages.FindIndex(d => d.Id == data.Id)] = data;
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
            else
            {
                OnAPIError?.Invoke(new Exception("[API Exception] User could not be retrieved."));
                return;
            }
        }

        DataUser = JsonConvert.DeserializeObject<DataUser>(request.downloadHandler.text);

        getGiftMessagesRequest.OnRequestFinished.AddListener(OnMessagesReceived);
        getSentMessagesRequest.OnRequestFinished.AddListener(OnSentMessagesReceived);
        RefreshMessages();
    }

    private void OnSentMessagesReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success)
        {
            OnAPIError?.Invoke(new Exception("[API Exception] Text categories could not be retrieved."));
            return;
        }

        DataSentMessages = JsonConvert.DeserializeObject<List<DataMessage>>(request.downloadHandler.text);
    }

    private void OnCategoriesReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success)
        {
            OnAPIError?.Invoke(new Exception("[API Exception] Text categories could not be retrieved."));
            return;
        }
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
        if (request.result != UnityWebRequest.Result.Success) 
        {
            OnAPIError?.Invoke(new Exception("[API Exception] Texts could not be retrieved."));
            return;
        }
        List<DataText> newTexts = JsonConvert.DeserializeObject<List<DataText>>(request.downloadHandler.text);

        DataTexts.AddRange(newTexts);
    }

    public void SendGiftRequest(DataText requestText)
    {
        WWWForm data = new WWWForm();
        data.AddField("text_id", requestText.Id);
        postGiftRequest.OnRequestFinished.AddListener(request =>
        {
            if (request.result != UnityWebRequest.Result.Success)
            {
                OnAPIError?.Invoke(new Exception("[API Exception] Request could not be created."));
                return;
            }

            DataRequests.Add(JsonConvert.DeserializeObject<DataRequest>(request.downloadHandler.text));
        });
        postGiftRequest.Execute(data: data);
    }

    public void SendMessageThanks(DataMessage dataMessage)
    {
        postMessageThankRequest.OnRequestFinished.AddListener(request =>
        {
            if (request.result != UnityWebRequest.Result.Success)
            {
                OnAPIError?.Invoke(new Exception("[API Exception] Message thanks could not be created."));
                return;
            }

            DataReceivedMessages[DataReceivedMessages.FindIndex(m => m.Id == dataMessage.Id)] = JsonConvert.DeserializeObject<DataMessage>(request.downloadHandler.text);
        });
        postMessageThankRequest.Execute(new Dictionary<string, string>() { { ":id", dataMessage.Id.ToString() } });
    }
}
