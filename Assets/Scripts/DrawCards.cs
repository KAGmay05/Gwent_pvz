using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DrawCards : MonoBehaviour 
{
    public GameObject normalcard;
    public GameObject Cardwithhability;
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public GameObject pinchohierbapunzante;
    public GameObject coladegato;
    List<GameObject> cards = new List<GameObject>();
    void Start()
    {
        cards.Add(normalcard); //quite una pila de gente
  

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
