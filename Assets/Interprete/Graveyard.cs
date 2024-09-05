using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Graveyard : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();
    public GameObject graveyard;
    public GameObject cardPrefab;
    public void Push(GameObject card)
    {
        cards.Add(card);
        GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newCard.transform.SetParent(graveyard.transform, false);
    }
    public GameObject Pop()
    {
        GameObject card = cards[cards.Count-1];
        Remove(card);
        return card;
    }
    public void Remove(GameObject card)
    {
        UnityEngine.Debug.Log("removing card");
        foreach(Transform transform in graveyard.transform)
        {
            if(transform.gameObject.GetComponent<Card>()==card)
            {
                Destroy(transform.gameObject);
                break;
            }
        }
        cards.Remove(card);
    }
    public void SendBottom(GameObject card)
    {
        cards.Insert(0,card);
        GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newCard.transform.SetParent(graveyard.transform, false);
    }
    public void Shuffle()
    {
        cards = cards.OrderBy(x => UnityEngine.Random.value).ToList();
    }
}
