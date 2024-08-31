
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject graveyard;
	public Draw draw;
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
	public GameObject playerAum;
	public GameObject zombiesAum;
	public GameObject playerCaC;
	public GameObject playerArq;
	public GameObject playerAsd;
	public GameObject enemyCaC;
	public GameObject enemyArq;
	public GameObject enemyAsd;
	public GameObject playerArea;
	public GameObject enemyArea;
	public int totalPlantspoints = 0;
	public int totalZombiespoints = 0;
	public bool zombieWin;
	public bool plantWin;
	public bool tie;
    public GameObject weatherZone;

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
			{
				Asdpoints += card.power;

			}
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
	
	public int TriggerPlayer()
	{
     bool playerturn = GameObject.Find("TurnSystem").GetComponent<turns>().isPlayerTurn;
	 if(playerturn) return 1;
	 else return 0;
	}
	public List<Card> CardsOnBoard ()
	{
     List<Card> cardsOnBoard = new List<Card>();
	 foreach(Transform child in playerAsd.transform)
	 {
      CardDisplay card = child.GetComponent<CardDisplay>();
	  cardsOnBoard.Add(card.card);
	 }
	 foreach(Transform child in playerCaC.transform) 
	 {
		CardDisplay card = child.GetComponent<CardDisplay>();
		cardsOnBoard.Add(card.card);
	 }
	 foreach(Transform child in enemyArq.transform) 
	 {
		CardDisplay card = child.GetComponent<CardDisplay>();
		cardsOnBoard.Add(card.card);
	 }
	 foreach(Transform child in enemyAsd.transform) 
	 {
		CardDisplay card = child.GetComponent<CardDisplay>();
		cardsOnBoard.Add(card.card);
	 }
	 foreach(Transform child in enemyCaC.transform) 
	 {
		CardDisplay card = child.GetComponent<CardDisplay>();
		cardsOnBoard.Add(card.card);
	 }
	 foreach(Transform child in weatherZone.transform)
	 {
		CardDisplay card = child.GetComponent<CardDisplay>();
		cardsOnBoard.Add(card.card);
	 } 
	 foreach(Transform child in playerAum.transform)
	 {
		CardDisplay card = child.GetComponent<CardDisplay>();
		cardsOnBoard.Add(card.card);
	 } 
	 foreach(Transform child in zombiesAum.transform)
	 {
		CardDisplay card = child.GetComponent<CardDisplay>();
		cardsOnBoard.Add(card.card);
	 } 
	 foreach(Transform child in playerArq.transform)
	 {
		CardDisplay card = child.GetComponent<CardDisplay>();
		cardsOnBoard.Add(card.card);
	 }
	 return cardsOnBoard;
	}
	public List<Card> HandOfPlayer(int player)
	{
		List<Card> playerCards = new List<Card>();
		List<Card> enemyCards = new List<Card>();
		if(player == 1)
		{
			foreach(Transform child in playerArea.transform)
	         {
	            CardDisplay card = child.GetComponent<CardDisplay>();
		        playerCards.Add(card.card);
	         }  
			return playerCards;
		}
		else
		{
         foreach(Transform child in enemyArea.transform)
	      {
            CardDisplay card = child.GetComponent<CardDisplay>();
            enemyCards.Add(card.card);
	      } 
		 return enemyCards;
		}
	}
	public List<Card> FieldOfPlayer( int player)
	{       List<Card> cardsP = new List<Card>();    
	        List<Card> cardsZ = new List<Card>();
		    Card[] cardsAsd = playerAsd.GetComponentsInChildren<Card>();
	        Card[] cardsCaC = playerCaC.GetComponentsInChildren<Card>();
	        Card[] cardsArq = playerArq.GetComponentsInChildren<Card>();
			Card[] cardsZombiesAsd = enemyAsd.GetComponentsInChildren<Card>();
	        Card[] cardsZombiesCaC = enemyCaC.GetComponentsInChildren<Card>();
	        Card[] cardsZombiesArq = enemyArq.GetComponentsInChildren<Card>();
		
		if(player == 1)
		{
			foreach(Transform child in playerAsd.transform)
	         {
               CardDisplay card = child.GetComponent<CardDisplay>();
	           cardsP.Add(card.card);
	         }
	        foreach(Transform child in playerCaC.transform) 
	        {
              CardDisplay card = child.GetComponent<CardDisplay>();
              cardsP.Add(card.card);
	        }
	       foreach(Transform child in playerArq.transform)
	        {
              CardDisplay card = child.GetComponent<CardDisplay>();
              cardsP.Add(card.card);
	        }
			foreach(var card in cardsP) Debug.Log(card);
			return cardsP;
		}
		else
		{
			foreach(Transform child in enemyArq.transform) 
	 {
		CardDisplay card = child.GetComponent<CardDisplay>();
		cardsZ.Add(card.card);
	 }
	 foreach(Transform child in enemyAsd.transform) 
	 {
		CardDisplay card = child.GetComponent<CardDisplay>();
		cardsZ.Add(card.card);
	 }
	 foreach(Transform child in enemyCaC.transform) 
	 {
		CardDisplay card = child.GetComponent<CardDisplay>();
		cardsZ.Add(card.card);
	 }
			foreach(var card in cardsZ) Debug.Log(card);
			return cardsZ;
		}
	}
	public List<Card> GraveyardOfPlayer(int player)
	{
		List<Card> cardsP = new List<Card>();
		List<Card> cardsZ = new List<Card>();
		if(player == 1)
		{
			foreach(Transform child in graveyard.transform)
			{
				CardDisplay card = child.GetComponent<CardDisplay>();
				if(card.faction == "plantas")
				{
					cardsP.Add(card.card);
				}
			}
			return cardsP;
		}
		else
		{
			foreach(Transform child in graveyard.transform)
			{
				CardDisplay card = child.GetComponent<CardDisplay>();
				if(card.faction == "zombies")
				{
					cardsZ.Add(card.card);
				}
			}
			return cardsZ;
		}
	}
	public List<Card> DeckofPlayer(int player)
	{
		List<Card> cardsP = new List<Card>();
		List<Card> cardsZ = new List<Card>();
		if(player == 1)
		{
			foreach(var obj in draw.cards)
		{
			CardDisplay cardDisplay = obj.GetComponent<CardDisplay>();
			if (cardDisplay != null)
			{
				cardsP.Add(cardDisplay.card);
			}
        
		 }
		
		return cardsP;
		}
		else
		{
		foreach(var obj in draw.enemycards)
		{
           CardDisplay cardDisplay = obj.GetComponent<CardDisplay>();
		   if (cardDisplay != null) cardsZ.Add(cardDisplay.card);
		}
		return cardsZ;
		}
		
	}
}