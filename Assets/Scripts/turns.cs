
using System;
using System.Globalization;
using UnityEngine.UI;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class turns : MonoBehaviour
{

	public GameObject aumZombies;
	public GameObject aum;
	public bool decoytime;
	public bool factionp;
	public bool factionz;
	public GameObject clima;
	public Text results;
	public bool isPlayerTurn;
	public GameObject playerhand;
	public GameObject enemyhand;
	public bool Round;
	public int passNum = 0;
	public GameManager gameManager;
	public Draw secondDraw;
	public Draw thirdDraw;
	public bool roundEnded = false;
	public GameObject cementery;
	public GameObject playerCaC;
	public GameObject playerAsd;
	public GameObject playerArq;
	public GameObject enemyCaC;
	public GameObject enemyAsd;
	public GameObject enemyArq;
	public bool cardPlayed;
	public DeleteCards timep;
	public DeleteCards timez;
	void Start()
	{
		timep = GameObject.Find("Button1").GetComponent<DeleteCards>();
		timez = GameObject.Find("Button2").GetComponent<DeleteCards>();
		timep.tradingTime = true;
		timez.tradingTime = true;

		Movable(playerhand, false);
		Movable(enemyhand, false);
		EndTurn();
	}

	public void EndTurn()
	{


		if (!Round)
		{
			isPlayerTurn = !isPlayerTurn;

		}

		if (isPlayerTurn)
		{
			Visibility1(playerhand, true);
			Visibility1(enemyhand, false);
			UnityEngine.Debug.Log("Turno 1");
			Checking checking = GameObject.Find("checking").GetComponent<Checking>();

			checking.CheckingEffects();
			


		}
		else if (!isPlayerTurn)
		{
			Visibility1(playerhand, false);
			Visibility1(enemyhand, true);
			cardPlayed = true;
			
			UnityEngine.Debug.Log("Turno 2");
			Checking checking = GameObject.Find("checking").GetComponent<Checking>();

			checking.CheckingEffects();
			



		}


	}

	public void Visibility1(GameObject playerhand, bool visible)
	{
		CanvasGroup cg = playerhand.GetComponent<CanvasGroup>();
		cg.alpha = visible ? 1f : 0f;
		cg.blocksRaycasts = visible;
		cg.interactable = visible;

	}

	public void Pass()
	{
		EndTurn();
		Round = !Round;
		passNum++;
		UnityEngine.Debug.Log(passNum);
		if (passNum == 3)
		{
			gameManager = GameObject.Find("gamemanager").GetComponent<GameManager>();
			gameManager.DetermineWinner();
			gameManager.EndGame();
			secondDraw = GameObject.Find("Deck").GetComponent<Draw>();
			Deletecards();

			if (gameManager.zombieWin == true)
			{
				Visibility1(playerhand, false);
				Visibility1(enemyhand, true);

				UnityEngine.Debug.Log("Turno 2");

				secondDraw.SecondRound();

			}
			else if (gameManager.plantWin == true)
			{
				Visibility1(playerhand, true);
				Visibility1(enemyhand, false);
				UnityEngine.Debug.Log("Turno 1");

				secondDraw.SecondRound();
			}
		}
		if (passNum == 5)
		{
			gameManager = GameObject.Find("gamemanager").GetComponent<GameManager>();
			gameManager.DetermineWinner();
			Deletecards();
			if (gameManager.plants == 2 && gameManager.zombies == 0 || gameManager.plants == 0 && gameManager.zombies == 2)
			{
				roundEnded = true;
				
				gameManager.EndGame();
			}

			else
			{
				roundEnded = false;
				gameManager.EndGame();
				if (gameManager.zombieWin == true)
				{
					Visibility1(playerhand, false);
					Visibility1(enemyhand, true);

					UnityEngine.Debug.Log("Turno 2");
					thirdDraw = GameObject.Find("Deck").GetComponent<Draw>();
					thirdDraw.ThirdRound();

				}
				else if (gameManager.plantWin == true)
				{
					Visibility1(playerhand, true);
					Visibility1(enemyhand, false);
					UnityEngine.Debug.Log("Turno 1");
					thirdDraw = GameObject.Find("Deck").GetComponent<Draw>();
					thirdDraw.ThirdRound();
				}

			}
		}

		if (passNum == 7)
		{
			roundEnded = true;
			
			gameManager.DetermineWinner();
			gameManager.EndGame();
		}



	}

	public void Deletecards()
	{
		CardDisplay[] cardsCaC = playerCaC.GetComponentsInChildren<CardDisplay>();
		foreach (var card in cardsCaC)
		{
			card.transform.SetParent(cementery.transform, false);

		}
		CardDisplay[] cardsArq = playerArq.GetComponentsInChildren<CardDisplay>();
		foreach (var card in cardsArq)
		{

			card.transform.SetParent(cementery.transform, false);
		}
		CardDisplay[] cardsAsd = playerAsd.GetComponentsInChildren<CardDisplay>();
		foreach (var card in cardsAsd)
		{

			card.transform.SetParent(cementery.transform, false);
		}
		CardDisplay[] cardsZombiesCaC = enemyCaC.GetComponentsInChildren<CardDisplay>();
		foreach (var card in cardsZombiesCaC)
		{

			card.transform.SetParent(cementery.transform, false);
		}
		CardDisplay[] cardsZombiesAsd = enemyAsd.GetComponentsInChildren<CardDisplay>();
		foreach (var card in cardsZombiesAsd)
		{

			card.transform.SetParent(cementery.transform, false);
		}
		CardDisplay[] cardsZombiesArq = enemyArq.GetComponentsInChildren<CardDisplay>();
		foreach (var card in cardsZombiesArq)
		{

			card.transform.SetParent(cementery.transform, false);
		}
		CardDisplay[] cardsclima = clima.GetComponentsInChildren<CardDisplay>();
		foreach (var card in cardsclima)
		{

			card.transform.SetParent(cementery.transform, false);
		}

		foreach (Transform child in aum.transform)
		{
			GameObject card = child.gameObject;
			card.transform.SetParent(cementery.transform, false);
		}
		foreach (Transform child in aumZombies.transform)
		{
			GameObject card = child.gameObject;
			card.transform.SetParent(cementery.transform, false);
		}
	}
	public void Movable(GameObject hand, bool movable)
	{
		Drag[] cards = hand.GetComponentsInChildren<Drag>();
		foreach (Drag card in cards)
		{
			card.enabled = movable;
		}
	}
}



