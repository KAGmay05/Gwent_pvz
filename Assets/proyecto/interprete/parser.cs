using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;

public class Parser 
{
    private List<Token> tokens = new List<Token>();
    private int current = 0;

    public Parser (List<Token> Tokens)
    {
      Tokens = tokens;
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
      TokenType[] types = {TokenType.BANG_EQUAL, TokenType.EQUAL_EQUAL};
      while (Match(types))
      {
        Token operator = Previous();
        Expression right = Comparison();
        expr = new Expression.Binary(expr, operator, right )
      }
      return expr;
    }
    private bool Match(TokenType[] type)
    {
      for(int i =0; i<type.Length;i++)
      {
      if(Check(type[i]))
      {
        Advance();
        return true;
      }
      }
      return false;
    }
    bool Check(TokenType type)
    {
      if(IsAtEnd()) return false;
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
      if(!IsAtEnd()) current ++;
      return Previous();
    }
    Token Previous()
    {
      return tokens[current - 1];
    }
    Expression Comparison()
    {
      Expression expr = Term()
    }
    Expression Term()
    {
      Expression expr = TaskFactory
    }
    Expression Unary()
    {
      TokenType[] types = {TokenType.BANG, TokenType.MINUS};
      if (Match(types))
      {
        Token operator = Previous();
        Expression right = Unary();
        return new Expression.Unary(operator, right);
      }
      return Primary();
    }
    private Expression Factor()
    {
      Expression expr = Unary();
      TokenType[] type = {TokenType.SALSH, TokenType.STAR};
      while(Match(type))
      {
        Token operator = Previous();
        Expression right = Unary();
        expr = new Expression.Binary()
      }
    }
    Expression Primary()
    {
      TokenType[] falsetype = {TokenType.FALSE};
      TokenType[] truetype = {TokenType.TRUE};
      TokenType[] numberString = {TokenType.NUMBER,TokenType.STRING };
      
      if(Match(falsetype)) return new Expression.Literal(false);
      if(Match(truetype)) return new Expression.Literal(true);
      if(Match(numberString))
      {
        return new Expression.Literal(Previous().literal);
      }
      TokenType[] paren = {TokenType.LEFT_PAREN};
      if (Match(paren))
      {
        Expression = Expression();
        Consume(TokenType.RIGHT_PAREN, "Expect ')' after expression.");
        return new Expression.Grouping(expr);
      }
    }
}