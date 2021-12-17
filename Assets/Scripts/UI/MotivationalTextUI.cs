using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class MotivationalTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _textElement;
    [SerializeField] private DataHolder _dataHolder;
    public UnityEvent<DataHolder> OnMotivationalTextSelect;

    public void SetText(string text)
    {
        _textElement.text = text;
    }

    public void Click()
    {
        OnMotivationalTextSelect?.Invoke(_dataHolder);
    }
}
