using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundButtom : MonoBehaviour
{

    public turns turns;
    public GameObject panel;
    public GameObject buttom;
    public void ContinueGame()
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        turns = GameObject.Find("TurnSystem").GetComponent<turns>();


    }


}
