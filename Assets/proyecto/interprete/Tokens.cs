using System.Text.Json;

public class Token
{
    public TokenType type {get;private set;}
    public string lexeme {get;private set;}
    public int line {get; private set;}
    public int column {get;private set;}

     public Token (TokenType Type, string Lexeme, int Column, int Line)
    {
     type = Type;
     lexeme = Lexeme;
     column = Column;
     line = Line;
    }

}
public enum TokenType
{
    // single-character tokens
    LEFT_PAREN, RIGHT_PAREN, LEFT_BRACE, RIGHT_BRACE, LEFT_BRACKET, RIGHT_BRACKET,
    COMMA, DOT, COLON, SEMICOLON,SALSH, STAR, 

    // ONE OR TWO CHARACTER TOKENS
    BANG, BANG_EQUAL, EQUAL, EQUAL_EQUAL,
    GREATER, GREATER_EQUAL, LESS, LESS_EQUAL,
    MINUS, MINUS_MINUS, PLUS, PLUS_PLUS,
    PLUS_EQUAL, MINUS_EQUAL, EQUAL_GREATER,


    // LITERALS
    IDENTIFIER, STRING, NUMBER,
    // KEYWORRDS
    AND,FOR,FALSE,TRUE,WHILE,OR, 
    EFFECT, CARD, POSTACTION, PREDICATE, IN,
    NAME, PARAMS, ACTION,TYPE,FACTION,POWER,RANGE,
    ONACTIVATION,FUN,SELECTOR,SINGLE,POINTER,METHOD,STRINGTYPE,NUMBERTYPE,BOOLTYPE,
    SOURCE,

    EOF
}
