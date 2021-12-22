using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISticker : MonoBehaviour
{
    [SerializeField]
    private Button UIPrevious;
    [SerializeField]
    private Button UINext;

    public void Default(List<GameObject> pages, int currentPage)
    {
        if (currentPage == 0 && pages.Count - 1 == 0)
        {
            UIPrevious.gameObject.SetActive(false);
            UINext.gameObject.SetActive(false);
        }
        else if (currentPage == 0)
        {
            UIPrevious.gameObject.SetActive(false);
            UINext.gameObject.SetActive(true);
        }
        else if (currentPage == pages.Count - 1)
        {
            UIPrevious.gameObject.SetActive(true);
            UINext.gameObject.SetActive(false);
        }
        else
        {
            UIPrevious.gameObject.SetActive(true);
            UINext.gameObject.SetActive(true);
        }
    }

    public void ShowPage(List<GameObject> pages, int currentPage)
    {
        for (int i = 0; i < pages.Count; i++)
        {
            if (i == currentPage)
            {
                pages[currentPage].gameObject.SetActive(true);
            }
            else
            {
                pages[i].gameObject.SetActive(false);
            }
        }
    }
}
