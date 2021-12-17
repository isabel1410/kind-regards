using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gifting : MonoBehaviour
{
    [SerializeField] private GameObject _selectMotivationalMessageUI;
    [SerializeField] private WebRequest _sendMessageRequest;
    private MotivationalText _motivationalText;

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

    public void SelectCustomizationStep(MotivationalText motivationalText)
    {
        _selectMotivationalMessageUI.SetActive(false);
        _motivationalText = motivationalText;
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
