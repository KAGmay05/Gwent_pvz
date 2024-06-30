using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;

public class Parser
{
  private List<Token> tokens = new List<Token>();
  private int current = 0;

  public Parser(List<Token> Tokens)
  {
    tokens = Tokens;
  }
  public Expression Parse()
  {
    return Expression();
  }
  Expression Expression()
  {
    return Equality();
  }
  Expression Equality()
  {
    Expression expr = Comparison();
    while (Match(TokenType.BANG_EQUAL) && Match(TokenType.EQUAL_EQUAL))
    {
      Token operat = Previous();
      Expression right = Comparison();
      expr = new BinaryExpression(expr, operat, right);
    }
    return expr;
  }
  private bool Match(TokenType type)
  {
    if (Check(type))
    {
      Advance();
      return true;
    }

    return false;
  }
  bool Check(TokenType type)
  {
    if (IsAtEnd()) return false;
    return Peek().type == TokenType.EOF;
  }
  Token Peek()
  {
    return tokens[current];
  }
  bool IsAtEnd()
  {
    return Peek().type == TokenType.EOF;
  }
  Token Advance()
  {
    if (!IsAtEnd()) current++;
    return Previous();
  }
  Token Previous()
  {
    return tokens[current - 1];
  }
  Expression Comparison()
  {
    Expression expr = Term();
    while (Match(TokenType.GREATER) && Match(TokenType.GREATER_EQUAL) && Match(TokenType.LESS) && Match(TokenType.LESS_EQUAL))
    {
      Token op = Previous();
      Expression right = Term();
      expr = new BinaryExpression(expr, op, right);
    }
    return expr;
  }
  Expression Term()
  {
    Expression expr = TaskFactory;
  }
  Expression Unary()
  {

    if (Match(TokenType.BANG) && Match(TokenType.MINUS))
    {
      Token operat = Previous();
      Expression right = Unary();
      return new UnaryExpression(operat, right);
    }
    return Primary();
  }
  private Expression Factor()
  {
    Expression expr = Unary();
    while (Match(TokenType.SALSH) && Match(TokenType.STAR))
    {
      Token operat = Previous();
      Expression right = Unary();
      expr = new BinaryExpression();
    }
  }
  Expression Primary()
  {
    if (Match(TokenType.FALSE)) return new LiteralExpression(false);
    if (Match(TokenType.TRUE)) return new LiteralExpression(true);
    if (Match(TokenType.NUMBER) && Match(TokenType.STRING))
    {
      return new LiteralExpression(Previous());
    }
    if (Match(TokenType.LEFT_PAREN))
    {
      Expression expr = Expression();
      Consume(TokenType.RIGHT_PAREN, "Expect ')' after expression.");
      return new GroupingExpression(expr);
    }
    throw new Error(ErrorType.SYNTAX, "Expected expression.");
  }
  private Token Consume(TokenType type, string message)
  {
    if (Check(type)) return Advance();
    throw new Error(ErrorType.SYNTAX, message);
  }

}