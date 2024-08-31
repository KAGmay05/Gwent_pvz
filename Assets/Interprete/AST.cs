using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Node
{
    public void Show(int space = 0);
}

public class Program : Node
{
    public List<CardI> CardIs;
    public List<Effect> Effects;
    public Program()
    {
        CardIs = new List<CardI>();
        Effects = new List<Effect>();
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Program:");
        foreach (var cardI in CardIs)
        {
            cardI.Show(space + 2);
        }
        foreach (var effect in Effects)
        {
            effect.Show(space + 2);
        }
    }
}

public class CardI : Node
{
    public Type Type;
    public Name Name;
    public Faction Faction;
    public Power Power;
    public Range Range;
    public OnActivation OnActivation;
    public CardI()
    {
        
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "CardI:");
        Type?.Show(space + 2);
        Name?.Show(space + 2);
        Faction?.Show(space + 2);
        Power?.Show(space + 2);
        Range?.Show(space + 2);
        OnActivation?.Show(space + 2);
    }
}

public class Name : Node
{
    public Expression name;

    public Name (Expression expression)
    {
        name = expression;
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Name:");
        name.Show(space + 2);
    }
}

public class Type : Node
{
    public Expression type;

    public Type (Expression expression)
    {
        type = expression;
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Type:");
        type.Show(space + 2);
    }
}

public class Faction : Node
{
    public Expression faction;
    public Faction(Expression expression)
    {
        faction = expression;
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Faction:");
        faction.Show(space + 2);
    }
}

public class Power : Node
{
    public Expression power;

    public Power(Expression expression)
    {
        power = expression;
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Power:");
        power.Show(space + 2);
    }
}

public class PowerAsField : Node
{
    public PowerAsField()
    {

    }

    public void Show(int space) //Checkear
    {

    }
}

public class Range : Node
{
    public Expression[] range;
    string Lexeme;

    public Range(Expression[] expressions)
    {
        range = expressions;
    }
    public Range(string lexeme)
    {
        Lexeme = lexeme;
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Range:");
        if(range != null)
        {
            foreach(var expr in range)
            {
                expr.Show(space + 2);
            }
        }
        else
        {
            Debug.Log(new string(' ', space) + "Lexeme: " + Lexeme);
        }
    }
}

public class OnActivation : Node
{
    public List<OnActivationElements> Elements;
    public OnActivation()
    {
        Elements = new List<OnActivationElements>();   
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "OnActivation:");
        foreach (var element in Elements)
        {
            element.Show(space + 2);
        }
    }
}

public class OnActivationElements : Node
{
    public OAEffect OAEffect;
    public Selector Selector;
    public List<PostAction> postAction;
    public OnActivationElements(OAEffect oaEffect, Selector selector, List<PostAction> PostActions)
    {
        OAEffect = oaEffect;
        Selector = selector;
        postAction = PostActions;
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "OnActivationElements:");
        OAEffect?.Show(space + 2);
        Selector?.Show(space + 2);
        foreach(var postAction in postAction)
        postAction?.Show(space + 2);
    }
}

