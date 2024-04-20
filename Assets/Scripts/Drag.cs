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
			else if (effect == a || aum == true)
			{
				aum = true;
				efectos.Aumento();

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
				UnityEngine.Debug.Log("multiplico");
				efectos.Multiply();

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
		if (señuelo.decoytime && señuelo.factionp)
		{
			UnityEngine.Debug.Log("esta en el primer if de on click");
			if (cardDisplay.faction == "plantas")
			{
				GameObject zone1 = GameObject.Find("PlayerArea");
				cardDisplay.power = cardDisplay.ogpower;
				transform.position = zone1.transform.position;
				transform.SetParent(zone1.transform, false);
				señuelo.decoytime = false;
				onField = false;
				señuelo.EndTurn();
			}
		}
		else if (señuelo.decoytime && señuelo.factionz)
		{
			if (cardDisplay.faction == "zombies")
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

}

