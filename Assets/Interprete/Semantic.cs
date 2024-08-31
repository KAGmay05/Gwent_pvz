using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Semantic : MonoBehaviour
{
public Dictionary<string, object> objectVars = new Dictionary<string, object>();
public Dictionary<string, Variable.Type> variables = new Dictionary<string, Variable.Type>();
 public Dictionary<string, Effect> effects = new Dictionary<string, Effect>();
 public Dictionary<string, CardI> cards = new Dictionary<string, CardI>();
 public Stack<Dictionary<string, Variable.Type>> scopes = new Stack<Dictionary<string, Variable.Type>>();
 public List<string> Errors = new List<string>();
 public Semantic(List<string> errors)
 {
  Errors = errors;
 }
 public void CheckNode(Node node)
 
    {
        Debug.Log("esta en checking");
        if(node is Program program)
        {
             foreach (var effect in program.Effects)
             {
                 Debug.Log(" node is an effect");
                 CheckEffect(effect);

             
             }
             foreach (var card in program.CardIs)
             {
                 Debug.Log(" node is a card");
                 CheckCard(card);
                
             }
        
        }
    }
    public void CheckCard(CardI card)
    {
     PushScope();
     CheckName(card.Name.name);
     if(card.Name.name is Number)
     {
        Errors.Add("Card name must be a string");
     }
     string cardname = ((String)card.Name.name).Value;
     if(cards.ContainsKey(cardname))
     {
        Errors.Add("Card already defined");
     }
     else cards[cardname] = card;
     Variable cardVar = new Variable(new Token(TokenType.IDENTIFIER, cardname, cardname, 0, 0));
     cardVar.type = Variable.Type.CARDI;
     if(IsVariableDeclared(cardname))
     {
       Errors.Add($"A local variable named {cardname} is already defined");
     }
     else variables[cardname] = cardVar.type;
     CheckCardType(card.Type.type);
     CheckName(card.Faction.faction);
     CheckCardPower(card.Power.power);
     if(card.Range.range != null)
     {
        foreach (var expr in card.Range.range)
        {
            CheckName(expr);
            string range = ((String)expr).Value;
            if(range != "Cuerpo a cuerpo" && range != "Arquero" && range != "Asedio" && range != "Melee" && range != "Ranged" && range != "Siege")
            {
                Errors.Add($"Range : {range} is not a valid range");
            }
        }
     }
     else Errors.Add("Card must have a range");
     if(card.OnActivation != null)
     {
        foreach(var element in card.OnActivation.Elements)
        {
            CheckOnActivationE(element);
        }
     }
     else Errors.Add("Card must have an on activation");
     PopScope();
    }
    void CheckCardPower(Expression power)
    {
     if(power is Number)
      {
        if(((Number)power).Value < 0)
        {
            Errors.Add("Card power must be greater than 0");
        }
      }
      else if(power is UnaryIntergerExpression unaryIntergerExpression)
      CheckCardPower(unaryIntergerExpression.Right);
      else if(power is BinaryIntergerExpression binaryExpression)
      {
       CheckCardPower(binaryExpression.Left);
       CheckCardPower(binaryExpression.Right);
      }
       else if(power is ExpressionGroup expressionGroup)
       CheckCardPower(expressionGroup.Exp);
       else if(power is Variable variable)
       {
        if(power is VariableComp variableComp)
        {
            CheckVariable(variableComp);
            if(variableComp.type != Variable.Type.INT)
            {
                Errors.Add("Power must be a number");
            }
        }
        else if(variable.type != Variable.Type.INT)
        {
            Errors.Add("Power must be a number");
        }
       }
       else Errors.Add("Power must be a number");
    }
    void CheckCardType(Expression type)
    {
        CheckName(type);
        if(type is Number)
        {
         Errors.Add("Card type must be a string");
        }
        else
        {
            string trueType = ((String)type).Value;
        if(trueType!= "Oro" && trueType!= "Plata" && trueType!= "Clima" && trueType!="Aumento" && trueType != "Lider")
        {
            Errors.Add($"The type: {trueType} is not a valid card type");
        }
        }
    }
    void CheckOnActivationE(OnActivationElements element)
    {
        PushScope();
        if(element.OAEffect !=null) CheckOAEffect(element.OAEffect);
        else Errors.Add("On activation must have an effect");
        if(element.Selector != null) CheckSelector(element.Selector);
        else Errors.Add("On activation must have a selector");
        if(element.postAction != null) CheckPostAction(element.postAction);
        PopScope();
    }
    void CheckOAEffect(OAEffect oaEffect)
    {
     PushScope();
     List<Assignment> assignments = oaEffect.Assingments;
     List<Node> paramsOGeffect = effects[oaEffect.Name].Params.Arguments;
     int counterParams = 0;
     int counterAsg = 0;
     if(effects.ContainsKey(oaEffect.Name) )
     {
      Debug.Log("entra a lo contiene en el diccioonario");
      foreach(var asg in assignments)
        {
            asg.Left.type = TypeExpr(asg.Right);
            foreach(var param in paramsOGeffect)
            {
                if(param is Variable variable)
                {
                    if(asg.Left.type == variable.type)
                    {
                        counterParams++;
                        counterAsg++;
                    }
                }
            }
        }
        if(paramsOGeffect.Count != counterParams || assignments.Count != counterAsg)
        Errors.Add("OnActivation effect must have the same number of parameters as the effect");
      
     }
     else 
     {
        Debug.Log("entra a q NOOOOOOOOOOOOOOOOOOOOOO lo contiene en el diccioonario");
        Errors.Add("Effect not defined");
     }

     PopScope();
    }
    void CheckSelector(Selector selector)
    {
      CheckSource(selector.Source);
      CheckPredicate(selector.Predicate);
    }
    void CheckSource(string source)
    {
        if(source == "deck")
        Debug.Log("Source: deck");
        else if(source != "parent" && source != "board" && source != "hand" && source!= "otherhand" && source != "deck" && source != "otherdeck"&& source != "field" && source != "otherfield" )
        {
            Errors.Add($"Source: {source} is not valid");
        }
        
    }
    void CheckPredicate(Predicate predicate)
    {
     PushScope();
     if(predicate.Var.type != Variable.Type.CARDI)
     {
        Errors.Add("Predicate must be of type card");
     }
     Debug.Log($"{predicate.Var.type} este es el tipo del predicado");
     variables[predicate.Var.Value] = predicate.Var.type;
     CheckExpression(predicate.Condition);
     PopScope();
    }
    void CheckPostAction(List<PostAction> postAction)
    {
     foreach(var postAc in postAction)
     {
        if(postAc.Selector != null) CheckSelector(postAc.Selector);
        CheckName(postAc.Type);
     }
    }
    public void CheckEffect(Effect effect)
    {
        Debug.Log("esta en checking effect");
        PushScope();
        CheckName(effect.Name.name);
        if(effect.Name.name is Number)
        {
            Errors.Add("Effect name must be a string");
        }
        else
         {
            string effectname = ((String)effect.Name.name).Value;
        
        if(effects.ContainsKey(effectname))
        {
           Errors.Add("Effect already defined");
        }
        else effects[effectname] = effect;
        Variable effectVar = new Variable(new Token(TokenType.IDENTIFIER, effectname, effectname, 0, 0));
        effectVar.type = Variable.Type.EFFECT;
        if(IsVariableDeclared(effectname))
        {
            Errors.Add($"A local variable or function named {effectname} is already defined");
        }
        else variables[effectname] = effectVar.type;        
        if (effect.Params != null)
        {
            foreach (var param in effect.Params.Arguments)
            {
                if (param is Variable variable)
                {
                    Debug.Log($"{variable.type} este es el tipo de la variable");
                    Variable var = new Variable(variable.Token);
                    var.type = variable.type;
                    Debug.Log($"{var.type} este es el tipo");
                    DefineVariable(var);
                 }
            }
        CheckAction(effect.Action);
        }
        PopScope();
         }
        
    }
    void CheckName(Expression name)
    {
    Debug.Log("esta en checking name");
    if(name is String )
    {
      if(name == null || name is String stringName && string.IsNullOrWhiteSpace(stringName.Value))
        {
           Errors.Add("Name cannot be null or empty");
        }

    }
    else if(name is BinaryStringExpression binaryStringExpression)
    {
     CheckName(binaryStringExpression.Left);
     CheckName(binaryStringExpression.Right);
    }
    else if(name is ExpressionGroup expressionGroup)
    {
     CheckName(expressionGroup.Exp);
    }
    else if(name is Variable variable)
    {
        if(name is VariableComp variableComp)
        {
            CheckVariable(variableComp);
            if(variableComp.type != Variable.Type.STRING)
            {
                Errors.Add("String expected");
      
            }
        }
        else if(variable.type != Variable.Type.STRING)
        {
            Errors.Add("String expected");

        }
    }
    else 
    {
        name = new Number(7);
        Errors.Add("String expected");
    }
    }
 public void CheckExpression(Expression expr)
 {
    if(expr is Variable variable)
    {
        if(!IsVariableDeclared(variable.Value))
        {
            Errors.Add($"Variable '{variable.Value}' is not declared.");
        }
    }
    else if (expr is Binary binary)
    {
        CheckExpression(binary.Left);
        CheckExpression(binary.Right);
    }
    else if(expr is Unary unary)
    {
        CheckExpression(unary.Right);
    }
 }
 public void CheckAction(Action action)
 {
    PushScope();
    DefineVariable(new Variable(new Token(TokenType.IDENTIFIER, "targets", "targets", 0, 0)));
    DefineVariable(new Variable(new Token(TokenType.IDENTIFIER, "context", "context", 0, 0)));
    foreach(var statement in action.Block.statements)
    {
        CheckStatement(statement);
    }
    PopScope();
 }
 public void CheckStatement(Statement statement)
 {
    if(statement is ForStatement forStatement)
    {
        PushScope();
        DefineVariable(forStatement.Target);
        CheckVariable(forStatement.Targets);
        CheckStatementBlock(forStatement.Body);
        PopScope();
    }
    else if(statement is Assignment assignment)
    {
        CheckAssignement(assignment);
    }
    else if(statement is WhileStatement whileStatement)
    {
        PushScope();
        CheckExpression(whileStatement.Condition);
        CheckStatementBlock(whileStatement.Body);
        PopScope();
    }
    else if(statement is FunctionDeclaration function)
    {
        CheckFunction(function);
    }
    else if(statement is VariableComp variableComp)
    {
        CheckVariable(variableComp);
    }
 }
 public void CheckVariable(Variable variable)
 {
    if(!IsVariableDeclared(variable.Value))
    {
        Errors.Add($"Variable '{variable.Value}' is not declared.");
    }
    if(variable is VariableComp variableComp)
    {
        Debug.Log("esta en variablcomp");
        foreach(var node in variableComp.args.Arguments)
        {
            Debug.Log("foreach");
            if(node is FunctionDeclaration function)
             {
                Debug.Log("function");
                CheckFunction(function);
             }
            else if (node is Expression expression)
            {
                CheckExpression(expression);
            }
            else if(node is Variable subvariable)
            {
             CheckVariable(variable);
            }
        }
        // for(int i = 0; i<variableComp.args.Arguments.Count(); i++)
        // {
        //     var arg = variableComp.args.Arguments[i];
        //     foreach(var node in variableComp.args.Arguments)
        //     Debug.Log($"en la lista esta{node} y solo eso ");
        //     if(variableComp.args.Arguments[i] is FunctionDeclaration function)
        //     {
        //         Debug.Log("function");
        //         CheckFunction(function);
        //     }
        //     else if(variableComp.args.Arguments[i] is PowerAsField powerAsField)
        //     {
        //         // CheckAssignement()
        //         // Debug.Log($"{variableComp.} estpo es poweras field");
                
        //     }
        //     else 
        //     Debug.Log("no entro");
        // }
    }
 }
 public void CheckStatementBlock(StatementBlock statementBlock)
 {
    PushScope();
    foreach(var statement in statementBlock.statements)
    {
        CheckStatement(statement);
    }
    PopScope();
 }
 public void CheckAssignement(Assignment assignment)
 {
    if(IsVariableDeclared(assignment.Left.Value))
    {
        Variable.Type type =assignment.Left.type;
        if(assignment.Right is Variable variable)
        {
            // CheckVariable(variable);
             if(IsVariableDeclared(variable.Value))
             {
            //  if( type != TypeExpr(assignment.Right) )
             Debug.Log($"Esta es la variable {variable.Value} y esta en el diccionario");
             Debug.Log($"La variable {variable.Value} tiene tipo {variables[variable.Value]}");
             if(type != variables[variable.Value])
             {
              Errors.Add($"Type mismatch, type expected {type} and type declared {variable.type}");
             }
            //  if(varia)
            //  DefineVariable(assignment.Left);
             }
            else Errors.Add($"Variable: {variable.Value} has not been defined in this scope");
        }
        
    }
    else 
    {
        if(assignment.Op.Type == TokenType.EQUAL)
         {  
            Variable.Type type = TypeExpr(assignment.Right);
            assignment.Left.type = type;
            variables[assignment.Left.Value] = assignment.Left.type;
         }
    }
    if(assignment.Left is VariableComp variableComp)
    {
      CheckVariable(variableComp);
    }
    CheckExpression(assignment.Right);
 }
 void CheckFunction(FunctionDeclaration function)
 {
    Debug.Log("entro a function");
    Variable functionvar = new Variable(new Token(TokenType.FUN, function.FunctionName, function.FunctionName, 0,0));
    variables[function.FunctionName] = functionvar.type;
    foreach(var param in function.Args.Arguments)
    {
        if(param is Expression expression)
        {
            CheckExpression(expression);
        }
        else if(param is Variable variable)
        {
            CheckVariable(variable);
        }
    }
    Debug.Log(function.FunctionName);
    if(function.FunctionName == "Pop"|| function.FunctionName == "Shuffle")
    {
     Debug.Log("entro a function pop");
     if(function.Args.Arguments != null && function.Args.Arguments.Any())
     {
        Errors.Add("Invalid params for the function declared");
     }
    }
    else if(function.FunctionName == "Push" || function.FunctionName == "SendBottom")
    {
        if(function.Args.GetType() != typeof(Card))
        Errors.Add("Invalid params for the function declared");

    }
 }
 Variable.Type TypeExpr(Expression expr)
 {
    if(expr is String)
    return Variable.Type.STRING;
    else if(expr is Number)
    return Variable.Type.INT;
    else if(expr is Bool)
    return Variable.Type.BOOL;
    else if(expr is Variable variable)
    return variable.type;
    else if(expr is ExpressionGroup expressionGroup)
    return TypeExpr(expressionGroup.Exp);
    Errors.Add("Invalid expression type");
    return Variable.Type.NULL;
}
 public  void DefineVariable(Variable variable)
 {
    scopes.Peek()[variable.Value] = variable.type;
     variables[variable.Value] = variable.type;
    Debug.Log($"variable se llama {variable.Value} y tiene tipo {variable.type}");
 }
 public bool IsVariableDeclared(string name)
 {
    foreach(var scope in scopes)
    {
        if(scope.ContainsKey(name)) return true;
        if(variables.ContainsKey(name)) return true;
    }
    return false;
 }
 void PushScope()
 {
    scopes.Push(new Dictionary<string, Variable.Type>());
 }
 void PopScope()
 {
    if (scopes.Count > 0)
        {
            scopes.Pop();
        }
        else
        {
            Errors.Add("No more scopes to pop.");
            
        }
 }
}
public class SemanticError : System.Exception
{
    public SemanticError(string message) : base(message) { }
}
