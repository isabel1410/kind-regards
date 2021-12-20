using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gifting : MonoBehaviour
{
    public GameObject _selectMotivationalMessageUI;
    public GameObject _selectIncludeGiftUI;
    
    [SerializeField] private WebRequest _sendMessageRequest;

    private DataText _motivationalText;

    private void Start()
    {
        SelectReceiverStep();
    }

    public void SelectReceiverStep()
    {
        //Not implemented so continue to motivational step
        SelectMotivationalMessageStep();
    }

    public void SelectMotivationalMessageStep()
    {
        _selectMotivationalMessageUI.SetActive(true);
    }

    public void SelectIncludeGift(DataText motivationalText)
    {
        _motivationalText = motivationalText;
        _selectMotivationalMessageUI.SetActive(false);
        _selectIncludeGiftUI.SetActive(true);
    }

    public void SelectCustomizationStep()
    {
        _selectIncludeGiftUI.SetActive(false);
        // No customization step stuff yet
        Send();
    }

    public void Send()
    {
        WWWForm formData = new WWWForm();
        formData.AddField("text_id", _motivationalText.Id);

        _sendMessageRequest.Execute(new Dictionary<string, string>()
        {
            { ":id", "1" }
        }, formData);
    }
}