public class OAEffect : Node
{
    public string Name;
    public List<Assignment> Assingments;
    public OAEffect(string name, List<Assignment> assingments)
    {
        Name = name;
        Assingments = assingments;
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "OAEffect:");
        Debug.Log(new string(' ', space + 2) + "Name: " + Name);
        foreach (var assignment in Assingments)
        {
            assignment.Show(space + 2);
        }
    }
}
public class Effect : Node
{
    public Name Name;
    public Args Params;
    public Action Action;
    public Effect()
    {
       
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Effect:");
        Name?.Show(space + 2);
        Params?.Show(space + 2);
        Action?.Show(space + 2);
    }
}
public class Action : Node
{
    public Variable Targets;
    public Variable Context;
    public StatementBlock Block;
    public Action(Variable targets,Variable context,StatementBlock block)
    {
        Targets = targets;
        Context = context;
        Block = block;
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Action:");
        Targets?.Show(space + 2);
        Context?.Show(space + 2);
        Block?.Show(space + 2);
    }
}
public class Single : Node
{
    public bool Value;
    public Single(Token token)
    {
        if(token.Type == TokenType.TRUE)
        {
           Value = true;
        }
        else if(token.Type == TokenType.FALSE) Value = false;
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Single: " + Value);
    }
}
public class Selector : Node
{
    public string Source;
    public Single Single;
    public Predicate Predicate;
    public Selector(string source,Single single,Predicate predicate)
    {
        Source = source;
        Single = single;
        Predicate = predicate;
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Selector:");
        Debug.Log(new string(' ', space + 2) + "Source: " + Source);
        Single?.Show(space + 2);
        Predicate?.Show(space + 2);
    }
}
public class Predicate : Node
{
    public Variable Var;
    public Expression Condition;
    public Predicate(Variable var,Expression condition)
    {
        Var = var;
        Condition = condition;
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Predicate:");
        Var?.Show(space + 2);
        Condition?.Show(space + 2);
    }
}

public class PostAction : Node
{
    public Expression Type;
    public List<Assignment> Assignments;
    public Selector Selector;
    public PostAction(Expression type,Selector selector)
    {
        Type = type;
        Selector = selector;
        Assignments = new List<Assignment>();
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "PostAction:");
        Type?.Show(space + 2);
        Selector?.Show(space + 2);
    }
}
public abstract class Expression : Node
{
    public abstract void Show(int space=0);
    public abstract object Evaluate(Semantic semantic, GameManager gameManager);
}
public class Binary : Expression
{
    public Expression Left;
    public Token Operators;
    public Expression Right;
    public Binary(Expression left,Token operators,Expression right)
    {
        Left = left;
        Operators = operators;
        Right = right;
    }
    public override object Evaluate(Semantic semantic, GameManager gameManager)
    {
        object leftValue = Left.Evaluate(semantic, gameManager);
        if(leftValue is Card card)
        {
            if(Left is VariableComp variableComp)
            {
                if(variableComp.args.Arguments[0] is PowerAsField)
                {leftValue = card.power;
                UnityEngine.Debug.Log("la lista era en 0?");
                UnityEngine.Debug.Log(card.power);
                }
                if(variableComp.args.Arguments[0] is Faction)
                {
                    leftValue = card.faction;
                    UnityEngine.Debug.Log(card.faction);
                }
            }
        }
        object rightValue = Right.Evaluate(semantic, gameManager);
        if(leftValue is double && rightValue is double|| leftValue is int && rightValue is int|| leftValue is int && rightValue is Double || leftValue is Double && rightValue is int)
        {
            switch (Operators.Type)
            {
                case TokenType.PLUS:
                    return System.Convert.ToDouble(leftValue) + System.Convert.ToDouble(rightValue);    
                case TokenType.MINUS:
                    return System.Convert.ToDouble(leftValue) - System.Convert.ToDouble(rightValue);
                case TokenType.STAR:
                    return System.Convert.ToDouble(leftValue) * System.Convert.ToDouble(rightValue);
                case TokenType.SLASH:
                    return System.Convert.ToDouble(leftValue) / System.Convert.ToDouble(rightValue);
                case TokenType.PERCENT:
                    return System.Convert.ToDouble(leftValue) % System.Convert.ToDouble(rightValue);
                case TokenType.POW:
                    return System.Math.Pow(System.Convert.ToDouble(leftValue),System.Convert.ToDouble(rightValue));
                case TokenType.GREATER:
                    return System.Convert.ToDouble(leftValue) > System.Convert.ToDouble(rightValue);
                case TokenType.GREATER_EQUAL:
                    return System.Convert.ToDouble(leftValue) >= System.Convert.ToDouble(rightValue);
                case TokenType.LESS:
                    return System.Convert.ToDouble(leftValue) < System.Convert.ToDouble(rightValue);
                case TokenType.LESS_EQUAL:
                    return System.Convert.ToDouble(leftValue) <= System.Convert.ToDouble(rightValue);
                case TokenType.BANG_EQUAL: return !leftValue.Equals(rightValue);
                case TokenType.EQUAL_EQUAL: return leftValue.Equals(rightValue);
                default:
                throw new System.InvalidOperationException("Unsupported operator: " + Operators.Lexeme);
            }
        }
        else if(leftValue is string && rightValue is string)
        {
            switch (Operators.Type)
            { 
                case TokenType.EQUAL_EQUAL : return leftValue.Equals(rightValue);
                case TokenType.ATSIGN : return leftValue.ToString() + rightValue.ToString();
                case TokenType.ATSIGN_ATSIGN : return leftValue.ToString() + " " + rightValue.ToString();
                default:
                throw new System.InvalidOperationException("Unsupported operator: " + Operators.Lexeme);
            }
        }
        
        else 
        {
           UnityEngine.Debug.Log($"{leftValue} y tiene tipo {leftValue.GetType()}");
           UnityEngine.Debug.Log($"{rightValue} y tiene tipo {rightValue.GetType()}");
            throw new System.InvalidOperationException("Unsupported operator: " + Operators.Lexeme);
    }}
    public override void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "BinaryOperator: " + Operators.Lexeme);
        Left.Show(space + 2);
        Right.Show(space + 2);
    }
}
public class BinaryBooleanExpression : Binary
{
    public BinaryBooleanExpression(Expression left, Token operators, Expression right) : base(left, operators, right)
    {}
}

