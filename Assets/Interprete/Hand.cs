using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Hand : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();
    public List<Card> cardsA = new List<Card>();
    public GameObject hand;
    public GameObject cardPrefab;
    public List<GameObject> Find(Predicate predicate, Semantic semantic, GameManager gameManager)
    {
        List<GameObject> filtredCards = new List<GameObject>();
        foreach(var card in cards)
        {
            semantic.objectVars[predicate.Var.Value] = card;
            if((bool)predicate.Condition.Evaluate(semantic, gameManager)) filtredCards.Add(card);
            semantic.objectVars.Remove(predicate.Var.Value);
        }
        return filtredCards;
    }
    public void Push(GameObject card)
    {
        cards.Add(card);
        GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newCard.transform.SetParent(hand.transform, false);
    }
    public void PushA(Card card)
    {
        cardsA.Add(card);
        GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newCard.transform.SetParent(hand.transform, false);
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
    public void SendBottom(GameObject card)
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
