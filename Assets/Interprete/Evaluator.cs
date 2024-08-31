using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Evaluator : MonoBehaviour
{
    GameManager gameManager;
    Card card;
    Semantic semantic;
    public Evaluator(Card card,Semantic semantic, GameManager gameManager )
    {
        this.card = card;
        this.semantic = semantic;
        this.gameManager = gameManager;
    }
    public void EvaluateCardEffects()
    {
     foreach (var oAElement in card.onActivation.Elements)
     {
        EvaluateOAEffect(oAElement.OAEffect,oAElement.Selector, oAElement.Selector.Source);
        
        if(oAElement.postAction != null)
        {
            foreach(var postAction in oAElement.postAction)
            {
                EvaluatePostActions(postAction,oAElement.Selector.Source);
            }
        }
     }
    }
    public void EvaluateOAEffect(OAEffect oAEffect, Selector selector, string source)
    {
     Effect effect = semantic.effects[oAEffect.Name];
     UnityEngine.Debug.Log(effect.Name + "esta en evaluate OAEffect");
     foreach(var assignment in oAEffect.Assingments)
     {
        semantic.objectVars[assignment.Left.Value] = assignment.Right.Evaluate(semantic, gameManager);
        UnityEngine.Debug.Log(assignment.Left.Value + "esta en evaluate OAEffect" + "este es right" + assignment.Right.Evaluate(semantic, gameManager));
     }
     if(selector !=null)
     {
        semantic.objectVars["targets"] = EvaluateSelector(selector, source);
        EvaluateAction(effect.Action);
        semantic.objectVars.Remove("targets");
     }
     else
     EvaluateAction(effect.Action);
    }
    public void EvaluateAction(Action action)
    {
        EvaluateStatementBlock(action.Block);
    }
    public List<Card> EvaluateSelector( Selector selector, string source)
    {
     List<Card> cards = new List<Card>();
     if(selector.Source == "parent") cards = EvaluateSource(source);
     else cards = EvaluateSource(selector.Source);
     List<Card> filteredCards = new List<Card>();
     foreach( var card in cards)
     {
        semantic.objectVars[selector.Predicate.Var.Value] = card;
        if((bool)selector.Predicate.Condition.Evaluate(semantic, gameManager))
        {
            UnityEngine.Debug.Log($"{selector.Predicate.Condition.Evaluate(semantic, gameManager)} no se q hay aqui");
            filteredCards.Add(card);
        }
        semantic.objectVars.Remove(selector.Predicate.Var.Value);

     }
     if(selector.Single.Value)
     {
        if(filteredCards.Count()!= 0)
        {
            UnityEngine.Debug.Log("la lista no esta vacia");
            List<Card> finalCards = new List<Card>{ filteredCards[0]};
        return finalCards;
        }
        else return filteredCards;
     }
     else return filteredCards;
    }
    public Func<Card,bool> EvaluatePredicate(Predicate predicate)
    {
        return card =>
        {
            semantic.objectVars[predicate.Var.Value] = card;
            return (bool)predicate.Condition.Evaluate(semantic, gameManager);
        };
    }
    public List<Card> EvaluateSource(string source)
    {
     if(source == "hand") 
     {
        if(gameManager != null) UnityEngine.Debug.Log("game manager esta !null");
         if(gameManager.HandOfPlayer(gameManager.TriggerPlayer()) != null){ UnityEngine.Debug.Log("esta !null la lista de cartas"); 
         if(gameManager.TriggerPlayer() == 0)
         {
            foreach(var card in gameManager.HandOfPlayer(0))
         {
            UnityEngine.Debug.Log(card + "AQUI LAS CARTASSSSSSS");
            
         }
            return gameManager.HandOfPlayer(0);
         }
         else 
         {
            foreach(var card in gameManager.HandOfPlayer(1))
         {
            UnityEngine.Debug.Log(card + "AQUI LAS CARTASSSSSSS");
            
         }
            return gameManager.HandOfPlayer(1);
         }
    
         }
         else UnityEngine.Debug.Log("esta vacia la list");
     }
    
     if(source == "otherhand")
     {
        if(gameManager.TriggerPlayer() == 0) return gameManager.HandOfPlayer(1);
        else return gameManager.HandOfPlayer(0);
     }
     if(source == "field") return gameManager.FieldOfPlayer(gameManager.TriggerPlayer());
     if(source == "otherfield")
     {
        if(gameManager.TriggerPlayer() == 0) return gameManager.FieldOfPlayer(1);
        else return gameManager.FieldOfPlayer(0);
     }
     if(source == "deck") return gameManager.DeckofPlayer(gameManager.TriggerPlayer());
     if(source == "otherdeck")
     {
        if(gameManager.TriggerPlayer() == 0) return gameManager.DeckofPlayer(1);
        else return gameManager.DeckofPlayer(0);
     }
     else return gameManager.CardsOnBoard();
    }
    public void EvaluatePostActions(PostAction postAction,  string source)
    {
     Effect effect = semantic.effects[(string)postAction.Type.Evaluate(semantic, gameManager)];
     foreach(var assignment in postAction.Assignments)
     {
        semantic.objectVars[assignment.Left.Value] = assignment.Right.Evaluate(semantic, gameManager);
     }
     if(postAction.Selector != null)
     {
        semantic.objectVars["targets"] = EvaluateSelector(postAction.Selector, source);
        EvaluateAction(effect.Action);
        semantic.objectVars.Remove("targets");
     }
     else 
     EvaluateAction(effect.Action);
    }
    public void EvaluateStatementBlock(StatementBlock statementBlock)
    {
        foreach(var statement in statementBlock.statements)
        {
            statement.Ejecuta(semantic,gameManager);
        }
    }
    public void EvaluateStatement(Statement statement)
    {
     if(statement is WhileStatement whileStatement)
     {
        while((bool)whileStatement.Condition.Evaluate(semantic, gameManager))
        {
         foreach(var stmt in whileStatement.Body.statements)
         {
          EvaluateStatement(stmt);
         }
        }
     }
     if(statement is VariableComp variableComp)
     {
        object result = null;
        foreach(var arg in variableComp.args.Arguments)
        {
           if(arg is FunctionDeclaration functionDeclaration)
           {
            // result = EvaluateFunction(functionDeclaration);
           }
           else if(arg is Pointer pointer)
           {
            if(pointer.pointer == "hand") gameManager.HandOfPlayer(gameManager.TriggerPlayer());
            if(pointer.pointer == "Deck") gameManager.DeckofPlayer(gameManager.TriggerPlayer());
            if(pointer.pointer == "Field") gameManager.FieldOfPlayer(gameManager.TriggerPlayer());
            if(pointer.pointer == "board") gameManager.CardsOnBoard();
            if(pointer.pointer == "Graveyard") gameManager.GraveyardOfPlayer(gameManager.TriggerPlayer());
           }
        }
     }
     if(statement is ForStatement forStatement)
     {
        List<Card> targets = semantic.objectVars["targets"] as List<Card>;
        foreach(Card target in targets)
        {
         semantic.objectVars["target"] = target;
         foreach(var smt in forStatement.Body.statements)
         {
            EvaluateStatement(smt);
         }
         semantic.objectVars.Remove("targets");
        }
     }
     if(statement is Assignment assignment)
     {
        EvaluateAssignment(assignment);
     }
    }
    // public object EvaluateFunction(FunctionDeclaration functionDeclaration)
    // {
    //     if(functionDeclaration.FunctionName == "TriggerPlayer") return gameManager.TriggerPlayer();
    //     if(functionDeclaration.FunctionName == "HandOfPlayer") 
    //     {
    //         if(functionDeclaration.Args.Arguments[0] is FunctionDeclaration function)
    //         {
    //          return gameManager.HandOfPlayer(Convert.ToInt32(function as Expression).Evaluate(semantic, gameManager));
    //         }
    //     }
    //     return gameManager.HandOfPlayer(Convert.ToInt32(functionDeclaration.Args.Arguments[0].Evaluate(semantic, gameManager)));
    // }
    public void EvaluateAssignment(Assignment assignment)
    {
       
         
        if(assignment.Op.Type == TokenType.LESS_EQUAL)
        {
            if(assignment.Left is VariableComp variableComp)
            {
             AssignVariableComp(variableComp, Convert.ToInt32(assignment.Left.Evaluate(semantic, gameManager)) - Convert.ToInt32(assignment.Right.Evaluate(semantic, gameManager)));
            }
            else
            {
            int result = Convert.ToInt32(semantic.variables[assignment.Left.Value]);
            result -= Convert.ToInt32(assignment.Right.Evaluate(semantic, gameManager));
            semantic.objectVars[assignment.Left.Value] = result;
            }
       
        }
        if(assignment.Op.Type == TokenType.PLUS_EQUALS)
        {
            if(assignment.Left is VariableComp variableComp)
            {
             AssignVariableComp(variableComp, Convert.ToInt32(assignment.Left.Evaluate(semantic, gameManager)) + Convert.ToInt32(assignment.Right.Evaluate(semantic, gameManager)));
            }
            int result = Convert.ToInt32(semantic.variables[assignment.Left.Value]);
            result -= Convert.ToInt32(assignment.Right.Evaluate(semantic, gameManager));
            semantic.objectVars[assignment.Left.Value] = result;
        }
        else if(assignment.Op.Type == TokenType.EQUAL)
        {
            if(assignment.Left is VariableComp variableComp)
            {
                AssignVariableComp(variableComp, assignment.Right.Evaluate(semantic, gameManager));
            }
            semantic.objectVars[assignment.Left.Value] = assignment.Right.Evaluate(semantic, gameManager);
        }
         
    }
    public void AssignVariableComp(VariableComp variableComp, object value)
    {
     foreach(var arg in variableComp.args.Arguments)
     {
        if(arg is FunctionDeclaration functionDeclaration)
        {
            // EvaluateFunction(functionDeclaration);
        }
        if(arg is Pointer pointer)
        {
            if(pointer.pointer == "hand") gameManager.HandOfPlayer(gameManager.TriggerPlayer());
            if(pointer.pointer == "Deck") gameManager.DeckofPlayer(gameManager.TriggerPlayer());
            if(pointer.pointer == "Field") gameManager.FieldOfPlayer(gameManager.TriggerPlayer());
            if(pointer.pointer == "board") gameManager.CardsOnBoard();
       }
       else if(arg is Card card)
       {
        if(arg is Name ) card.name = value as string;
        if(arg is Faction) card.faction = value as string;
        if(arg is Type) card.tipo = value as string;
        if(arg is PowerAsField) card.ogpower = Convert.ToInt32(value);

       }
    }

    }
    public object EvaluateVariableComp(VariableComp variableComp)
    {
        object last = null;
        foreach(var arg in variableComp.args.Arguments)
        {
            // if(arg is FunctionDeclaration)
            // {
            //     last = (arg as FunctionDeclaration).GetValue(context,last);
            // }
            // else if(arg is Indexer)
            // {
            //     if(last is CardList)
            //     {
            //         List<Card> cards = (last as CardList).Cards;
            //         Indexer indexer = arg as Indexer;
            //         last = cards[indexer.index];
            //     }
            //     else
            //     {
            //         string[] range = last as string[];
            //         Indexer indexer = arg as Indexer;
            //         last = range[indexer.index];
            //     }
            // }
         if(arg is Pointer)
            {
                Pointer pointer = arg as Pointer;
                switch(pointer.pointer)
                {
                    case "Hand": last = gameManager.HandOfPlayer(gameManager.TriggerPlayer());break;
                    case "Deck": last = gameManager.DeckofPlayer(gameManager.TriggerPlayer());break;
                    case "Graveyard": last = gameManager.GraveyardOfPlayer(gameManager.TriggerPlayer());break;
                    case "Field": last = gameManager.FieldOfPlayer(gameManager.TriggerPlayer());break;
                    case "Board": last = gameManager.CardsOnBoard();break;
                }
            }
            else
            {
                Card card = last as Card;
                switch(arg)
                {
                    case Type: last = card.tipo;break;
                    case Name: last = card.name;break;
                    case Faction: last = card.faction;break;
                    case PowerAsField: last = card.power;break;
                    // case Range: last = card.range;break;
                    // case Owner: last = card.Owner;break;
                }
            }
        }
        return last;
    }
}