public class BinaryIntergerExpression : Binary
{
    public BinaryIntergerExpression(Expression left, Token operators, Expression right) : base(left, operators, right)
    {}
}

public class BinaryStringExpression : Binary
{
    public BinaryStringExpression(Expression left, Token operators, Expression right) : base(left, operators, right)
    {}
}

public class Unary : Expression
{
    public Token Operators;
    public Expression Right;
    public Unary(Token operators,Expression right)
    {
        Operators = operators;
        Right = right;
    }
    public override object Evaluate(Semantic semantic, GameManager gameManager)
    {
        object rightValue = Right.Evaluate(semantic, gameManager);

        switch (Operators.Type)
        {
            case TokenType.MINUS:
                return -System.Convert.ToDouble(rightValue);
            case TokenType.BANG:
                return !(bool)rightValue;
            case TokenType.PLUS_PLUS:
                return System.Convert.ToDouble(rightValue) +1;
            
            default:
                UnityEngine.Debug.Log($"{rightValue} y de tipo {rightValue.GetType()}");
                throw new System.InvalidOperationException("Unsupported operator: " + Operators.Lexeme);
        }
    }
    
    public override void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Unary: " + Operators.Lexeme);
        Right.Show(space + 2);
    }
}

public class UnaryBooleanExpression : Unary
{
    public UnaryBooleanExpression(Token operators, Expression right) : base(operators,right){}
}

public class UnaryIntergerExpression : Unary
{
    public UnaryIntergerExpression(Token operators, Expression right) : base(operators,right){}
}
public class Variable : Expression
{
    public Token Token{get;}
    public string Value{get;}
    public Type type;
    public enum Type
    {
        TARGETS, CONTEXT, CARDI, FIELD, INT, STRING, BOOL, VOID, NULL, EFFECT
    }
    public Variable(Token token)
    {
        this.Token = token;
        Value = token.Lexeme;
        type = Type.NULL;
    }
    public void TypeParam(TokenType tokenType)
    {
        if(tokenType == TokenType.BOOLTYPE)
        {
            type = Type.BOOL;
        }
        if(tokenType == TokenType.NUMBERTYPE)
        {
            type = Type.INT;
        }
        if(tokenType == TokenType.STRINGTYPE)
        {
            type = Type.STRING;
        }
    }

    public override object Evaluate(Semantic semantic, GameManager gameManager)
    {
        return semantic.objectVars[Value];
    }

