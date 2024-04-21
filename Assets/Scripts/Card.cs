using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string name;
    public string nameCards;
    public string description;
    public int power;
    public Sprite plantbckgrImage;
    public string zone;
    public string zoneaux;
    public Sprite plantimage;
    public Sprite kind;
    public string zonextra;
    public string efecto;
    public string faction;
    public int ogpower;
    public string tipo;

    public Card()
    {

    }
}
