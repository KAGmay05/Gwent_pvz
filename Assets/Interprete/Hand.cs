using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Hand : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public GameObject hand;
    public GameObject cardPrefab;
    public List<Card> Find(Func<Card,bool> func)
    {
        return cards.Where(func).ToList();
    }
    public void Push(Card card)
    {
        cards.Add(card);
        GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newCard.transform.SetParent(hand.transform, false);
    }
    public Card Pop()
    {
        Card card = cards[cards.Count-1];
        Remove(card);
        return card;
    }
    public void Remove(Card card)
    {
        UnityEngine.Debug.Log("removing card");
        foreach(Transform transform in hand.transform)
        {
            if(transform.gameObject.GetComponent<Card>()==card)
            {
                Destroy(transform.gameObject);
                break;
            }
        }
        cards.Remove(card);
    }
    public void SendBottom(Card card)
    {
        cards.Insert(0,card);
        GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newCard.transform.SetParent(hand.transform, false);
    }
    public void Shuffle()
    {
        cards = cards.OrderBy(x => UnityEngine.Random.value).ToList();
    }
}
