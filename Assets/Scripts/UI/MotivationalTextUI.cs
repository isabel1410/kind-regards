using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class MotivationalTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textElement;
    [SerializeField] private DataHolder dataHolder;
    public UnityEvent<DataHolder> OnMotivationalTextSelect;

    public void SetText(string text)
    {
        textElement.text = text;
    }

    public void Click()
    {
        OnMotivationalTextSelect?.Invoke(dataHolder);
    }
}
