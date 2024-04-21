using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checking : MonoBehaviour
{
	public GameObject clima;
	public GameObject aum;
	public void CheckingEffects()
	{
		CardDisplay[] cardsClima = clima.GetComponentsInChildren<CardDisplay>();
		Effects efectos = GameObject.Find("efectos").GetComponent<Effects>();

		if (cardsClima.Length == 0)
		{
			UnityEngine.Debug.Log("No hay CardDisplay en clima");
			return;
		}
		else
		{
			foreach (var card in cardsClima)
			{
				UnityEngine.Debug.Log("entro al checking");
				if (card.efecto == "climaCaC")
				{
					UnityEngine.Debug.Log("checkeo");
					efectos.WeatherCaC();
				}
				else if (card.efecto == "climaArq")
				{
					UnityEngine.Debug.Log("checkeo");
					efectos.WeatherArq();
				}
				else if (card.efecto == "climaAsd")
				{
					UnityEngine.Debug.Log("checkeo");
					efectos.WeatherAsd();
				}

			}
		}
		CardDisplay[] cardaum = aum.GetComponentsInChildren<CardDisplay>();
		if (cardaum.Length == 0)
		{
			UnityEngine.Debug.Log("No hay CardDisplay en aumento");
			return;
		}
		else
		{
			foreach (var card in cardaum)
			{
				UnityEngine.Debug.Log("entro al checking aumento");
				if (card.efecto == "aumento")
				{
					UnityEngine.Debug.Log("checkeo aumento");
					efectos.Aumento();
				}

			}

		}
	}
}
