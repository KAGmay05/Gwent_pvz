using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnsystem : MonoBehaviour
{
	public bool isYourTurn;
	public int yourTurn;
	public int yourEnemyTurn;
	public Text turnText;

	public int maxMana;
	public int currentMana;
	public Text manaText;
	void Start()
	{
		isYourTurn = true;
		yourTurn = 1;
		yourEnemyTurn = 0;

		maxMana = 0;
		currentMana = 0;
	}


	void Update()
	{
		if (isYourTurn == true)
		{
			turnText.text = "Your Turn";

		}
		else
		{
			turnText.text = "Enemy turn";
		}
		manaText.text = currentMana + "/" + maxMana;
	}
	public void EndYourTurn()
	{
		isYourTurn = false;
		yourEnemyTurn += 1;
	}
	public void EndYourEnemyTurn()
	{
		isYourTurn = true;
		yourTurn += 1;
		maxMana += 1;
		currentMana = maxMana;
	}
}
