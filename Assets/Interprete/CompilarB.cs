using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class CompilarB : MonoBehaviour
{
    public GameManager gameManager;
    public TMP_InputField input;
    public TMP_InputField outputText;
    public GameObject cardPrefab;
    string output = "Tokens: \n";
    public GameObject errors;
    public turns turns;
    public GameObject PlayerField;
    public GameObject EnemyField;
    
    public void CompileAndRun()
    {
        List<string> Errors =new List<string>();
        // try
        // {
        // if(Errors.Count != 0)
        // {
        //     foreach(var error in Errors)
        //     Errors.Remove(error);
        // }
        string code = input.text;
        Lexer lexer = new Lexer(code,Errors);
        List<Token> tokens = lexer.ScanTokens();
        CanvasGroup canvaserrores = errors.GetComponent<CanvasGroup>();
        if(Errors.Count != 0)
        {
            outputText.text = string.Join("\n", Errors);
            canvaserrores.alpha = 1;
            canvaserrores.interactable = true;
            canvaserrores.blocksRaycasts = true;   
        }
        // foreach(var token in tokens)
        // {
        //  output += $"{token.Type}: {token.Lexeme}\n";
        // }

        Parser parser = new Parser(tokens, Errors);
        Node ast = parser.Parse();
        if(Errors.Count != 0)
        {
            outputText.text = string.Join("\n", Errors);
            canvaserrores.alpha = 1;
            canvaserrores.interactable = true;
            canvaserrores.blocksRaycasts = true;   
        }
        // Debug.Log("Parsed Effect:");
        // effect.Show();
        Semantic semanticNew = new Semantic(Errors);
        semanticNew.CheckNode(ast);
        if(Errors.Count != 0)
        {
            outputText.text = string.Join("\n", Errors);
            canvaserrores.alpha = 1;
            canvaserrores.interactable = true;
            canvaserrores.blocksRaycasts = true;   
        }
        else
        {
            outputText.text = "Carta creada con éxito";
            canvaserrores.alpha = 1;
            canvaserrores.interactable = true;
            canvaserrores.blocksRaycasts = true;
        
        if(ast is Program program)
        {   
             
             foreach(var card in program.CardIs)
             {
                 Debug.Log(" node is a card");
                if(turns.isPlayerTurn)
                {
                    // GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    // newCard.transform.SetParent(PlayerField.transform, false);
                    // CardDisplay cardDisplay = newCard.GetComponent<CardDisplay>();
                    // cardDisplay.name = card.Name.name.Evaluate(semanticNew, gameManager).ToString();
                    // cardDisplay.tipo = card.Type.type.Evaluate(semanticNew, gameManager).ToString();
                    // cardDisplay.power = (int)card.Power.power.Evaluate(semanticNew, gameManager);   
                    // cardDisplay.faction = card.Faction.faction.Evaluate(semanticNew, gameManager).ToString();
                    // cardDisplay.efecto = "effectI";
                    Card cardPrueba  = new Card();
                    cardPrueba.name =  card.Name.name.Evaluate(semanticNew, gameManager).ToString();
                    cardPrueba.tipo = card.Type.type.Evaluate(semanticNew, gameManager).ToString();
                    cardPrueba.power = (int)card.Power.power.Evaluate(semanticNew, gameManager);   
                    cardPrueba.faction = card.Faction.faction.Evaluate(semanticNew, gameManager).ToString();
                    cardPrueba.efecto = "effectI";
                    if(card.Range.range.Count() == 1)
                    {
                     if(card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Melee")
                     {
                        // cardDisplay.zone = "playerCaC";
                        // cardDisplay.zoneaux = "playerCaC";
                        // cardDisplay.zonextra = "playerCaC";
                        cardPrueba.zone = "playerCaC";
                        cardPrueba.zoneaux = "playerCaC";
                        cardPrueba.zonextra = "playerCaC";
                     }
                     if(card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Ranged")
                     {
                        // cardDisplay.zone = "playerArq";
                        // cardDisplay.zoneaux = "playerArq";
                        // cardDisplay.zonextra = "playerArq";
                        cardPrueba.zone = "playerArq";
                        cardPrueba.zoneaux = "playerArq";
                        cardPrueba.zonextra = "playerArq";
                     }
                     if(card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Siege")
                     {
                        // cardDisplay.zone = "playerAsd";
                        // cardDisplay.zoneaux = "playerAsd";
                        // cardDisplay.zonextra = "playerAsd";
                        cardPrueba.zone = "playerAsd";
                        cardPrueba.zoneaux = "playerAsd";
                        cardPrueba.zonextra = "playerAsd";
                     }
                    }
                    else if(card.Range.range.Count() == 2)
                    {
                        if(card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Siege" && card.Range.range[1].Evaluate(semanticNew, gameManager).ToString() == "Ranged"|| card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Ranged" && card.Range.range[1].Evaluate(semanticNew, gameManager).ToString() == "Siege")
                        // cardDisplay.zone = "playerAsd";
                        // cardDisplay.zoneaux = "playerArq";
                        // cardDisplay.zonextra = "playerArq";
                        cardPrueba.zone = "playerAsd";
                        cardPrueba.zoneaux = "playerArq";
                        cardPrueba.zonextra = "playerArq";
                        if(card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Melee" && card.Range.range[1].Evaluate(semanticNew, gameManager).ToString() == "Ranged"|| card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Ranged" && card.Range.range[1].Evaluate(semanticNew, gameManager).ToString() == "Melee")
                        {
                            // cardDisplay.zone = "playerCaC";
                            // cardDisplay.zoneaux = "playerArq";
                            // cardDisplay.zonextra = "playerArq";
                            cardPrueba.zone = "playerCaC";
                            cardPrueba.zoneaux = "playerArq";
                            cardPrueba.zonextra = "playerArq";
                        }
                        if(card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Melee" && card.Range.range[1].Evaluate(semanticNew, gameManager).ToString() == "Siege"|| card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Siege" && card.Range.range[1].Evaluate(semanticNew, gameManager).ToString() == "Melee")
                        {
                            // cardDisplay.zone = "playerCaC";
                            // cardDisplay.zoneaux = "playerAsd";
                            // cardDisplay.zonextra = "playerAsd";
                            cardPrueba.zone = "playerCaC";
                            cardPrueba.zoneaux = "playerAsd";
                            cardPrueba.zonextra = "playerAsd";
                        }
                    }
                    else if(card.Range.range.Count() == 3)
                    {
                        // cardDisplay.zone = "playerCaC";
                        // cardDisplay.zoneaux = "playerArq";
                        // cardDisplay.zonextra = "playerAsd";
                        cardPrueba.zone = "playerCaC";
                        cardPrueba.zoneaux = "playerArq";
                        cardPrueba.zonextra = "playerAsd";
                    }  
                    cardPrueba.onActivation = card.OnActivation;
                    cardPrueba.semantic = semanticNew;
                    CardDisplay cardDisplay = cardPrefab.GetComponent<CardDisplay>();
                    cardDisplay.card = cardPrueba;
                    Hand hand = PlayerField.GetComponent<Hand>();
                    hand.Push(cardPrueba);
                    
                }
                else
                {
                    Card cardPrueba  = new Card();
                    cardPrueba.name =  card.Name.name.Evaluate(semanticNew, gameManager).ToString();
                    cardPrueba.tipo = card.Type.type.Evaluate(semanticNew, gameManager).ToString();
                    cardPrueba.power = (int)card.Power.power.Evaluate(semanticNew, gameManager);   
                    cardPrueba.faction = card.Faction.faction.Evaluate(semanticNew, gameManager).ToString();
                    cardPrueba.efecto = "effectI";
                    if(card.Range.range.Count() == 1)
                    {
                     if(card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Melee")
                     {
                        // cardDisplay.zone = "playerCaC";
                        // cardDisplay.zoneaux = "playerCaC";
                        // cardDisplay.zonextra = "playerCaC";
                        cardPrueba.zone = "enemyCaC";
                        cardPrueba.zoneaux = "enemyCaC";
                        cardPrueba.zonextra = "enemyCaC";
                     }
                     if(card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Ranged")
                     {
                        // cardDisplay.zone = "enemyArq";
                        // cardDisplay.zoneaux = "enemyArq";
                        // cardDisplay.zonextra = "enemyArq";
                        cardPrueba.zone = "enemyArq";
                        cardPrueba.zoneaux = "enemyArq";
                        cardPrueba.zonextra = "enemyArq";
                     }
                     if(card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Siege")
                     {
                        // cardDisplay.zone = "enemyAsd";
                        // cardDisplay.zoneaux = "enemyAsd";
                        // cardDisplay.zonextra = "enemyAsd";
                        cardPrueba.zone = "enemyAsd";
                        cardPrueba.zoneaux = "enemyAsd";
                        cardPrueba.zonextra = "enemyAsd";
                     }
                    }
                    else if(card.Range.range.Count() == 2)
                    {
                        if(card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Siege" && card.Range.range[1].Evaluate(semanticNew, gameManager).ToString() == "Ranged"|| card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Ranged" && card.Range.range[1].Evaluate(semanticNew, gameManager).ToString() == "Siege")
                        // cardDisplay.zone = "enemyAsd";
                        // cardDisplay.zoneaux = "enemyArq";
                        // cardDisplay.zonextra = "enemyArq";
                        cardPrueba.zone = "enemyAsd";
                        cardPrueba.zoneaux = "enemyArq";
                        cardPrueba.zonextra = "enemyArq";
                        if(card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Melee" && card.Range.range[1].Evaluate(semanticNew, gameManager).ToString() == "Ranged"|| card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Ranged" && card.Range.range[1].Evaluate(semanticNew, gameManager).ToString() == "Melee")
                        {
                            // cardDisplay.zone = "enemyCaC";
                            // cardDisplay.zoneaux = "enemyArq";
                            // cardDisplay.zonextra = "enemyArq";
                            cardPrueba.zone = "enemyCaC";
                            cardPrueba.zoneaux = "enemyArq";
                            cardPrueba.zonextra = "enemyArq";
                        }
                        if(card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Melee" && card.Range.range[1].Evaluate(semanticNew, gameManager).ToString() == "Siege"|| card.Range.range[0].Evaluate(semanticNew, gameManager).ToString() == "Siege" && card.Range.range[1].Evaluate(semanticNew, gameManager).ToString() == "Melee")
                        {
                            // cardDisplay.zone = "enemyCaC";
                            // cardDisplay.zoneaux = "enemyAsd";
                            // cardDisplay.zonextra = "enemyAsd";
                            cardPrueba.zone = "enemyCaC";
                            cardPrueba.zoneaux = "enemyAsd";
                            cardPrueba.zonextra = "enemyAsd";
                        }
                    }
                    else if(card.Range.range.Count() == 3)
                    {
                        // cardDisplay.zone = "enemyCaC";
                        // cardDisplay.zoneaux = "enemyArq";
                        // cardDisplay.zonextra = "enemyAsd";
                        cardPrueba.zone = "enemyCaC";
                        cardPrueba.zoneaux = "enemyArq";
                        cardPrueba.zonextra = "enemyAsd";
                    }  
                    cardPrueba.onActivation = card.OnActivation;
                    cardPrueba.semantic = semanticNew;
                    CardDisplay cardDisplay = cardPrefab.GetComponent<CardDisplay>();
                    cardDisplay.card = cardPrueba;
                    Hand hand = EnemyField.GetComponent<Hand>();
                    hand.Push(cardPrueba);
                }
                
             }
        }
        }

        
        
}
}

// void ShowErrors()
// {
//         CanvasGroup canvaserrores = errors.GetComponent<CanvasGroup>();
//         canvaserrores.alpha = 1;
//         canvaserrores.interactable = true;
//         canvaserrores.blocksRaycasts = true;
// }

