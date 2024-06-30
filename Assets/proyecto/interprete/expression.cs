using System.Collections.Generic;
abstract class Expression
{

}

public class BinaryExpression : Expression
{
    public Expression Left { get; }
    public Token Operator { get; }
    public Expression Right { get; }
    public BinaryExpression(Expression left, Token op, Expression right)
    {
        Left = left;
        Operator = op;
        Right = right;
    }
}
public class UnaryExpression : Expression
{
    public Token Operator { get; }
    public Expression Right { get; }
    public UnaryExpression(Token op, Expression right)
    {
        Operator = op;
        Right = right;
    }
}
public class LiteralExpression : Expression
{
    public object Value { get; }
    public LiteralExpression(object value)
    {
        Value = value;
    }
}
public class GroupingExpression : Expression
{
    public Expression Expression { get; }
    public GroupingExpression(Expression expression)
    {
        Expression = expression;
    }
}
public class AssignExpression : Expression
{
    public Token ID { get; }
    public Expression Value { get; }
    public AssignExpression(Token id, Expression value)
    {
        ID = id;
        Value = value;
    }

}
public class VariableExpression : Expression
{
    public Token ID { get; }
    public VariableExpression(Token id)
    {
        ID = id;
    }
}
public class IfElseStatement : Expression
{
    public Expression Condition { get; }
    public Expression ThenBranch { get; }
    public Expression ElseBranch { get; }
    public IfElseStatement(Expression condition, Expression thenbranch, Expression elseBranch)
    {
        Condition = condition;
        ThenBranch = thenbranch;
        ElseBranch = elseBranch;
    }
}
public class LetInExpression : Expression
{
    public List<AssignExpression> Assignments { get; }
    public Expression Body { get; }
    public LetInExpression(List<AssignExpression> assignments, Expression body)
    {
        Assignments = assignments;
        Body = body;
    }
}
public class FunctionDeclaration : Expression
{
    public string Identifier { get; }
    public List<VariableExpression> Arguments { get; }
    public Expression Body { get; }
    public FunctionDeclaration(string identifier, List<VariableExpression> arguments, Expression body)
    {
        Identifier = identifier;
        Arguments = arguments;
        Body = body;
    }
}
public class FunctionCall : Expression
{
    public string Identifier { get; }
    public List<Expression> Arguments { get; }
    public FunctionCall(string identifier, List<Expression> arguments)
    {
        Identifier = identifier;
        Arguments = arguments;
    }
}
public class Name : Expression
{
    public string Name;
    public Name(string name)
    {
        Name = name;
    }
}
public class Type : Expression
{
    public string Type;
    public Type(string type)
    {
        Type = type;
    }
}
public class Faction : Expression
{
    public string Faction;
    public Faction(string faction)
    {
        Faction = faction;
    }
}
public class Power : Expression
{
    public int Power;
    public Power(int power)
    {
        Power = power;
    }
}
public class Range : Expression
{
    public string Range;
    public Range(string range)
    {
        Range = range;
    }
}
public class Compound : Expression
{
    public List<Expression> Children;
    public Compound()
    {
        Children = new List<Expression>();
    }
    public bool ValidCard()
    {
        Dictionary<TokenType, int> check = new Dictionary<TokenType, int>();
        foreach (Expression node in Children)
        {
            if (node.GetType() == typeof(Name)) check[TokenType.NAME]++;
            if (node.GetType() == typeof(Type)) check[TokenType.TYPE]++;
            if (node.GetType() == typeof(Faction)) check[TokenType.FACTION]++;
            if (node.GetType() == typeof(Power)) check[TokenType.POWER]++;
            if (node.GetType() == typeof(Range)) check[TokenType.RANGE]++;
            if (node.GetType() == typeof(Compound)) check[TokenType.ONACTIVATION]++;
        }
        if (Children.Count != 6) return false;
        if (check.ContainsKey(TokenType.NAME) && check[TokenType.NAME] != 1) return false;
        if (check.ContainsKey(TokenType.TYPE) && check[TokenType.TYPE] != 1) return false;
        if (check.ContainsKey(TokenType.FACTION) && check[TokenType.FACTION] != 1) return false;
        if (check.ContainsKey(TokenType.POWER) && check[TokenType.POWER] != 1) return false;
        if (check.ContainsKey(TokenType.RANGE) && check[TokenType.RANGE] != 1) return false;
        if (check.ContainsKey(TokenType.ONACTIVATION) && check[TokenType.ONACTIVATION] != 1) return false;
        return true;
    }
    public bool ValidEffect()
    {
        Dictionary<TokenType, int> check = new Dictionary<TokenType, int>();
        foreach (Expression node in Children)
        {
            if (node.GetType() == typeof(Name)) check[TokenType.NAME]++;
            if (node.GetType() == typeof(Compound) && !check.ContainsKey(TokenType.PARAMS)) check[Token.Type.PARAMS]++;
            if (node.GetType() == typeof(Compound) && !check.ContainsKey(TokenType.ACTION)) check[Token.Type.ACTION]++;
        }
        if (Children.Count > 3) return false;
        if (check.ContainsKey(TokenType.NAME) && check[TokenType.NAME] != 1) return false;
        if (check.ContainsKey(TokenType.PARAMS) && check[TokenType.PARAMS] != 1) return false;
        if (check.ContainsKey(TokenType.ACTION) && check[TokenType.ACTION] != 1) return false;
        return true;
    }
}
