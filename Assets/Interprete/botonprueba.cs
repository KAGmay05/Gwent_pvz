using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botonprueba : MonoBehaviour
{
 public GameManager gameManager;
 public void List()
 {
  if(gameManager != null)
 { 
    foreach(var card in gameManager.HandOfPlayer(gameManager.TriggerPlayer()))
  {
    UnityEngine.Debug.Log(card);
  }
  UnityEngine.Debug.Log(gameManager.TriggerPlayer());
  foreach(var card in gameManager.DeckofPlayer(gameManager.TriggerPlayer()))
  {
    UnityEngine.Debug.Log(card);
  }
  foreach(var card in gameManager.CardsOnBoard())
  {
    UnityEngine.Debug.Log(card);
  }
 }
 }
}