    public override void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Variable: " + Value + " (" + type.ToString() + ")");
    }
}

public class VariableComp : Variable,Statement
{
    public Args args;

    public VariableComp(Token token) : base(token)
    {
        args = new Args();
    }
    
    public override void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "VariableComp: " + Value);
        args?.Show(space + 2);
    }
    public  object EvaluateVC(Semantic semantic, GameManager gameManager)
    
    {
        object last = null;
        foreach(var arg in args.Arguments)
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
                    // case Type: last = card.tipo;break;
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
    public void Ejecuta(Semantic semantic, GameManager gameManager)
    {
        object last = null;
        if(semantic.objectVars.ContainsKey(Value)) last = semantic.objectVars[Value];
        foreach(var arg in args.Arguments)
        {
            if(arg is FunctionDeclaration)
            {
                last = (arg as FunctionDeclaration).GetValue(semantic,gameManager,last);
            }
            else if(arg is Pointer)
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
        }
    }
    public void AssignValue(Semantic semantic, GameManager gameManager, System.Object value)
    {
        object last = null;
        if(Value == "target")
        {
            last = semantic.objectVars[Value];
        }
        foreach(var arg in args.Arguments)
        {
            if(arg is FunctionDeclaration)
            {
                last = (arg as FunctionDeclaration).GetValue(semantic,gameManager,last);
            }
            
            else if(arg is Pointer)
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
                GameObject card = last as GameObject;
                switch(arg)
                {
                    // case Type: card.GetComponent<CardDisplay>().tipo = value as string;break;
                    case Name: card.GetComponent<CardDisplay>().nameText.text = value as string;break;
                    case Faction: card.GetComponent<CardDisplay>().faction = value as string;break;
                    case PowerAsField: card.GetComponent<CardDisplay>().power = Convert.ToInt32(value);
                    break;
                    // case Range: last = card.GetComponent<CardDisplay>().range;break;
                }
            }
        }
    }
    
}
public class Number : Expression
{
    public int Value;
    public Number(int value)
    {
        Value = value;
    }

    public override object Evaluate(Semantic semantic, GameManager gameManager)
    {
        return Value;
    }

    public override void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Number: " + Value);
    }
}

public class String : Expression
{
    public string Value;
    public String(string value)
    {
        Value = value;
    }

    public override object Evaluate(Semantic semantic, GameManager gameManager)
    {
        return Value;
    }

    public override void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "String: " + Value);
    }
}

public class Bool : Expression
{
    public bool Value;
    public Bool(bool value)
    {
        Value = value;
    }

    public override object Evaluate(Semantic semantic, GameManager gameManager)
    {
        return Value;
    }

    public override void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Bool: " + Value);
    }
}
public class ExpressionGroup : Expression
{
    public Expression Exp;
    public ExpressionGroup(Expression expression)
    {
        Exp = expression;
    }
    public override object Evaluate(Semantic semantic, GameManager gameManager)
    {
        return Exp.Evaluate(semantic, gameManager);
    }
    public override void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "ExpressionGroup:");
        Exp.Show(space + 2);
    }
}



public class Args : Node
{
    public List<Node> Arguments;
    public Args()
    {
        Arguments = new List<Node>();
    }

    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Args:");
        foreach (var arg in Arguments)
        {
            arg.Show(space + 2);
        }
    }
}

public interface Statement : Node
{
    public void Ejecuta(Semantic semantic, GameManager gameManager);
}

public class StatementBlock : Node
{
    public List<Statement> statements;

