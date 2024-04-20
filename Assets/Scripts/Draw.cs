using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
  public GameObject panel;
  public Drag drag;
  public turns endturn;
  bool isPlayerTurn = true;
  // plants
  public GameObject PlayerArea;
  public GameObject lider;
  public GameObject liderdz;
  public GameObject guisantralladora;
  public GameObject melonpulta;
  public GameObject pinchohierba;
  public GameObject coladegato;
  public GameObject luz;
  public GameObject hielo;
  public GameObject margarita;
  public GameObject trebol;
  public GameObject repetidora;
  public GameObject espia;
  public GameObject apisonaflor1;
  public GameObject apisonaflor2;
  public GameObject apisonaflor3;
  public GameObject apio1;
  public GameObject apio2;
  public GameObject lanzaguisantestres1;
  public GameObject lanzaguisantestres2;
  public GameObject guisanteshielo1;
  public GameObject guisanteshielo2;
  public GameObject guisanteshielo3;
  public GameObject mazorcañon1;
  public GameObject mazorcañon2;
  public GameObject mazorcañon3;
  public GameObject nuez1;
  public GameObject nuez2;
  // zombies
  public GameObject EnemyArea;
  public GameObject liderzombies;
  public GameObject liderEnemydz;
  public GameObject zombiegigante;
  public GameObject zombicubo;
  public GameObject atleta;
  public GameObject quaterback;
  public GameObject minizomb;
  public GameObject yeti;
  public GameObject cajasorpresa;
  public GameObject zombienormal;
  public GameObject zombiebandera;
  public GameObject zombieladron;
  public GameObject zombiecono1;
  public GameObject zombiecono2;
  public GameObject zombiecono3;
  public GameObject zombieperiod1;
  public GameObject zombieperiod2;
  public GameObject zomboni1;
  public GameObject zomboni2;
  public GameObject zomboni3;
  public GameObject zombiecatapul1;
  public GameObject zombiecatapul2;
  public GameObject zombieminero;
  public GameObject zombieglobo1;
  public GameObject zombieglobo2;
  public GameObject zombieEscal1;
  public GameObject zombieEscal2;

  // listas 
  public List<GameObject> cards = new List<GameObject>();
  public List<GameObject> enemycards = new List<GameObject>();
  public List<GameObject> plantsOnBoard = new List<GameObject>();
  public List<GameObject> zombiesOnBoard = new List<GameObject>();


  void Start()
  {
    Panel();

    cards.Add(guisantralladora);
    cards.Add(trebol);
    cards.Add(coladegato);
    cards.Add(luz);
    cards.Add(hielo);
    cards.Add(pinchohierba);
    cards.Add(apisonaflor1);
    cards.Add(apisonaflor2);
    cards.Add(apisonaflor3);
    cards.Add(apio1);
    cards.Add(apio2);
    cards.Add(margarita);
    cards.Add(espia);
    cards.Add(repetidora);
    cards.Add(lanzaguisantestres1);
    cards.Add(lanzaguisantestres2);
    cards.Add(guisanteshielo1);
    cards.Add(guisanteshielo2);
    cards.Add(guisanteshielo3);
    cards.Add(mazorcañon1);
    cards.Add(mazorcañon2);
    cards.Add(mazorcañon3);
    cards.Add(nuez1);
    cards.Add(nuez2);
    cards.Add(melonpulta);

    GameObject lidercard = Instantiate(lider, new Vector3(0, 0, 0), Quaternion.identity);
    lidercard.transform.SetParent(liderdz.transform, false);

    for (var i = 0; i < 10; i++)
    {
      int RandomIndex = Random.Range(0, cards.Count);
      GameObject playerCard1 = Instantiate(cards[RandomIndex], new Vector3(0, 0, 0), Quaternion.identity);
      playerCard1.transform.SetParent(PlayerArea.transform, false);
      plantsOnBoard.Add(cards[RandomIndex]);
      cards.RemoveAt(RandomIndex);
    }
    enemycards.Add(yeti);
    enemycards.Add(minizomb);
    enemycards.Add(atleta);
    enemycards.Add(cajasorpresa);
    enemycards.Add(zombiegigante);
    enemycards.Add(zombienormal);
    enemycards.Add(zombicubo);
    enemycards.Add(quaterback);
    enemycards.Add(zombiebandera);
    enemycards.Add(zombieladron);
    enemycards.Add(zombiecono2);
    enemycards.Add(zombiecono3);
    enemycards.Add(zombiecono1);
    enemycards.Add(zombieperiod1);
    enemycards.Add(zombieperiod2);
    enemycards.Add(zomboni1);
    enemycards.Add(zomboni2);
    enemycards.Add(zomboni3);
    enemycards.Add(zombiecatapul1);
    enemycards.Add(zombiecatapul2);
    enemycards.Add(zombieminero);
    enemycards.Add(zombieglobo1);
    enemycards.Add(zombieglobo2);
    enemycards.Add(zombieEscal1);
    enemycards.Add(zombieEscal2);

    GameObject liderencard = Instantiate(liderzombies, new Vector3(0, 0, 0), Quaternion.identity);
    liderencard.transform.SetParent(liderEnemydz.transform, false);

    for (var i = 0; i < 10; i++)
    {
      int RandomIndex = Random.Range(0, enemycards.Count);
      GameObject enemyCard1 = Instantiate(enemycards[RandomIndex], new Vector3(0, 0, 0), Quaternion.identity);
      enemyCard1.transform.SetParent(EnemyArea.transform, false);
      zombiesOnBoard.Add(enemycards[RandomIndex]);
      enemycards.RemoveAt(RandomIndex);
    }
  }
  public void Panel()
  {
    CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
    canvasGroup.alpha = 0;
    canvasGroup.interactable = false;
    canvasGroup.blocksRaycasts = false;
  }

  public void SecondRound()
  {
    for (var i = 0; i < 2; i++)
    {
      int RandomIndex = Random.Range(0, cards.Count);
      GameObject playerCard1 = Instantiate(cards[RandomIndex], new Vector3(0, 0, 0), Quaternion.identity);
      playerCard1.transform.SetParent(PlayerArea.transform, false);
      cards.RemoveAt(RandomIndex);
    }
    for (var i = 0; i < 2; i++)
    {
      int RandomIndex = Random.Range(0, enemycards.Count);
      GameObject enemyCard1 = Instantiate(enemycards[RandomIndex], new Vector3(0, 0, 0), Quaternion.identity);
      enemyCard1.transform.SetParent(EnemyArea.transform, false);
      enemycards.RemoveAt(RandomIndex);
    }
  }
  public void ThirdRound()
  {
    for (var i = 0; i < 3; i++)
    {
      int RandomIndex = Random.Range(0, cards.Count);
      GameObject playerCard1 = Instantiate(cards[RandomIndex], new Vector3(0, 0, 0), Quaternion.identity);
      playerCard1.transform.SetParent(PlayerArea.transform, false);
      cards.RemoveAt(RandomIndex);
    }
    for (var i = 0; i < 3; i++)
    {
      int RandomIndex = Random.Range(0, enemycards.Count);
      GameObject enemyCard1 = Instantiate(enemycards[RandomIndex], new Vector3(0, 0, 0), Quaternion.identity);
      enemyCard1.transform.SetParent(EnemyArea.transform, false);
      enemycards.RemoveAt(RandomIndex);
    }
  }
  public void OnClickPlants()
  {
    turns turns = GameObject.Find("TurnSystem").GetComponent<turns>();

    if (clickCount == 3)
    {
      turns.cardPlayed = true;

    }
    if (turns.cardPlayed == false)
    {
      int RandomIndex = Random.Range(0, cards.Count);
      GameObject playerCard1 = Instantiate(cards[RandomIndex], new Vector3(0, 0, 0), Quaternion.identity);
      playerCard1.transform.SetParent(PlayerArea.transform, false);
      cards.RemoveAt(RandomIndex);
    }
  }


  public void OnClick()
  {

    int RandomIndex = Random.Range(0, cards.Count);
    GameObject playerCard1 = Instantiate(cards[RandomIndex], new Vector3(0, 0, 0), Quaternion.identity);
    playerCard1.transform.SetParent(PlayerArea.transform, false);
    cards.RemoveAt(RandomIndex);

  }
  public Button plants;
  public int clickCount = 0;
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      RectTransform buttonRectTransform = plants.GetComponent<RectTransform>();
      if (RectTransformUtility.RectangleContainsScreenPoint(buttonRectTransform, Input.mousePosition))
        clickCount++;
    }

  }

}


