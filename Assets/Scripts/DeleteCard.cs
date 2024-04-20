using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCard : MonoBehaviour
{


	public GameObject card;
	public GameObject cementery;
	public int click;
	public bool canDraw;
	public bool CanDrawAgain;
	public void Delete()
	{
		if (click != 3)
		{
			card.transform.SetParent(cementery.transform, false);
			canDraw = true;
			click++;

		}
		if (click == 2)
		{
			CanDrawAgain = true;
			canDraw = false;
		}
		if (click == 3)
		{
			turns turns = GameObject.Find("TurnSystem").GetComponent<turns>();
			turns.cardPlayed = true;
		}

	}

}
