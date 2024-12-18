using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeControlsMenu : MonoBehaviour
{
    private int pages = 0;

    public GameObject page0;
    public GameObject page1;
    public GameObject page2;


    public void ButtonLeft()
    {
        if (pages == 2)
        {
            pages = 1;
            page2.SetActive(false);
            page1.SetActive(true);

        }
        else if (pages == 1)
        {
            pages = 0;
            page1.SetActive(false);
            page0.SetActive(true);
        }
        else
        {
            pages = 2;
            page2.SetActive(true);
            page0.SetActive(false);
        }
    }

    public void ButtonRight()
    {
        if (pages == 0)
        {
            pages = 1;
            page1.SetActive(true);
            page0.SetActive(false);

        }
        else if (pages == 1)
        {
            pages = 2;
            page2.SetActive(true);
            page1.SetActive(false);
        }
        else
        {
            pages = 0;
            page0.SetActive(true);
            page2.SetActive(false);
        }
    }
}
