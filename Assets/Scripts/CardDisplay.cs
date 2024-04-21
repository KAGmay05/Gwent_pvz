using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
  public string tipo;
  public string faction;
  public int power;
  public string efecto;
  public string zone;
  public Card card;
  public Text nameText;
  public Text descriptionText;
  public Image plantbckgrImage;
  public Text powerText;
  public string zoneaux;
  public Image plantimage;
  public Image kind;
  public string zonextra;
  public int ogpower;
  public string name;
  void Start()
  {
    tipo = card.tipo;
    name = card.name;
    ogpower = card.ogpower;
    faction = card.faction;
    efecto = card.efecto;
    power = card.power;
    zonextra = card.zonextra;
    zoneaux = card.zoneaux;
    zone = card.zone;
    nameText.text = card.name;
    descriptionText.text = card.description;
    plantbckgrImage.sprite = card.plantbckgrImage;
    powerText.text = card.power.ToString();
    plantimage.sprite = card.plantimage;
    kind.sprite = card.kind;
  }

  /*public bool CanPlayCard (bool faction, char zone)
  {
    if (cardFaction == faction && cardZone == zone)
      return true;
    else
      return false;
  }*/

}
