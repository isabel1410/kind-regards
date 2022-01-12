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

    /// <summary>
    /// Make the API manager a singleton.
    /// </summary>
    public void Awake()
    {
        if (Instance && Instance != this) DestroyImmediate(gameObject);
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    /// <summary>
    /// Listen to API events and execute the default requests that don't need a user.
    /// </summary>
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

    /// <summary>
    /// Mark a received thank you message as seen
    /// </summary>
    /// <param name="message">The message that includes a thanks</param>
    public void MarkThankedSeen(DataMessage message)
    {
        postThankedSeenRequest.OnRequestFinished.AddListener(OnThanksMarkedSeen);
        WWWForm form = new WWWForm();
        form.AddField("message_id", message.Id);
        postThankedSeenRequest.Execute(new Dictionary<string, string>() { { ":id", message.Thanks.Id.ToString() } }, form);
    }

    /// <summary>
    /// The response of the MarkThankedSeen request. It updates the original message with new data.
    /// </summary>
    /// <param name="request">The request response data</param>
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

    /// <summary>
    /// Refreshes the user requests.
    /// </summary>
    public void RefreshRequests()
    {
        getGiftRequestsRequest.Execute();
    }

    /// <summary>
    /// Refreshes the user messages.
    /// </summary>
    public void RefreshMessages()
    {
        getSentMessagesRequest.Execute();
        getGiftMessagesRequest.Execute();
    }

    /// <summary>
    /// The response of the getStickersRequest. It will load all the stickers from the database.
    /// </summary>
    /// <param name="request">The request response data</param>
    private void OnStickersReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success)
        {
            OnAPIError?.Invoke(new Exception("[API Exception] Stickers could not be retrieved."));
            return;
        }
        DataStickers = JsonConvert.DeserializeObject<List<DataSticker>>(request.downloadHandler.text);
    }

    /// <summary>
    /// The response of the getGiftMessagesRequest. It will load all the messages from the database.
    /// </summary>
    /// <param name="request">The request response data</param>
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

    /// <summary>
    /// The response of the getGiftRequestsRequest. It will load all the gift requests from the database.
    /// </summary>
    /// <param name="request">The request response data</param>
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

    /// <summary>
    /// Sends the request to mark a received messages as seen.
    /// </summary>
    /// <param name="dataMessage">The message you are marking as seen</param>
    public void MarkMessageSeen(DataMessage dataMessage)
    {
        postMessageSeenRequest.OnRequestFinished.AddListener(OnMessageMarkedSeen);
        postMessageSeenRequest.Execute(new Dictionary<string, string>() { { ":id", dataMessage.Id.ToString() } });
    }

    /// <summary>
    /// The response of the postMessageSeenRequest. It will update the received message with the new data.
    /// </summary>
    /// <param name="request">The request response data</param>
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

    /// <summary>
    /// Send a message to an user.
    /// </summary>
    /// <param name="request">The request you are replying to</param>
    /// <param name="text">The text you chose to reply with</param>
    /// <param name="customization">The customization options for the gift</param>
    public void SendMessage(DataRequest request, DataText text, DataCustomization customization)
    {
        WWWForm data = new WWWForm();
        data.AddField("text_id", text.Id);
        data.AddField("customization", JsonConvert.SerializeObject(customization, Formatting.None, new ColorConverter()));
        data.AddField("sticker_id", 1);
        postGiftSendRequest.Execute(new Dictionary<string, string>() { { ":id", request.Id.ToString() } }, data);
    }

    /// <summary>
    /// The response of the getUserDataRequest and postRegisterUserRequest. It will try and load the user from the database. If no user exists it will try and register one.
    /// </summary>
    /// <param name="request">The request response data</param>
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

    /// <summary>
    /// The response of the getSentMessagesRequest. It will load all the sent messages by the user.
    /// </summary>
    /// <param name="request">The request response data</param>
    private void OnSentMessagesReceived(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success)
        {
            OnAPIError?.Invoke(new Exception("[API Exception] Text categories could not be retrieved."));
            return;
        }

        DataSentMessages = JsonConvert.DeserializeObject<List<DataMessage>>(request.downloadHandler.text);
    }

    /// <summary>
    /// The response of the getCategoriesRequest. It will load all the text categories of the app. After loading them it will load all the text for each category.
    /// </summary>
    /// <param name="request">The request response data</param>
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

    /// <summary>
    /// The response of the getTextsRequest. It will load all the texts by category of the app.
    /// </summary>
    /// <param name="request">The request response data</param>
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

    /// <summary>
    /// Send out a request to receive messages/gifts.
    /// </summary>
    /// <param name="requestText">The text you chose for the request</param>
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

    /// <summary>
    /// Send out a thanks to the sender of your received message.
    /// </summary>
    /// <param name="dataMessage">The message you want to thank</param>
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
