// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Decoy : MonoBehaviour
// {

// 	public GameObject decoyCard;
// 	public GameObject card;
// 	public GameObject playerArea;
// 	bool decoy;

// 	public void OnClickCard()
// 	{
// 		if (decoy == true)
// 		{
// 			card.transform.SetParent(playerArea.transform, false);
// 		}
// 	}
// 	public void OnClickDecoy()
// 	{
// 		decoy = true;
// 		Transform parent = card.transform.parent;


// 		decoyCard.transform.SetParent(parent.transform, false);
// 	}

// }
