using System.Collections.Generic;
using System.Collections;
using UnityEngine;

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

  // zombies
  public GameObject EnemyArea;
  public GameObject liderzombies;
  public GameObject liderEnemydz;


  // listas 
  public List<GameObject> cards = new List<GameObject>();
  public List<GameObject> enemycards = new List<GameObject>();
  public List<GameObject> plantsOnBoard = new List<GameObject>();
  public List<GameObject> zombiesOnBoard = new List<GameObject>();

  public GameObject almanaqPlantas;
  public GameObject almanaqZombies;
  public GameObject info;
 

  void Start()
  {
    CanvasGroup canvasGroup = almanaqPlantas.GetComponent<CanvasGroup>();
    canvasGroup.alpha = 0;
    canvasGroup.interactable = false;
    canvasGroup.blocksRaycasts = false;
    CanvasGroup canvasGroupz = almanaqZombies.GetComponent<CanvasGroup>();
    canvasGroupz.alpha = 0;
    canvasGroupz.interactable = false;
    canvasGroupz.blocksRaycasts = false;
    CanvasGroup canvasinfo = info.GetComponent<CanvasGroup>();
    canvasinfo.alpha = 0;
    canvasinfo.interactable = false;
    canvasinfo.blocksRaycasts = false;
    
    Panel();
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
  // public void OnClickPlants()
  // {
  //   turns turns = GameObject.Find("TurnSystem").GetComponent<turns>();

  //   if (clickCount == 3)
  //   {
  //     turns.cardPlayed = true;

  //   }
  //   if (turns.cardPlayed == false)
  //   {
  //     int RandomIndex = Random.Range(0, cards.Count);
  //     GameObject playerCard1 = Instantiate(cards[RandomIndex], new Vector3(0, 0, 0), Quaternion.identity);
  //     playerCard1.transform.SetParent(PlayerArea.transform, false);
  //     cards.RemoveAt(RandomIndex);
  //   }
  // }


  public void Draw1Card()
  {

    int RandomIndex = Random.Range(0, cards.Count);
    GameObject playerCard1 = Instantiate(cards[RandomIndex], new Vector3(0, 0, 0), Quaternion.identity);
    playerCard1.transform.SetParent(PlayerArea.transform, false);
    cards.RemoveAt(RandomIndex);

  }
  public void Draw1Zombies()
  {

    int RandomIndex = Random.Range(0, enemycards.Count);
    GameObject playerCard1 = Instantiate(enemycards[RandomIndex], new Vector3(0, 0, 0), Quaternion.identity);
    playerCard1.transform.SetParent(EnemyArea.transform, false);
    enemycards.RemoveAt(RandomIndex);

  }

  // public Button plants;
  // public int clickCount = 0;
  // void Update()
  // {
  //   if (Input.GetMouseButtonDown(0))
  //   {
  //     RectTransform buttonRectTransform = plants.GetComponent<RectTransform>();
  //     if (RectTransformUtility.RectangleContainsScreenPoint(buttonRectTransform, Input.mousePosition))
  //       clickCount++;
  //   }

  // }

}


