using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checking : MonoBehaviour
{
	public GameObject clima;
	public GameObject aum;
	public GameObject aumZombies;
	public void CheckingEffects()
	{

		CardDisplay[] cardsClima = clima.GetComponentsInChildren<CardDisplay>();
		Effects efectos = GameObject.Find("efectos").GetComponent<Effects>();
		efectos.Multiply();
		if (cardsClima.Length == 0)
		{

			return;
		}
		else
		{
			foreach (var card in cardsClima)
			{

				if (card.efecto == "climaCaC")
				{

					efectos.WeatherCaC();
				}
				else if (card.efecto == "climaArq")
				{

					efectos.WeatherArq();
				}
				else if (card.efecto == "climaAsd")
				{

					efectos.WeatherAsd();
				}

			}
		}
		CardDisplay[] cardaum = aum.GetComponentsInChildren<CardDisplay>();
		if (cardaum.Length == 0)
		{

			return;
		}
		else
		{
			foreach (var card in cardaum)
			{

				if (card.efecto == "aumento")
				{

					efectos.Aumento();
				}

			}

		}
		CardDisplay[] cardaumZombies = aumZombies.GetComponentsInChildren<CardDisplay>();
		if (cardaumZombies.Length == 0)
		{

			return;
		}
		else
		{
			foreach (var card in cardaumZombies)
			{

				if (card.efecto == "aumento")
				{

					efectos.AumentoZombies();
				}

			}

		}
	}
}
