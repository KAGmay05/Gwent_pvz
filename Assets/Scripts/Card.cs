using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string nameCards;
    public string description;
    public int power;
    public Sprite plantImage;
    public string zone;
    public string zoneaux;
	
    public Card ()
    {

    }
}
