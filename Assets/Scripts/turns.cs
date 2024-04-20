
using System;
using System.Globalization;
using UnityEngine.UI;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class turns : MonoBehaviour
{

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

		}
		else if (!isPlayerTurn)
		{
			Visibility1(playerhand, false);
			Visibility1(enemyhand, true);
			cardPlayed = true;
			UnityEngine.Debug.Log("se convierte en true");
			UnityEngine.Debug.Log("Turno 2");

		}
		timep = GameObject.Find("Button1").GetComponent<DeleteCards>();
		timez = GameObject.Find("Button2").GetComponent<DeleteCards>();
		timep.tradingTime = true;
		timez.tradingTime = true;
		timez.Hide();
		Movable(playerhand, false);
		Movable(enemyhand, false);

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
				UnityEngine.Debug.Log("aaaa esta pasando");
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
					secondDraw = GameObject.Find("Deck").GetComponent<Draw>();
					thirdDraw.ThirdRound();
				}

			}
		}

		if (passNum == 7)
		{
			roundEnded = true;
			UnityEngine.Debug.Log("se cambia a true num=7");
			gameManager.DetermineWinner();
			gameManager.EndGame();
		}



	}

	public void Deletecards()
	{
		foreach (Transform child in playerCaC.transform)
		{
			GameObject card = child.gameObject;
			card.transform.SetParent(cementery.transform, false);
		}
		foreach (Transform child in playerArq.transform)
		{
			GameObject card = child.gameObject;
			card.transform.SetParent(cementery.transform, false);
		}
		foreach (Transform child in playerAsd.transform)
		{
			GameObject card = child.gameObject;
			card.transform.SetParent(cementery.transform, false);
		}
		foreach (Transform child in enemyArq.transform)
		{
			GameObject card = child.gameObject;
			card.transform.SetParent(cementery.transform, false);
		}
		foreach (Transform child in enemyAsd.transform)
		{
			GameObject card = child.gameObject;
			card.transform.SetParent(cementery.transform, false);
		}
		foreach (Transform child in enemyCaC.transform)
		{
			GameObject card = child.gameObject;
			card.transform.SetParent(cementery.transform, false);
		}
		foreach (Transform child in clima.transform)
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



