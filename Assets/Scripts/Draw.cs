using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Draw : MonoBehaviour 
{
  public GameObject Card;
  public GameObject PlayerArea;
  public GameObject EnemyArea;
  List<GameObject> cards = new List<GameObject>();
	
	void Start () 
	{
		cards.Add(Card);
	}
	
	public void OnClick()
	{
		GameObject playerCard = Instantiate(cards[Random.Range(0, cards.Count)], new UnityEngine.Vector3(0, 0, 0), Quaternion.identity);
        playerCard.transform.SetParent(PlayerArea.transform, false);

        // playerCard.GetComponent<Drag>().dropZone=GameObject.Find("Arq");

        GameObject enemyCard = Instantiate(cards[Random.Range(0, cards.Count)], new UnityEngine.Vector3(0, 0, 0), Quaternion.identity);
        enemyCard.transform.SetParent(EnemyArea.transform, false);
	}
}