    public StatementBlock()
    {
        statements = new List<Statement>();
    }

    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "StatementBlock:");
        foreach (var Statement in statements)
        {
            Statement.Show(space + 2);
        }
    }
}
public class WhileStatement : Statement
{
    public Expression Condition;
    public StatementBlock Body;
    public WhileStatement(Expression condition,StatementBlock body)
    {
        Condition = condition;
        Body = body;
    }
    public  void Ejecuta(Semantic semantic, GameManager gameManager)
    {
        while((bool)Condition.Evaluate(semantic, gameManager))
        {
            foreach(var stmt in Body.statements)
            {
                stmt.Ejecuta(semantic, gameManager);
            }
        }
    }
    
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "WhileStatement:");
        Debug.Log(new string(' ', space + 2) + "Condition:");
        Condition?.Show(space + 2);
        Debug.Log(new string(' ', space + 2) + "Body:");
        Body?.Show(space + 2);
    }
}

public class ForStatement : Statement
{
    public Variable Target;
    public Variable Targets;
    public StatementBlock Body;
    public ForStatement(Variable target, Variable targets, StatementBlock body)
    {
        Target = target;
        Targets = targets;
        Body = body;
    }

    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "ForStatement:");
        Debug.Log(new string(' ', space + 2) + "Target:");
        Target?.Show(space + 2);
        Debug.Log(new string(' ', space + 2) + "Targets:");
        Targets?.Show(space + 2);
        Debug.Log(new string(' ', space + 2) + "Body:");
        Body?.Show(space + 2);
    }
    public void Ejecuta(Semantic semantic, GameManager gameManager)
    {
        foreach(GameObject target in semantic.objectVars["targets"] as List<GameObject>)
        {
            semantic.objectVars["target"] = target;
            foreach(var stmt in Body.statements)
            {
                stmt.Ejecuta(semantic,gameManager);
            }
            semantic.objectVars.Remove("target");
        }
    }
}


public class Assignment : Statement
{
    public Variable Left;
    public Token Op;
    public Expression Right;
    public Assignment(Variable left, Token op, Expression right)
    {
        Left = left;
        Op = op;
        Right = right;
    }
     public void Ejecuta(Semantic semantic, GameManager gameManager)
     {
        if(Op.Type == TokenType.EQUAL)
        {
            if(Left is VariableComp)
            {
                (Left as VariableComp).AssignValue(semantic,gameManager,Right.Evaluate(semantic,gameManager));
            }
            else
            {
                semantic.objectVars[Left.Value] = Right.Evaluate(semantic,gameManager);
            }
        }
        else if(Op.Type == TokenType.PLUS_EQUALS)
        {
            if(Left is VariableComp)
            {
                (Left as VariableComp).AssignValue(semantic,gameManager,Convert.ToInt32(Left.Evaluate(semantic,gameManager))+Convert.ToInt32(Right.Evaluate(semantic,gameManager)));
            }
            else
            {
                int result = Convert.ToInt32(semantic.objectVars[Left.Value]);
                result += Convert.ToInt32(Right.Evaluate(semantic,gameManager));
                semantic.objectVars[Left.Value] = result;
            }
        }
        else if(Op.Type == TokenType.MINUS_EQUALS)
        {
            if(Left is VariableComp)
            {
                (Left as VariableComp).AssignValue(semantic,gameManager,Convert.ToInt32(Left.Evaluate(semantic, gameManager))-Convert.ToInt32(Right.Evaluate(semantic, gameManager)));
            }
            else
            {
                int result = Convert.ToInt32(semantic.objectVars[Left.Value]);
                result -= Convert.ToInt32(Right.Evaluate(semantic,gameManager));
                semantic.objectVars[Left.Value] = result;
            }
        }
     }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Assignment:");
        Left?.Show(space + 2);
        Debug.Log(new string(' ', space + 2) + "Op: " + Op.Lexeme);
        Right?.Show(space + 2);
    }
}


public class FunctionDeclaration : Statement
{
    public string FunctionName;
    public Args Args;
    public Variable.Type Type;
    public FunctionDeclaration(string functionName,Args args)
    {
        FunctionName = functionName;
        Args = args;
        Type = Variable.Type.NULL;
        TypeToReturn();
    }
    public void Ejecuta(Semantic semantic, GameManager gameManager)
    {
        throw new NotImplementedException();
    }

