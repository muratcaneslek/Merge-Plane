using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject[] gamePanels;
    public void ShopButton()
    {
        gamePanels[0].SetActive(false);
        gamePanels[1].SetActive(true);
    }

    public void Shopback()
    {
        gamePanels[1].SetActive(false);
        gamePanels[0].SetActive(true);
    }
}
