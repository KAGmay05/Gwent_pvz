using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checking : MonoBehaviour
{
	public GameObject clima;
	public void CheckingEffects()
	{
		UnityEngine.Debug.Log("entro al checking afuera");
		Effects efectos = GameObject.Find("efectos").GetComponent<Effects>();
		CardDisplay[] cardsClima = clima.GetComponentsInChildren<CardDisplay>();
		foreach (var card in cardsClima)
		{
			UnityEngine.Debug.Log("entro al checking");


			CardDisplay cardDisplay = card.GetComponent<CardDisplay>();
			if (cardDisplay.efecto == "climaCaC")
			{
				UnityEngine.Debug.Log("checkeo");
				efectos.WeatherCaC();
			}

		}

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

