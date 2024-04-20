
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


	public Text score;
	public GameObject buttom;
	public turns turns;
	public GameObject panel;
	public int zombies = 0;
	public int plants = 0;
	public Text puntuacion;
	public Text winner;
	public Text plantspoints;
	public Text zombiespoints;
	public GameObject playerCaC;
	public GameObject playerArq;
	public GameObject playerAsd;
	public GameObject enemyCaC;
	public GameObject enemyArq;
	public GameObject enemyAsd;
	public GameObject deckzone;
	public GameObject playerArea;
	public GameObject enemyArea;
	public int totalPlantspoints = 0;
	public int totalZombiespoints = 0;
	public bool zombieWin;
	public bool plantWin;
	public bool tie;
	// List<GameObject> playerCaCcards = new List<GameObject>();
	// List<GameObject> cards = new List<GameObject>();
	// List<GameObject> cards = new List<GameObject>();
	// List<GameObject> cards = new List<GameObject>();
	// List<GameObject> cards = new List<GameObject>();
	// List<GameObject> cards = new List<GameObject>();
	// public void Card()
	// {
	// 	foreach (Transform zone in playerArea.transform)
	// 	{
	// 		cardPlayer += TimeZone.transform.childCount;
	// 		UnityEngine.Debug.Log(cardPlayer);
	// 	}
	// 	int CardInZone = 0;
	// 	foreach (GameObject zone in deckZone)
	// 	{
	// 		cardInZone += zone.transform.childCount;
	// 	}
	// 	if (cardDropZone == 1 && cardPlayer == 9 && cardInZone == 0)
	// 	{
	// 		foreach (Transform child in dropzone.transform)
	// 		{
	// 			GameObject card = ChildrenEnumerator.gameObject;

	// 		}
	// 	}
	// }

	void Update()
	{
		Totalplantspoints();
		Totalzombiespoints();

	}
	void Totalplantspoints()
	{
		int CaCpoints = 0;
		int Arqpoints = 0;
		int Asdpoints = 0;
		CardDisplay[] cardsCaC = playerCaC.GetComponentsInChildren<CardDisplay>();
		foreach (var card in cardsCaC)
		{
			if (cardsCaC != null)
			{
				CaCpoints += card.power;
			}
		}
		CardDisplay[] cardsArq = playerArq.GetComponentsInChildren<CardDisplay>();
		foreach (var card in cardsArq)
		{
			if (cardsArq != null)
			{ Arqpoints += card.power; }
		}
		CardDisplay[] cardsAsd = playerAsd.GetComponentsInChildren<CardDisplay>();
		foreach (var card in cardsAsd)
		{
			if (cardsAsd != null)
			{ Asdpoints += card.power; }
		}
		totalPlantspoints = CaCpoints + Arqpoints + Asdpoints;
		plantspoints.text = totalPlantspoints.ToString();

	}
	void Totalzombiespoints()
	{
		int enemyCaCpoints = 0;
		int enemyArqpoints = 0;
		int enemyAsdpoints = 0;
		CardDisplay[] enemycardsCaC = enemyCaC.GetComponentsInChildren<CardDisplay>();
		foreach (var card in enemycardsCaC)
		{
			enemyCaCpoints += card.power;
			// enemyCaCenemycards.Add(card);
		}
		CardDisplay[] enemycardsArq = enemyArq.GetComponentsInChildren<CardDisplay>();
		foreach (var card in enemycardsArq)
		{
			enemyArqpoints += card.power;
		}
		CardDisplay[] enemycardsAsd = enemyAsd.GetComponentsInChildren<CardDisplay>();
		foreach (var card in enemycardsAsd)
		{
			enemyAsdpoints += card.power;
		}
		totalZombiespoints = enemyCaCpoints + enemyArqpoints + enemyAsdpoints;
		zombiespoints.text = totalZombiespoints.ToString();

	}
	public void DetermineWinner()
	{
		int a = totalPlantspoints;
		int b = totalZombiespoints;
		if (a > b)
		{
			plantWin = true;
			UnityEngine.Debug.Log("Ganaron las plantas");
			winner.text = "LAS PLANTAS HAN GANADO";
			plants++;
		}
		else if (b > a || b == a)
		{
			zombieWin = true;
			UnityEngine.Debug.Log("Ganaron los zombies");
			winner.text = "LOS ZOMBIES HAN GANADO";
			zombies++;
		}


		score.text = plants + "/" + zombies;
	}
	public void EndGame()
	{
		CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1;
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;

		CanvasGroup canvasbuttom = buttom.GetComponent<CanvasGroup>();
		canvasbuttom.alpha = 1;
		canvasbuttom.interactable = true;
		canvasbuttom.blocksRaycasts = true;
		int a = totalPlantspoints;
		int b = totalZombiespoints;
		turns = GameObject.Find("TurnSystem").GetComponent<turns>();
		if (turns.roundEnded == true)
		{
			canvasbuttom.alpha = 0;
			canvasbuttom.interactable = false;
			canvasbuttom.blocksRaycasts = false;
			UnityEngine.Debug.Log("ocurre el if");
			if (plants > zombies)
			{
				puntuacion.text = "VICTORIA DE LAS PLANTAS";
			}
			else
			{
				puntuacion.text = "VICTORIA DE LOS ZOMBIES";
			}

		}
		else
		{
			if (a > b)
			{

				plantWin = true;
				zombieWin = false;
				puntuacion.text = "VICTORIA DE LAS PLANTAS";
			}
			else if (b > a || b == a)
			{
				plantWin = false;
				zombieWin = true;
				puntuacion.text = "VICTORIA DE LOS ZOMBIES";
			}


			if (turns.roundEnded == true)
			{

				canvasbuttom.alpha = 0;
				canvasbuttom.interactable = false;
				canvasbuttom.blocksRaycasts = false;
				UnityEngine.Debug.Log("ocurre el if");
			}
		}

	}
}