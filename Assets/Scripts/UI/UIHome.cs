using UnityEngine;

public class UIHome : MonoBehaviour
{
    private GameObject CanvasCompanionActions;

    // Start is called before the first frame update
    private void Start()
    {
        CanvasCompanionActions = transform.Find("Canvas Companion Actions").gameObject;
    }

    public void ToggleCompanionActions()
    {
        bool showing = !CanvasCompanionActions.activeSelf;
        if (showing)
        {
            CanvasCompanionActions.SetActive(true);
        }
        CanvasCompanionActions.GetComponent<Animator>().Play("Fade " + (showing ? "In" : "Out") + " Companion Actions");
    }

    public void DeactivateCompanionActions()
    {
        CanvasCompanionActions.SetActive(false);
    }
}
