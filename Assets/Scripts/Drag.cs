using System.Globalization;
using System;
using System.Diagnostics;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public bool cardPlayed;
	public int cardspCaC = 0;
	public int cardspArq = 0;
	public int cardspAsd = 0;
	public int cardszCaC = 0;
	public int cardszArq = 0;
	public int cardszAsd = 0;
	public bool aum;
	public bool weatherCaC;
	public bool weatherArq;
	public bool weatherAsd;
	public Effects efectos;
	public bool CanDrawZombies;
	public bool CanDrawPlants;
	public bool onField = false;

	public bool isOverDropZone;

	GameObject playerDropZone;

	public GameObject canvas;
	public turns endturn;
	// public Effects aumento;

	public GameObject aumCaC;
	public Vector2 startPosition;
	public GameObject startParent;
	GameObject hand;
	public string a = "aumento";
	public string r = "robar";
	public string deleteMax = "deleteMax";
	public string deleteMin = "deleteMin";
	public string climaCaC = "climaCaC";
	public string climaArq = "climaArq";
	public string climaAsd = "climaAsd";
	public string despeje = "despeje";
	public string robarZombies = "robarZombies";
	public string robarPlantas = "robarPlants";
	public string multiply = "multiply";
	public string decoy = "señuelo";


	void Start()
	{
		canvas = GameObject.Find("Canvas");
		hand = gameObject.transform.parent.gameObject;
		playerDropZone = hand;

	}


	public void OnBeginDrag(PointerEventData eventData)
	{

		startPosition = transform.position;
		startParent = transform.parent.gameObject;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!onField)
		{
			this.transform.position = eventData.position;
			transform.SetParent(canvas.transform);
		}

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

		isOverDropZone = true;
		playerDropZone = collision.gameObject;
	}
	private void OnCollisionExit2D(Collision2D collision)
	{

		isOverDropZone = false;
	}
	public void OnEndDrag(PointerEventData eventData)
	{

		if (isOverDropZone && Correctzone() && !onField)
		{

			transform.SetParent(playerDropZone.transform, false);
			onField = true;
			UnityEngine.Debug.Log("on field");
			CardDisplay cardDisplay = gameObject.GetComponent<CardDisplay>();
			efectos = GameObject.Find("efectos").GetComponent<Effects>();
			string effect = cardDisplay.efecto;
			string faction = cardDisplay.faction;
			Draw draw = GameObject.Find("Deck").GetComponent<Draw>();
			endturn = GameObject.Find("TurnSystem").GetComponent<turns>();


			// checking.CheckingEffects();
			// UnityEngine.Debug.Log("esta comprobando");

			if (effect == deleteMax)
			{
				UnityEngine.Debug.Log("efecto atleta");
				efectos.DeleteMaxPlantcard();

			}
			else if (effect == deleteMin)
			{
				efectos.DeleteMinZombiecard();
			}
			else if (effect == climaCaC || weatherCaC == true)
			{
				weatherCaC = true;
				efectos.WeatherCaC();

			}
			else if (effect == climaArq || weatherArq == true)
			{
				weatherArq = true;

				efectos.WeatherArq();

			}
			else if (effect == climaAsd || weatherAsd == true)
			{
				weatherAsd = true;
				efectos.WeatherAsd();

			}
			else if (effect == a && cardDisplay.faction == "plantas")
			{
				UnityEngine.Debug.Log("aumento plantas");
				aum = true;
				efectos.Aumento();


			}
			else if (effect == a && cardDisplay.faction == "zombies")
			{
				UnityEngine.Debug.Log("aumento zombies");
				aum = true;
				efectos.AumentoZombies();

			}


			else if (effect == despeje)
			{
				efectos.Despeje();
				weatherArq = false;
				weatherAsd = false;
				weatherCaC = false;
			}
			else if (effect == robarPlantas)
			{
				efectos.RobarPlants();
				UnityEngine.Debug.Log("robar plantas");
			}
			else if (effect == robarZombies)
			{
				efectos.RobarZombies();
				UnityEngine.Debug.Log("robar zombies");
			}
			else if (effect == multiply)
			{
				UnityEngine.Debug.Log("multiplico carta");
				efectos.Multiply();

			}
			else if (effect == "promedio")
			{
				UnityEngine.Debug.Log("promedio");
				efectos.Promedio();
			}


			turns señuelo = GameObject.Find("TurnSystem").GetComponent<turns>();
			if (effect == "señuelo")
			{
				UnityEngine.Debug.Log("efecto decoy");

				señuelo.decoytime = true;
				if (cardDisplay.GetComponent<CardDisplay>().faction == "plantas")
				{
					señuelo.factionp = true;
					UnityEngine.Debug.Log("entro a faction == planras");
				}
				else
				{
					señuelo.factionz = true;
					UnityEngine.Debug.Log("entro a faction == zombies");

				}
			}

			if (señuelo.decoytime == false)
			{
				endturn.EndTurn();
			}






		}
		else
		{

			transform.position = startPosition;
			transform.SetParent(startParent.transform, false);

		}
	}

	public bool Correctzone()
	{
		Zones conditions = playerDropZone.GetComponent<Zones>();
		string p = conditions.zoneNames;
		string l = gameObject.GetComponent<CardDisplay>().zone;
		string a = gameObject.GetComponent<CardDisplay>().zoneaux;
		string w = gameObject.GetComponent<CardDisplay>().zonextra;

		if (p == l || p == a || p == w)
		{
			return true;
		}
		else
		{
			return false;
		}

	}
	public void OnClick()
	{
		CardDisplay cardDisplay = GetComponent<CardDisplay>();
		turns señuelo = GameObject.Find("TurnSystem").GetComponent<turns>();
		if (señuelo.decoytime && señuelo.factionp == true)
		{

			UnityEngine.Debug.Log("entro al primer if del decoy");
			if (cardDisplay.faction == "plantas" && cardDisplay.tipo != "oro")
			{

				UnityEngine.Debug.Log("entro al segundo if del decoy");
				GameObject zone1 = GameObject.Find("PlayerArea");
				cardDisplay.power = cardDisplay.ogpower;
				transform.position = zone1.transform.position;
				transform.SetParent(zone1.transform, false);
				señuelo.decoytime = false;
				onField = false;
				señuelo.EndTurn();
				UnityEngine.Debug.Log("esta saliendo del decoy");
			}
		}
		else if (señuelo.decoytime && señuelo.factionz)
		{
			if (cardDisplay.faction == "zombies" && cardDisplay.tipo != "oro")
			{
				GameObject zone1 = GameObject.Find("EnemyArea");
				cardDisplay.power = cardDisplay.ogpower;
				transform.position = zone1.transform.position;
				transform.SetParent(zone1.transform, false);
				señuelo.decoytime = false;
				onField = false;
				señuelo.EndTurn();

			}
		}
	}
	public void TradingCards()
	{
		Draw draw = GameObject.Find("Deck").GetComponent<Draw>();
		turns endturn = GameObject.Find("TurnSystem").GetComponent<turns>();
		CardDisplay cardDisplay = GetComponent<CardDisplay>();
		if (endturn.isPlayerTurn)
		{

			DeleteCards button1 = GameObject.Find("Button1").GetComponent<DeleteCards>();
			DeleteCards button2 = GameObject.Find("Button2").GetComponent<DeleteCards>();
			GameObject hand = GameObject.Find("PlayerArea");
			if (button1.tradingTime)
			{

				Draw deck = GameObject.Find("Deck").GetComponent<Draw>();
				List<GameObject> deckcards = deck.cards;
				if (cardDisplay.faction == "plantas")
				{

					deckcards.Add(gameObject);
					draw.Draw1Card();
					Destroy(gameObject);
					button1.counter++;
					if (button1.counter == 2)
					{

						endturn.Movable(hand, true);
						button1.tradingTime = false;
						button1.Hide();

					}

				}

			}

		}
		else if (!endturn.isPlayerTurn)
		{


			DeleteCards button2 = GameObject.Find("Button2").GetComponent<DeleteCards>();

			GameObject hand = GameObject.Find("EnemyArea");

			if (button2.tradingTime && button2.counter < 3)
			{

				Draw deck = GameObject.Find("Deck").GetComponent<Draw>();
				List<GameObject> deckcards = deck.enemycards;
				if (cardDisplay.faction == "zombies")
				{

					deckcards.Add(gameObject);
					deck.Draw1Zombies();
					Destroy(gameObject);
					button2.counter++;
					if (button2.counter == 2)
					{
						endturn.Movable(hand, true);
						button2.tradingTime = false;
						button2.Hide();
					}

				}

			}
		}
	}

}

