public interface Node
{
    public void Show(int space = 0);
}

public class Program : Node
{
    public List<Card> Cards;
    public List<Effect> Effects;
    public Program()
    {
        Cards = new List<Card>();
        Effects = new List<Effect>();
    }
    public void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "Program:");
        foreach (var card in Cards)
        {
            card.Show(space + 2);
        }
        foreach (var effect in Effects)
        {
            effect.Show(space + 2);
        }
    }
}

public class Card : Node
{
    public Type Type;
    public Name Name;
    public Faction Faction;
    public Power Power;
    public Range Range;
    public OnActivation OnActivation;
    public Card()
    {
        
    }
    public void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "Card:");
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
        Console.WriteLine(new string(' ', space) + "Name:");
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
        Console.WriteLine(new string(' ', space) + "Type:");
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
        Console.WriteLine(new string(' ', space) + "Faction:");
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
        Console.WriteLine(new string(' ', space) + "Power:");
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
        Console.WriteLine(new string(' ', space) + "Range:");
        if(range != null)
        {
            foreach(var expr in range)
            {
                expr.Show(space + 2);
            }
        }
        else
        {
            Console.WriteLine(new string(' ', space) + "Lexeme: " + Lexeme);
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
        Console.WriteLine(new string(' ', space) + "OnActivation:");
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
    public PostAction postAction;
    public OnActivationElements(OAEffect oaEffect, Selector selector, PostAction pA)
    {
        OAEffect = oaEffect;
        Selector = selector;
        postAction = pA;
    }
    public void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "OnActivationElements:");
        OAEffect?.Show(space + 2);
        Selector?.Show(space + 2);
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
        Console.WriteLine(new string(' ', space) + "OAEffect:");
        Console.WriteLine(new string(' ', space + 2) + "Name: " + Name);
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
        Console.WriteLine(new string(' ', space) + "Effect:");
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
        Console.WriteLine(new string(' ', space) + "Action:");
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
        if(token.Type == TokenType.BOOL)
        {
            if(token.Lexeme == "true")  Value = true;
            else Value = false;
        }
    }
    public void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "Single: " + Value);
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
        Console.WriteLine(new string(' ', space) + "Selector:");
        Console.WriteLine(new string(' ', space + 2) + "Source: " + Source);
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
        Console.WriteLine(new string(' ', space) + "Predicate:");
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
        Console.WriteLine(new string(' ', space) + "PostAction:");
        Type?.Show(space + 2);
        Selector?.Show(space + 2);
    }
}
public abstract class Expression : Node
{
    public abstract void Show(int space=0);
    public abstract object Evaluate();
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
    public override object Evaluate()
    {
        object leftValue = Left.Evaluate();
        object rightValue = Right.Evaluate();
        if(leftValue is double && rightValue is double)
        {
            switch (Operators.Type)
            {
                case TokenType.PLUS:
                    return Convert.ToDouble(leftValue) + Convert.ToDouble(rightValue);    
                case TokenType.MINUS:
                    return Convert.ToDouble(leftValue) - Convert.ToDouble(rightValue);
                case TokenType.STAR:
                    return Convert.ToDouble(leftValue) * Convert.ToDouble(rightValue);
                case TokenType.SLASH:
                    return Convert.ToDouble(leftValue) / Convert.ToDouble(rightValue);
                case TokenType.PERCENT:
                    return Convert.ToDouble(leftValue) % Convert.ToDouble(rightValue);
                case TokenType.POW:
                    return Math.Pow(Convert.ToDouble(leftValue),Convert.ToDouble(rightValue));
                case TokenType.GREATER:
                    return Convert.ToDouble(leftValue) > Convert.ToDouble(rightValue);
                case TokenType.GREATER_EQUAL:
                    return Convert.ToDouble(leftValue) >= Convert.ToDouble(rightValue);
                case TokenType.LESS:
                    return Convert.ToDouble(leftValue) < Convert.ToDouble(rightValue);
                case TokenType.LESS_EQUAL:
                    return Convert.ToDouble(leftValue) <= Convert.ToDouble(rightValue);
                case TokenType.BANG_EQUAL: return !leftValue.Equals(rightValue);
                case TokenType.EQUAL_EQUAL: return leftValue.Equals(rightValue);
                default:
                throw new InvalidOperationException("Unsupported operator: " + Operators.Lexeme);
            }
        }
        else if(leftValue is string && rightValue is string)
        {
            switch (Operators.Type)
            {
                case TokenType.ATSIGN : return leftValue.ToString() + rightValue.ToString();
                case TokenType.ATSIGN_ATSIGN : return leftValue.ToString() + " " + rightValue.ToString();
                default:
                throw new InvalidOperationException("Unsupported operator: " + Operators.Lexeme);
            }
        }
        else throw new InvalidOperationException("Unsupported operator: " + Operators.Lexeme);
    }
    public override void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "BinaryOperator: " + Operators.Lexeme);
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
    public override object Evaluate()
    {
        object rightValue = Right.Evaluate();

        switch (Operators.Type)
        {
            case TokenType.MINUS:
                return -Convert.ToDouble(rightValue);
            case TokenType.BANG:
                return !(bool)rightValue;
            default:
                throw new InvalidOperationException("Unsupported operator: " + Operators.Lexeme);
        }
    }
    
    public override void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "Unary: " + Operators.Lexeme);
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
        TARGETS, CONTEXT, CARD, FIELD, INT, STRING, BOOL, VOID, NULL, EFFECT
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

    public override object Evaluate()
    {
        throw new NotImplementedException();
    }

    public override void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "Variable: " + Value + " (" + type.ToString() + ")");
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
        Console.WriteLine(new string(' ', space) + "VariableComp: " + Value);
        args?.Show(space + 2);
    }
}
public class Number : Expression
{
    public int Value;
    public Number(int value)
    {
        Value = value;
    }

