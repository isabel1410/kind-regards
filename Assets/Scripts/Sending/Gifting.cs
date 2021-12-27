using System.Collections.Generic;
using UnityEngine;

public class Gifting : MonoBehaviour
{
    public GameObject selectMotivationalMessageUI;
    public GameObject selectIncludeGiftUI;
    
    [SerializeField] private WebRequest sendMessageRequest;

    private DataText motivationalText;

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
       selectMotivationalMessageUI.SetActive(true);
    }

    public void SelectIncludeGift(DataText motivationalText)
    {
        this.motivationalText = motivationalText;
        selectMotivationalMessageUI.SetActive(false);
        selectIncludeGiftUI.SetActive(true);
    }

    public void SelectCustomizationStep()
    {
        selectIncludeGiftUI.SetActive(false);
        // No customization step stuff yet
        Send();
    }

    public void Send()
    {
        WWWForm formData = new WWWForm();
        formData.AddField("text_id", motivationalText.Id);

        sendMessageRequest.Execute(new Dictionary<string, string>()
        {
            { ":id", "1" }
        }, formData);
    }
}
