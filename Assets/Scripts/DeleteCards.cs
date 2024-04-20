using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCards : MonoBehaviour
{
	public bool tradingTime;
	public GameObject hand;
	public GameObject button;
	public int counter;
	public void OnClick()
	{
		turns turn = GameObject.Find("TurnSystem").GetComponent<turns>();
		if (tradingTime == true)
		{
			UnityEngine.Debug.Log("entro al boton");
			turn.Movable(hand, true);
			Hide();
			tradingTime = false;
		}
	}
	public void Hide()
	{
		CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
	}
	public void Show()
	{
		CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1;
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;
	}

}
