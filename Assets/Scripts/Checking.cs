using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checking : MonoBehaviour
{
	public GameObject clima;
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

			}
		}
		// foreach (Transform child in clima.transform)
		// {
		// 	UnityEngine.Debug.Log("entro al checking");
		// }

		// UnityEngine.Debug.Log("entro al checking afuera");

		// // CardDisplay[] cardsClima = clima.GetComponentsInChildren<CardDisplay>();
		// if (clima == null)
		// {
		// 	UnityEngine.Debug.Log("clima es null");
		// 	return;
		// }

		// if (cardsClima.Length == 0)
		// {
		// 	UnityEngine.Debug.Log("No hay CardDisplay en clima");
		// 	return;
		// }
		// if (!clima.activeInHierarchy)
		// {
		// 	UnityEngine.Debug.Log("clima está inactivo");
		// 	return;
		// }





		// foreach (Transform child in playerArq.transform)
		// {
		// 	GameObject card = child.gameObject;
		// 	card.transform.SetParent(cementery.transform, false);
		// }
		// foreach (Transform child in playerAsd.transform)
		// {
		// 	GameObject card = child.gameObject;
		// 	card.transform.SetParent(cementery.transform, false);
		// }
		// foreach (Transform child in enemyArq.transform)
		// {
		// 	GameObject card = child.gameObject;
		// 	card.transform.SetParent(cementery.transform, false);
		// }
		// foreach (Transform child in enemyAsd.transform)
		// {
		// 	GameObject card = child.gameObject;
		// 	card.transform.SetParent(cementery.transform, false);
		// }
		// foreach (Transform child in enemyCaC.transform)
		// {
		// 	GameObject card = child.gameObject;
		// 	card.transform.SetParent(cementery.transform, false);
		// }
		// foreach (Transform child in clima.transform)
		// {
		// 	GameObject card = child.gameObject;
		// 	card.transform.SetParent(cementery.transform, false);
		// }
	}

}
