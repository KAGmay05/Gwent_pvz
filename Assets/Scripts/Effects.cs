using System.Text.RegularExpressions;


using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class Effects : MonoBehaviour
{

    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public GameObject clima;
    public GameObject cementery;
    public GameObject playerCaC;
    public GameObject playerArq;
    public GameObject playerAsd;
    public GameObject enemyCaC;
    public GameObject enemyArq;
    public GameObject enemyAsd;
    public GameManager gameManager;
    public GameObject zonaaum;
    public CardDisplay efectos;
    private List<GameObject> affectedcards = new List<GameObject>();
    public Draw draw;
    void Start()
    {

    }


    public void Aumento()
    {
        foreach (Transform child in zonaaum.transform)
        {
            GameObject card = child.gameObject;
            if (!affectedcards.Contains(card))
            {
                CardDisplay cardDisplay = child.GetComponent<CardDisplay>();


                cardDisplay.power = cardDisplay.power * 2;

            }
            affectedcards.Add(card);
        }
    }
    public void DeleteMaxPlantcard()
    {
        gameManager = GameObject.Find("gamemanager").GetComponent<GameManager>();
        // del rival
        int maxCaC = 0;
        int maxArq = 0;
        int maxAsd = 0;
        CardDisplay maxCardCaC = null;
        CardDisplay maxCardArq = null;
        CardDisplay maxCardAsd = null;

        CardDisplay[] cardsCaC = playerCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsCaC)
        {
            if (card.power > maxCaC)
            {
                maxCaC = card.power;
                maxCardCaC = card;

            }

        }

        CardDisplay[] cardsArq = playerArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsArq)
        {
            if (card.power > maxCaC)
            {
                maxArq = card.power;
                maxCardArq = card;

            }
        }

        CardDisplay[] cardsAsd = playerAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsAsd)
        {
            if (card.power > maxCaC)
            {
                maxAsd = card.power;
                maxCardAsd = card;

            }

        }
        int max = Math.Max(maxCaC, maxArq);
        max = Math.Max(max, maxAsd);
        if (max == maxCaC)
        {
            maxCardCaC.transform.SetParent(cementery.transform, false);
        }
        else if (max == maxArq)
        {
            maxCardArq.transform.SetParent(cementery.transform, false);
        }
        else
        {
            maxCardAsd.transform.SetParent(cementery.transform, false);
        }
        // propio
        int maxenemyCaC = 0;
        int maxenemyArq = 0;
        int maxenemyAsd = 0;
        CardDisplay maxCardCaCzombies = null;
        CardDisplay maxCardArqzombies = null;
        CardDisplay maxCardAsdzombies = null;

        CardDisplay[] cardsenemyCaC = enemyCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsenemyCaC)
        {
            if (card.power > maxCaC)
            {
                maxenemyCaC = card.power;
                maxCardCaCzombies = card;

            }

        }

        CardDisplay[] cardsenemyArq = enemyArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsenemyArq)
        {
            if (card.power > maxCaC)
            {
                maxenemyArq = card.power;
                maxCardArqzombies = card;

            }
        }

        CardDisplay[] cardsenemyAsd = enemyAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsenemyAsd)
        {
            if (card.power > maxCaC)
            {
                maxenemyAsd = card.power;
                maxCardAsdzombies = card;

            }

        }
        int maxZombies = Math.Max(maxenemyCaC, maxenemyArq);
        maxZombies = Math.Max(maxZombies, maxenemyAsd);
        if (maxZombies == maxenemyCaC)
        {
            maxCardCaCzombies.transform.SetParent(cementery.transform, false);
        }
        else if (maxZombies == maxenemyArq)
        {
            maxCardArqzombies.transform.SetParent(cementery.transform, false);
        }
        else
        {
            maxCardAsdzombies.transform.SetParent(cementery.transform, false);
        }

    }
    public void DeleteMinZombiecard()
    {
        gameManager = GameObject.Find("gamemanager").GetComponent<GameManager>();

        int maxCaC = 0;
        int maxArq = 0;
        int maxAsd = 0;
        CardDisplay maxCardCaC = null;
        CardDisplay maxCardArq = null;

        CardDisplay maxCardAsd = null;



        CardDisplay[] cardsCaC = enemyCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsCaC)
        {
            if (card.power > maxCaC)
            {
                maxCaC = card.power;
                maxCardCaC = card;

            }

        }

        CardDisplay[] cardsArq = enemyArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsArq)
        {
            if (card.power > maxCaC)
            {
                maxArq = card.power;
                maxCardArq = card;

            }
        }

        CardDisplay[] cardsAsd = enemyAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsAsd)
        {
            if (card.power > maxCaC)
            {
                maxAsd = card.power;
                maxCardAsd = card;

            }

        }
        int max = Math.Max(maxCaC, maxArq);
        max = Math.Max(max, maxAsd);
        if (max == maxCaC)
        {
            maxCardCaC.transform.SetParent(cementery.transform, false);
        }
        else if (max == maxArq)
        {
            maxCardArq.transform.SetParent(cementery.transform, false);
        }
        else
        {
            maxCardAsd.transform.SetParent(cementery.transform, false);
        }

    }
    public void WeatherCaC()
    {
        CardDisplay[] cardsPlantsCaC = playerCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsPlantsCaC)
        {
            card.power = 1;

        }
        CardDisplay[] cardsZombiesCaC = enemyCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesCaC)
        {
            card.power = 1;

        }

    }
    public void WeatherArq()
    {
        CardDisplay[] cardsPlantsArq = playerArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsPlantsArq)
        {
            card.power = 1;

        }
        CardDisplay[] cardsZombiesArq = enemyArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesArq)
        {
            card.power = 1;


        }

    }
    public void WeatherAsd()
    {
        CardDisplay[] cardsPlantsAsd = playerAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsPlantsAsd)
        {
            card.power = 1;
        }
        CardDisplay[] cardsZombiesAsd = enemyAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesAsd)
        {
            card.power = 1;

        }
    }
    public void Despeje()
    {
        CardDisplay[] cardsClima = clima.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsClima)
        {
            card.transform.SetParent(cementery.transform, false);

        }
        EnableWeather();
    }
    public void EnableWeather()
    {

        CardDisplay[] cardsPlantsCaC = playerCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsPlantsCaC)
        {
            card.power = card.ogpower;


        }
        CardDisplay[] cardsZombiesCaC = enemyCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesCaC)
        {
            card.power = card.ogpower;

        }
        CardDisplay[] cardsPlantsArq = playerArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsPlantsArq)
        {
            card.power = card.ogpower;

        }
        CardDisplay[] cardsZombiesArq = enemyArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesArq)
        {
            card.power = card.ogpower;


        }
        CardDisplay[] cardsPlantsAsd = playerAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsPlantsAsd)
        {
            card.power = card.ogpower;
        }
        CardDisplay[] cardsZombiesAsd = enemyAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesAsd)
        {
            card.power = card.ogpower;

        }
    }
    public void RobarPlants()
    {
        draw = GameObject.Find("Deck").GetComponent<Draw>();

        int RandomIndex = Random.Range(0, draw.cards.Count);
        GameObject playerCard1 = Instantiate(draw.cards[RandomIndex], new Vector3(0, 0, 0), Quaternion.identity);
        playerCard1.transform.SetParent(PlayerArea.transform, false);
        draw.cards.RemoveAt(RandomIndex);
    }
    public void RobarZombies()
    {
        draw = GameObject.Find("Deck").GetComponent<Draw>();

        int RandomIndex = Random.Range(0, draw.enemycards.Count);
        GameObject playerCard1 = Instantiate(draw.enemycards[RandomIndex], new Vector3(0, 0, 0), Quaternion.identity);
        playerCard1.transform.SetParent(EnemyArea.transform, false);
        draw.enemycards.RemoveAt(RandomIndex);
    }
    public void DeleteRow()
    {
        int plantsCaC = 0;
        int plantsArq = 0;
        int plantsAsd = 0;
        int zombiesCaC = 0;
        int zombiesArq = 0;
        int zombiesAsd = 0;
        CardDisplay[] cardsCaC = playerCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsCaC)
        {
            plantsCaC++;

        }
        CardDisplay[] cardsArq = playerArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsArq)
        {
            plantsArq++;

        }
        CardDisplay[] cardsAsd = playerAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsAsd)
        {
            plantsAsd++;

        }
        CardDisplay[] cardsZombiesCaC = enemyCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesCaC)
        {
            zombiesCaC++;

        }
        CardDisplay[] cardsZombiesArq = enemyArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesArq)
        {
            zombiesArq++;

        }
        CardDisplay[] cardsZombiesAsd = enemyAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesAsd)
        {
            zombiesAsd++;

        }
        int plantsMin = Math.Min(plantsCaC, plantsAsd);
        plantsMin = Math.Min(plantsMin, plantsArq);
        if (plantsMin == plantsCaC)
        {
            foreach (var card in cardsCaC)
            {
                if (cardsCaC != null)
                {
                    card.transform.SetParent(cementery.transform, false);
                }
            }
        }
        else if (plantsMin == plantsArq)
        {
            foreach (var card in cardsArq)
            {
                if (cardsArq != null)
                {
                    card.transform.SetParent(cementery.transform, false);
                }
            }
        }
        else
        {
            foreach (var card in cardsAsd)
            {
                if (cardsAsd != null)
                {
                    card.transform.SetParent(cementery.transform, false);
                }
            }
        }
        int zombiesMin = Math.Min(zombiesCaC, zombiesAsd);
        zombiesMin = Math.Min(zombiesMin, zombiesArq);
        if (zombiesMin == zombiesCaC)
        {
            foreach (var card in cardsZombiesCaC)
            {
                card.transform.SetParent(cementery.transform, false);
            }
        }
        else if (zombiesMin == zombiesArq)
        {
            foreach (var card in cardsZombiesArq)
            {
                card.transform.SetParent(cementery.transform, false);
            }
        }
        else
        {
            foreach (var card in cardsZombiesAsd)
            {
                card.transform.SetParent(cementery.transform, false);
            }
        }

    }
    public void Multiply()
    {
        int numAsd = 0;
        int numArqplants = 0;
        int numCacplants = 0;
        int numCaCzombies = 0;
        int numArqzombies = 0;

        string mazorc = "mazorcañon";

        CardDisplay[] cardsAsd = playerAsd.GetComponentsInChildren<CardDisplay>();
        List<CardDisplay> mazorccards = new List<CardDisplay>();
        foreach (var card in cardsAsd)
        {

            if (card.name == mazorc)
            {
                mazorccards.Add(card);
                numAsd++;
                UnityEngine.Debug.Log("el numero de veces puesto es" + numAsd);

            }
            foreach (var cardmazor in mazorccards)
            {

                cardmazor.power = cardmazor.power * numAsd;
                UnityEngine.Debug.Log("el power es" + card.power);
            }

        }

        CardDisplay[] cardsCaC = playerCaC.GetComponentsInChildren<CardDisplay>();
        List<CardDisplay> apisonaflorccards = new List<CardDisplay>();
        foreach (var card in cardsCaC)
        {
            if (card.name == "apisonaflor")
            {
                apisonaflorccards.Add(card);
                numCacplants++;

            }
            foreach (var cardapisonaflor in apisonaflorccards)
            {

                cardapisonaflor.power = cardapisonaflor.power * numCacplants;
                UnityEngine.Debug.Log("el power es" + card.power);
            }

        }
        CardDisplay[] cardsArq = playerArq.GetComponentsInChildren<CardDisplay>();
        List<CardDisplay> guisanteshccards = new List<CardDisplay>();
        foreach (var card in cardsArq)
        {
            if (card.name == "guisantesHielo")
            {
                guisanteshccards.Add(card);
                numArqplants++;

            }
            foreach (var cardguisantesh in guisanteshccards)
            {

                cardguisantesh.power = cardguisantesh.power * numArqplants;
                UnityEngine.Debug.Log("el power es" + card.power);
            }

        }
        CardDisplay[] cardsZombiesArq = enemyArq.GetComponentsInChildren<CardDisplay>();
        List<CardDisplay> zomboniccards = new List<CardDisplay>();
        foreach (var card in cardsZombiesArq)
        {
            if (card.name == "zomboni")
            {
                zomboniccards.Add(card);
                numArqzombies++;

            }
            foreach (var cardzomboni in zomboniccards)
            {

                cardzomboni.power = cardzomboni.power * numArqzombies;
                UnityEngine.Debug.Log("el power es" + card.power);
            }

        }

        List<CardDisplay> zombieConoccards = new List<CardDisplay>();
        CardDisplay[] cardsZombiesCaC = enemyCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesCaC)
        {
            if (card.name == "zombieCono")
            {
                zombieConoccards.Add(card);
                numCaCzombies++;

            }
            foreach (var cardzombieCono in zombieConoccards)
            {

                cardzombieCono.power = cardzombieCono.power * numCaCzombies;
                UnityEngine.Debug.Log("el power es" + card.power);
            }

        }
    }
    public void Promedio()
    {
        int plantsCaC = 0;
        int plantsAsd = 0;
        int plantsArq = 0;
        int zombiesCaC = 0;
        int zombiesArq = 0;
        int zombiesAsd = 0;
        int totalPlants = 0;
        int totalzombies = 0;

        CardDisplay[] cardsCaC = playerCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsCaC)
        {
            plantsCaC++;

        }

        CardDisplay[] cardsArq = playerArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsArq)
        {
            plantsArq++;
        }

        CardDisplay[] cardsAsd = playerAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsAsd)
        {
            plantsAsd++;

        }
        CardDisplay[] cardsenemyCaC = enemyCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsenemyCaC)
        {
            zombiesCaC++;


        }

        CardDisplay[] cardsenemyArq = enemyArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsenemyArq)
        {
            zombiesArq++;

        }

        CardDisplay[] cardsenemyAsd = enemyAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsenemyAsd)
        {
            zombiesAsd++;

        }
        totalPlants = plantsArq + plantsAsd + plantsCaC;


        totalzombies = zombiesArq + zombiesAsd + zombiesCaC;
        GameManager gameManager = GameObject.Find("gamemanager").GetComponent<GameManager>();
        int pPoints = gameManager.totalPlantspoints + 12;
        int zPoints = gameManager.totalZombiespoints;

        int newpowerPlants = pPoints / totalPlants;
        int newpowerZombies = zPoints / totalzombies;


        foreach (var card in cardsCaC)
        {
            card.power = newpowerPlants;

        }


        foreach (var card in cardsArq)
        {
            card.power = newpowerPlants;
        }


        foreach (var card in cardsAsd)
        {
            card.power = newpowerPlants;

        }

        foreach (var card in cardsenemyCaC)
        {
            card.power = newpowerZombies;

        }


        foreach (var card in cardsenemyArq)
        {
            card.power = newpowerZombies;
        }


        foreach (var card in cardsenemyAsd)
        {
            card.power = newpowerZombies;

        }

    }
}