    public void TypeToReturn()
    {
        if (FunctionName == "FieldOfPlayer") Type = Variable.Type.CONTEXT;
        if (FunctionName == "HandOfPlayer") Type = Variable.Type.FIELD;
        if (FunctionName == "GraveyardOfPlayer") Type = Variable.Type.FIELD;
        if (FunctionName == "DeckOfPlayer") Type = Variable.Type.FIELD;
        if (FunctionName == "Find") Type = Variable.Type.TARGETS;
        if (FunctionName == "Push") Type = Variable.Type.VOID;
        if (FunctionName == "SendBottom") Type = Variable.Type.VOID;
        if (FunctionName == "Pop") Type = Variable.Type.CARDI;
        if (FunctionName == "Remove") Type = Variable.Type.VOID;
        if (FunctionName == "Shuffle") Type = Variable.Type.VOID;
        if (FunctionName == "Add") Type = Variable.Type.VOID;
    }
    public object GetValue(Semantic semantic, GameManager gameManager,System.Object value)
    {
        switch(FunctionName)
        {
            case "TriggerPlayer": return gameManager.TriggerPlayer();
            case "HandOfPlayer": if(Args.Arguments[0] is FunctionDeclaration) return gameManager.HandOfPlayer(Convert.ToInt32((Args.Arguments[0] as FunctionDeclaration).GetValue(semantic, gameManager,value)));
            else return gameManager.HandOfPlayer(Convert.ToInt32((Args.Arguments[0] as Expression).Evaluate(semantic,gameManager)));
            case "DeckOfPlayer": if(Args.Arguments[0] is FunctionDeclaration) return gameManager.DeckofPlayer(Convert.ToInt32((Args.Arguments[0] as FunctionDeclaration).GetValue(semantic, gameManager,value)));
            else return gameManager.DeckofPlayer(Convert.ToInt32((Args.Arguments[0] as Expression).Evaluate(semantic,gameManager)));
            case "GraveyardOfPlayer": if(Args.Arguments[0] is FunctionDeclaration) return gameManager.GraveyardOfPlayer(Convert.ToInt32((Args.Arguments[0] as FunctionDeclaration).GetValue(semantic, gameManager,value)));
            else return gameManager.GraveyardOfPlayer(Convert.ToInt32((Args.Arguments[0] as Expression).Evaluate(semantic,gameManager)));
            case "FieldOfPlayer": if(Args.Arguments[0] is FunctionDeclaration) return gameManager.FieldOfPlayer(Convert.ToInt32((Args.Arguments[0] as FunctionDeclaration).GetValue(semantic, gameManager,value)));
            else return gameManager.FieldOfPlayer(Convert.ToInt32((Args.Arguments[0] as Expression).Evaluate(semantic,gameManager)));
            // case "Find": (value as CardList).Find(Args.Arguments[0] as Predicate);return null;
            // case "Push": (value as CardList).Push((Args.Arguments[0] as Expression).Evaluate(semantic,gameManager) as GameObject);return null;
            // case "SendBottom": (value as CardList).SendBottom((Args.Arguments[0] as Expression).Evaluate(semantic,gameManager) as GameObject);return null;
            // case "Pop": return (value as CardList).Pop();
            // case "Remove": (value as CardList).Remove((Args.Arguments[0] as Expression).Evaluate(semantic,gameManager) as GameObject);return null;
            // case "Shuffle": (value as CardList).Shuffle();return null; 
            default: return null;
        }
    }
    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "FunctionDeclaration:");
        Debug.Log(new string(' ', space + 2) + "FunctionDeclarationName: " + FunctionName);
        Args?.Show(space + 2);
        Debug.Log(new string(' ', space + 2) + "Return Type: " + Type.ToString());
    }
}

public class Pointer : Node
{
    public string pointer;
    public Pointer(string pointer)
    {
        this.pointer = pointer;
    }

    public void Show(int space = 0)
    {
        Debug.Log(new string(' ', space) + "Pointer: " + pointer);
    }
}
