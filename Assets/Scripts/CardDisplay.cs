using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour 
{
  public string zone;
  public Card card;
  public Text nameText;
  public Text descriptionText;
  public Image plantImageImage;
  public Text powerText;	
  public string zoneaux;
 
	void Start () 
	{
    zoneaux = card.zoneaux;
    zone = card.zone;
		nameText.text = card.name;
    descriptionText.text = card.description;
    plantImageImage.sprite = card.plantImage;
		powerText.text = card.power.ToString();
	}
	
  /*public bool CanPlayCard (bool faction, char zone)
  {
    if (cardFaction == faction && cardZone == zone)
      return true;
    else
      return false;
  }*/
	
}
