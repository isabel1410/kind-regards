using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.Events;

public class FillMotivationalTextsUI : MonoBehaviour
{
    public WebRequest GetMotivationalMessagesRequest;
    public GameObject MotivationalMessagesUIPrefab;
    public GameObject TargetToFill;
    public UnityEvent<DataText> OnMotivationalTextSelected;

    private void Start()
    {
        GetMotivationalMessagesRequest.Execute();
    }

    public void OnRequestFinished(UnityWebRequest request)
    {
        if (request.result != UnityWebRequest.Result.Success) return;

        List<DataText> texts = JsonConvert.DeserializeObject<List<DataText>>(request.downloadHandler.text);
        foreach(DataText text in texts)
        {
            GameObject obj = Instantiate(MotivationalMessagesUIPrefab, TargetToFill.transform);
            MotivationalTextUI ui = obj.GetComponent<MotivationalTextUI>();
            ui.SetText(text.Text);
            ui.OnMotivationalTextSelect.AddListener(OnMotivationalTextSelect);
            obj.GetComponent<DataHolder>().Data = text;
        }

        RectTransform rect = TargetToFill.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, texts.Count * MotivationalMessagesUIPrefab.GetComponent<RectTransform>().sizeDelta.y);
    }

    private void OnMotivationalTextSelect(DataHolder data)
    {
        OnMotivationalTextSelected?.Invoke(data.Data as DataText);
    }
}