    public override object Evaluate()
    {
        return Value;
    }

    public override void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "Number: " + Value);
    }
}

public class String : Expression
{
    public string Value;
    public String(string value)
    {
        Value = value;
    }

    public override object Evaluate()
    {
        return Value;
    }

    public override void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "String: " + Value);
    }
}

public class Bool : Expression
{
    public bool Value;
    public Bool(bool value)
    {
        Value = value;
    }

    public override object Evaluate()
    {
        return Value;
    }

    public override void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "Bool: " + Value);
    }
}
public class ExpressionGroup : Expression
{
    public Expression Exp;
    public ExpressionGroup(Expression expression)
    {
        Exp = expression;
    }
    public override object Evaluate()
    {
        return Exp.Evaluate();
    }
    public override void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "ExpressionGroup:");
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
        Console.WriteLine(new string(' ', space) + "Args:");
        foreach (var arg in Arguments)
        {
            arg.Show(space + 2);
        }
    }
}

public interface Statement : Node
{
    
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
        Console.WriteLine(new string(' ', space) + "StatementBlock:");
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

    public void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "WhileStatement:");
        Console.WriteLine(new string(' ', space + 2) + "Condition:");
        Condition?.Show(space + 2);
        Console.WriteLine(new string(' ', space + 2) + "Body:");
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
        Console.WriteLine(new string(' ', space) + "ForStatement:");
        Console.WriteLine(new string(' ', space + 2) + "Target:");
        Target?.Show(space + 2);
        Console.WriteLine(new string(' ', space + 2) + "Targets:");
        Targets?.Show(space + 2);
        Console.WriteLine(new string(' ', space + 2) + "Body:");
        Body?.Show(space + 2);
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

    public void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "Assignment:");
        Left?.Show(space + 2);
        Console.WriteLine(new string(' ', space + 2) + "Op: " + Op.Lexeme);
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

    public void TypeToReturn()
    {
        if (FunctionName == "FieldOfPlayer") Type = Variable.Type.CONTEXT;
        if (FunctionName == "HandOfPlayer") Type = Variable.Type.FIELD;
        if (FunctionName == "GraveyardOfPlayer") Type = Variable.Type.FIELD;
        if (FunctionName == "DeckOfPlayer") Type = Variable.Type.FIELD;
        if (FunctionName == "Find") Type = Variable.Type.TARGETS;
        if (FunctionName == "Push") Type = Variable.Type.VOID;
        if (FunctionName == "SendBottom") Type = Variable.Type.VOID;
        if (FunctionName == "Pop") Type = Variable.Type.CARD;
        if (FunctionName == "Remove") Type = Variable.Type.VOID;
        if (FunctionName == "Shuffle") Type = Variable.Type.VOID;
        if (FunctionName == "Add") Type = Variable.Type.VOID;
    }

    public void Show(int space = 0)
    {
        Console.WriteLine(new string(' ', space) + "Function:");
        Console.WriteLine(new string(' ', space + 2) + "FunctionName: " + FunctionName);
        Args?.Show(space + 2);
        Console.WriteLine(new string(' ', space + 2) + "Return Type: " + Type.ToString());
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
        Console.WriteLine(new string(' ', space) + "Pointer: " + pointer);
    }
}