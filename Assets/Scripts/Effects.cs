using System.Text.RegularExpressions;
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
    public GameObject aumCaC;
    public GameObject aumArqzombies;

    public CardDisplay efectos;
    private List<GameObject> affectedcards = new List<GameObject>();
    public Draw draw;
    void Start()
    {

    }


    public void Aumento()
    {

        foreach (Transform child in aumCaC.transform)
        {


            GameObject card = child.gameObject;
            CardDisplay cardDisplay = child.GetComponent<CardDisplay>();
            if (cardDisplay.tipo != "oro")
            {
                if (!affectedcards.Contains(card))
                {
                    cardDisplay.power = cardDisplay.power * 2;
                }
                affectedcards.Add(card);
            }
        }
    }
    public void AumentoZombies()
    {
        foreach (Transform child in aumArqzombies.transform)
        {
            GameObject card = child.gameObject;
            CardDisplay cardDisplay = child.GetComponent<CardDisplay>();
            if (cardDisplay.tipo != "oro")
            {
                if (!affectedcards.Contains(card))
                {
                  cardDisplay.power = cardDisplay.power * 2;

                }
                affectedcards.Add(card);
            }
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
            if (card.tipo != "oro")
            {
                if (card.power > maxCaC)
                {
                    maxCaC = card.power;
                    maxCardCaC = card;

                }
            }

        }

        CardDisplay[] cardsArq = playerArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsArq)
        {
            if (card.tipo != "oro")
            {
                if (card.power > maxArq)
                {
                    maxArq = card.power;
                    maxCardArq = card;

                }
            }
        }

        CardDisplay[] cardsAsd = playerAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsAsd)
        {
            if (card.tipo != "oro")
            {
                if (card.power > maxAsd)
                {
                    maxAsd = card.power;
                    maxCardAsd = card;

                }
            }

        }
        int max = Math.Max(maxCaC, maxArq);
        max = Math.Max(max, maxAsd);

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
            if (card.tipo != "oro")
            {
                if (card.power > maxenemyCaC)
                {
                    maxenemyCaC = card.power;
                    maxCardCaCzombies = card;

                }
            }

        }

        CardDisplay[] cardsenemyArq = enemyArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsenemyArq)
        {
            if (card.tipo != "oro")
            {
                if (card.power > maxenemyArq)
                {
                    maxenemyArq = card.power;
                    maxCardArqzombies = card;

                }
            }
        }

        CardDisplay[] cardsenemyAsd = enemyAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsenemyAsd)
        {
            if (card.tipo != "oro")
            {
                if (card.power > maxenemyAsd)
                {
                    maxenemyAsd = card.power;
                    maxCardAsdzombies = card;

                }
            }

        }

        int maxZombies = Math.Max(maxenemyCaC, maxenemyArq);
        maxZombies = Math.Max(maxZombies, maxenemyAsd);
        max = Math.Max(max, maxZombies);
        if (max != 0)
        {
            if (max == maxCaC)
            {
                maxCardCaC.transform.SetParent(cementery.transform, false);
            }
            else if (max == maxArq)
            {
                maxCardArq.transform.SetParent(cementery.transform, false);
            }
            else if (max == maxAsd)
            {
                maxCardAsd.transform.SetParent(cementery.transform, false);
            }

            else if (max == maxenemyCaC)
            {
                maxCardCaCzombies.transform.SetParent(cementery.transform, false);
            }
            else if (max == maxenemyArq)
            {
                maxCardArqzombies.transform.SetParent(cementery.transform, false);
            }
            else
            {
                maxCardAsdzombies.transform.SetParent(cementery.transform, false);
            }


        }

    }

    public void DeleteMinZombiecard()
    {
        gameManager = GameObject.Find("gamemanager").GetComponent<GameManager>();

        int minCaC = 0;
        int minArq = 0;
        int minAsd = 0;
        CardDisplay minCardCaC = null;
        CardDisplay minCardArq = null;
        CardDisplay minCardAsd = null;



        CardDisplay[] cardsCaC = enemyCaC.GetComponentsInChildren<CardDisplay>();
        if (cardsCaC != null)
        {
            foreach (var card in cardsCaC)
            {
                if (card.tipo != "oro")
                {
                    minCaC = card.power;
                    minCardCaC = card;
                    UnityEngine.Debug.Log("entro al primer if efecto");
                    if (card.power < minCaC)
                    {
                        UnityEngine.Debug.Log("entro al segundo if");
                        minCaC = card.power;
                        minCardCaC = card;

                    }

                }

            }
        }

        CardDisplay[] cardsArq = enemyArq.GetComponentsInChildren<CardDisplay>();
        if (cardsArq != null)
        {
            foreach (var card in cardsArq)
            {
                if (card.tipo != "oro")
                {
                    minArq = card.power;
                    minCardArq = card;
                    if (card.power < minArq)
                    {
                        minArq = card.power;
                        minCardArq = card;

                    }

                }
            }
        }

        CardDisplay[] cardsAsd = enemyAsd.GetComponentsInChildren<CardDisplay>();
        if (cardsAsd != null)
        {
            foreach (var card in cardsAsd)
            {
                if (card.tipo != "oro")
                {
                    minAsd = card.power;
                    minCardAsd = card;
                    if (card.power < minAsd)
                    {
                        minAsd = card.power;
                        minCardAsd = card;

                    }

                }

            }
        }
        int min = 0;


        if (minCaC == 0 && minAsd == 0)
        {
            min = minArq;
        }
        else if (minArq == 0 && minAsd == 0)
        {
            min = minCaC;
        }
        else if (minCaC == 0 && minArq == 0)
        {
            min = minAsd;
        }
        else if (minCaC == 0)
        {
            min = Math.Min(minAsd, minArq);
        }
        else if (minAsd == 0)
        {
            min = Math.Min(minCaC, minArq);
        }
        else if (minArq == 0)
        {
            min = Math.Min(minCaC, minAsd);
        }
        else
        {
            min = Math.Min(minCaC, minArq);
            min = Math.Min(min, minAsd);
        }

        if (min != 0)
        {
            if (min == minCaC)
            {
                UnityEngine.Debug.Log("encontro la carta en CaC");
                minCardCaC.transform.SetParent(cementery.transform, false);

            }
            else if (min == minArq)
            {
                UnityEngine.Debug.Log("encontro la carta en aqr");
                minCardArq.transform.SetParent(cementery.transform, false);
            }
            else
            {
                UnityEngine.Debug.Log("encontro la carta en asd");
                minCardAsd.transform.SetParent(cementery.transform, false);
            }
        }

    }
    public void WeatherCaC()
    {
        CardDisplay[] cardsPlantsCaC = playerCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsPlantsCaC)
        {
            if (card.tipo != "oro")
            {
                card.power = 1;
            }

        }
        CardDisplay[] cardsZombiesCaC = enemyCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesCaC)
        {
            if (card.tipo != "oro")
            {
                card.power = 1;
            }

        }

    }
    public void WeatherArq()
    {
        CardDisplay[] cardsPlantsArq = playerArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsPlantsArq)
        {
            if (card.tipo != "oro")
            {
                card.power = 1;
            }

        }
        CardDisplay[] cardsZombiesArq = enemyArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesArq)
        {
            if (card.tipo != "oro")
            {
                card.power = 1;
            }


        }

    }
    public void WeatherAsd()
    {
        CardDisplay[] cardsPlantsAsd = playerAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsPlantsAsd)
        {
            if (card.tipo != "oro")
            {
                card.power = 1;
            }
        }
        CardDisplay[] cardsZombiesAsd = enemyAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesAsd)
        {
            if (card.tipo != "oro")
            {
                card.power = 1;
            }

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
            if (card.tipo != "oro")
            {
                plantsCaC++;
            }

        }
        CardDisplay[] cardsArq = playerArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsArq)
        {
            if (card.tipo != "oro")
            {
                plantsArq++;
            }

        }
        CardDisplay[] cardsAsd = playerAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsAsd)
        {
            if (card.tipo != "oro")
            {
                plantsAsd++;
            }

        }
        CardDisplay[] cardsZombiesCaC = enemyCaC.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesCaC)
        {
            if (card.tipo != "oro")
            {
                zombiesCaC++;
            }

        }
        CardDisplay[] cardsZombiesArq = enemyArq.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesArq)
        {
            if (card.tipo != "oro")
            {
                zombiesArq++;
            }

        }
        CardDisplay[] cardsZombiesAsd = enemyAsd.GetComponentsInChildren<CardDisplay>();
        foreach (var card in cardsZombiesAsd)
        {
            if (card.tipo != "oro")
            {
                zombiesAsd++;
            }


        }

        int plantsMin = 0;
        if (plantsCaC == 0 && plantsAsd == 0)
        {
            plantsMin = plantsArq;
        }
        else if (plantsArq == 0 && plantsAsd == 0)
        {
            plantsMin = plantsCaC;
        }
        else if (plantsCaC == 0 && plantsArq == 0)
        {
            plantsMin = plantsAsd;
        }
        else if (plantsCaC == 0)
        {
            plantsMin = Math.Min(plantsAsd, plantsArq);
        }
        else if (plantsAsd == 0)
        {
            plantsMin = Math.Min(plantsCaC, plantsArq);
        }
        else if (plantsArq == 0)
        {
            plantsMin = Math.Min(plantsCaC, plantsAsd);
        }
        else
        {
            plantsMin = Math.Min(plantsCaC, plantsAsd);
            plantsMin = Math.Min(plantsMin, plantsArq);
        }




        int zombiesMin = 0;
        if (zombiesCaC == 0 && zombiesAsd == 0)
        {
            zombiesMin = zombiesArq;
        }
        else if (zombiesArq == 0 && zombiesAsd == 0)
        {
            zombiesMin = zombiesCaC;
        }
        else if (zombiesCaC == 0 && zombiesArq == 0)
        {
            zombiesMin = zombiesAsd;
        }
        else if (zombiesCaC == 0)
        {
            zombiesMin = Math.Min(zombiesAsd, zombiesArq);
        }
        else if (zombiesAsd == 0)
        {
            zombiesMin = Math.Min(zombiesCaC, zombiesArq);
        }
        else if (zombiesArq == 0)
        {
            zombiesMin = Math.Min(zombiesCaC, zombiesAsd);
        }
        else
        {
            zombiesMin = Math.Min(zombiesCaC, zombiesAsd);
            zombiesMin = Math.Min(zombiesMin, zombiesArq);
        }

        int totalmin = Math.Min(plantsMin, zombiesMin);

        if (totalmin == zombiesCaC)
        {
            foreach (var card in cardsZombiesCaC)
            {
                card.transform.SetParent(cementery.transform, false);
            }
        }
        else if (totalmin == zombiesArq)
        {
            foreach (var card in cardsZombiesArq)
            {
                card.transform.SetParent(cementery.transform, false);
            }
        }
        else if (totalmin == zombiesAsd)
        {
            foreach (var card in cardsZombiesAsd)
            {
                card.transform.SetParent(cementery.transform, false);
            }
        }
        else if (totalmin == plantsCaC)
        {
            foreach (var card in cardsCaC)
            {
                if (cardsCaC != null)
                {
                    card.transform.SetParent(cementery.transform, false);
                }
            }
        }
        else if (totalmin == plantsArq)
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

    }
    public void Multiply()
    {
        int numAsd = 0;
        int numArqplants = 0;
        int numCaCzombies = 0;
        int numAsdzombies = 0;

        string mazorc = "mazorcañon";

        CardDisplay[] cardsAsd = playerAsd.GetComponentsInChildren<CardDisplay>();
        List<CardDisplay> mazorccards = new List<CardDisplay>();
        foreach (var card in cardsAsd)
        {

            if (card.name == mazorc)
            {
                UnityEngine.Debug.Log("entro mazorca");
                mazorccards.Add(card);
                numAsd++;
                UnityEngine.Debug.Log("el numero de veces puesto es" + numAsd);

            }
            foreach (var cardmazor in mazorccards)
            {

                cardmazor.power = cardmazor.ogpower * numAsd;
                UnityEngine.Debug.Log("el power es" + card.power);
            }

        }


        CardDisplay[] cardsArq = playerArq.GetComponentsInChildren<CardDisplay>();
        List<CardDisplay> guisanteshccards = new List<CardDisplay>();
        foreach (var card in cardsArq)
        {
            if (card.name == "guisantesHielo")
            {
                UnityEngine.Debug.Log("entro guisantesHielo");
                guisanteshccards.Add(card);
                numArqplants++;

            }
            foreach (var cardguisantesh in guisanteshccards)
            {

                cardguisantesh.power = cardguisantesh.ogpower * numArqplants;
                UnityEngine.Debug.Log("el power es" + card.power);
            }

        }
        CardDisplay[] cardsZombiesAsd = enemyAsd.GetComponentsInChildren<CardDisplay>();
        List<CardDisplay> zomboniccards = new List<CardDisplay>();
        foreach (var card in cardsZombiesAsd)
        {
            if (card.name == "zomboni")
            {
                zomboniccards.Add(card);
                numAsdzombies++;
                UnityEngine.Debug.Log("entro zomboni");

            }
            foreach (var cardzomboni in zomboniccards)
            {

                cardzomboni.power = cardzomboni.ogpower * numAsdzombies;
                UnityEngine.Debug.Log("el power es" + card.ogpower);
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

                cardzombieCono.power = cardzombieCono.ogpower * numCaCzombies;
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


        int newpowerPlants = 0;
        int newpowerZombies = 0;
        if (totalPlants != 0)
        {
            newpowerPlants = pPoints / totalPlants;

        }
        else
        {
            newpowerPlants = pPoints;
        }
        if (totalzombies != 0)
        {
            newpowerZombies = zPoints / totalzombies;
        }
        else
        {
            newpowerZombies = zPoints;
        }




        foreach (var card in cardsCaC)
        {
            if (card.tipo != "oro" && newpowerPlants != 0)
            {
                card.power = newpowerPlants;
            }

        }


        foreach (var card in cardsArq)
        {
            if (card.tipo != "oro" && newpowerPlants != 0)
            {
                card.power = newpowerPlants;
            }
        }


        foreach (var card in cardsAsd)
        {
            if (card.tipo != "oro" && newpowerPlants != 0)
            {
                card.power = newpowerPlants;
            }

        }

        foreach (var card in cardsenemyCaC)
        {
            if (card.tipo != "oro" && newpowerZombies != 0)
            {
                card.power = newpowerZombies;
            }

        }


        foreach (var card in cardsenemyArq)
        {
            if (card.tipo != "oro" && newpowerZombies != 0)
            {
                card.power = newpowerZombies;
            }
        }


        foreach (var card in cardsenemyAsd)
        {
            if (card.tipo != "oro" && newpowerZombies != 0)
            {
                card.power = newpowerZombies;
            }

        }

    }
}


