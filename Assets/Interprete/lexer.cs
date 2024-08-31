using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
public class Lexer : MonoBehaviour
{
     readonly List<Token> Tokens = new List<Token>();
     static Dictionary<string,Token> keywords = new Dictionary<string, Token>();
    List<string> Errors = new List<string>();
     string Input{get;}
     int start = 0;
     int current = 0;
     int line = 1;
    public Lexer(string input, List<string>errors)
    {
        Input=input;
        Errors = errors;
        keywords["while"] = new Token(TokenType.WHILE,"while","while",0,0);
        keywords["for"] = new Token(TokenType.FOR,"for","for",0,0);
        keywords["in"] = new Token(TokenType.IN,"in","in",0,0);
        keywords["true"] = new Token(TokenType.TRUE,"true","true",0,0);
        keywords["false"] = new Token(TokenType.FALSE,"false","false",0,0);
        
        keywords["card"] = new Token(TokenType.CARD, "card", "card", 0, 0);
        keywords["effect"] = new Token(TokenType.EFFECT, "effect", "effect", 0, 0);
        keywords["Name"] = new Token(TokenType.NAME, "Name", "Name", 0, 0);
        keywords["Params"]= new Token(TokenType.PARAMS, "Params", "Params", 0, 0);
        keywords["Action"] = new Token(TokenType.ACTION, "Action", "Action", 0, 0);
        keywords["Type"] = new Token(TokenType.TYPE, "Type", "Type", 0, 0);
        keywords["Faction"] = new Token(TokenType.FACTION, "Faction", "Faction", 0, 0);
        keywords["Power"]  = new Token(TokenType.POWER, "Power", "Power", 0, 0);
        keywords["Range"] = new Token(TokenType.RANGE, "Range", "Range", 0, 0);
        keywords["OnActivation"] = new Token(TokenType.ONACTIVATION, "OnActivation", "OnActivation", 0, 0);
        keywords["Effect"] = new Token(TokenType.ONACTIVATIONEFFECT, "Effect", "Effect", 0, 0);
        keywords["Selector"] = new Token(TokenType.SELECTOR, "Selector", "Selector", 0, 0);
        keywords["Single"] = new Token(TokenType.SINGLE, "Single", "Single", 0, 0);
        keywords["Predicate"] = new Token(TokenType.PREDICATE, "Predicate", "Predicate", 0, 0);
        keywords["PostAction"] = new Token(TokenType.POSTACTION, "PostAction", "PostAction", 0, 0);
        keywords["Source"] = new Token(TokenType.SOURCE, "Source", "Source", 0, 0);

        keywords["Hand"] = new Token(TokenType.POINTER, "Hand", "Hand", 0, 0);
        keywords["Field"] = new Token(TokenType.POINTER, "Field", "Field", 0, 0);
        keywords["Deck"] = new Token(TokenType.POINTER, "Deck", "Deck", 0, 0);
        keywords["Graveyard"] = new Token(TokenType.POINTER, "Graveyard", "Graveyard", 0, 0);
        keywords["Board"] = new Token(TokenType.POINTER, "Board", "Board", 0, 0);

        keywords["TriggerPlayer"] = new Token(TokenType.FUN, "TriggerPlayer", "TriggerPlayer", 0, 0);
        keywords["HandOfPlayer"] = new Token(TokenType.FUN, "HandOfPlayer", "HandOfPlayer", 0, 0);
        keywords["DeckOfPlayer"] = new Token(TokenType.FUN, "DeckOfPlayer", "DeckOfPlayer", 0, 0);
        keywords["FieldOfPlayer"] =  new Token(TokenType.FUN, "FieldOfPlayer", "FieldOfPlayer", 0, 0);
        keywords["GraveyardOfPlayer"] = new Token(TokenType.FUN, "GraveyardOfPlayer", "GraveyardOfPlayer", 0, 0);
        keywords["Find"] = new Token(TokenType.FUN, "Find", "Find", 0, 0);
        keywords["Push"] = new Token(TokenType.FUN, "Push", "Push", 0, 0);
        keywords["SendBottom"] = new Token(TokenType.FUN, "SendBottom", "SendBottom", 0, 0);
        keywords["Pop"] = new Token(TokenType.FUN, "Pop", "Pop", 0, 0);
        keywords["Remove"] = new Token(TokenType.FUN, "Remove", "Remove", 0, 0);
        keywords["Shuffle"] = new Token(TokenType.FUN, "Shuffle", "Shuffle", 0, 0);

        keywords["Number"] = new Token(TokenType.NUMBERTYPE, "Number", "Number", 0, 0);
        keywords["String"] = new Token(TokenType.STRINGTYPE, "String", "String", 0, 0);
        keywords["Bool"] = new Token(TokenType.BOOLTYPE, "Bool", "Bool", 0, 0);
    }
    public List<Token> ScanTokens()
    {
        try
        {
            while (!IsAtTheEnd())
            {
                start = current;
                ScanToken();
            }
            foreach(var token in Tokens)
            {
                if (token.Type== TokenType.SOURCE)
                {
                    Debug.Log($"{token.Lexeme} es el nombre del token");
                }
            }
            Tokens.Add(new Token(TokenType.EOF, "", "" , line, current));
            return Tokens;
        }
        catch (Error ex)
        {
            Errors.Add("Lexic error" + ex.Message);
            return null;
        }
    }
    void ScanToken()
    {
        char c = Advance();
        switch(c)
        {
            //Single-character token
            case '(': AddToken(TokenType.LEFT_PAREN); break;
            case ')': AddToken(TokenType.RIGHT_PAREN); break;
            case '{': AddToken(TokenType.LEFT_BRACE); break;
            case '}': AddToken(TokenType.RIGHT_BRACE); break;
            case '[': AddToken(TokenType.LEFT_BRACK); break;
            case ']': AddToken(TokenType.RIGHT_BRACK); break;
            case ',': AddToken(TokenType.COMMA); break;
            case '.': AddToken(TokenType.DOT); break;
            case ':': AddToken(TokenType.COLON); break;
            case ';': AddToken(TokenType.SEMICOLON); break;
            case '*': AddToken(TokenType.STAR); break;
            case '^': AddToken(TokenType.POW); break;
            case '%': AddToken(TokenType.PERCENT); break;
            //One,two or three character token
            case '+':
            if(Match('+')) AddToken(TokenType.PLUS_PLUS);
            else if(Match('=')) AddToken(TokenType.PLUS_EQUALS);
            else AddToken(TokenType.PLUS);
            break;
            case '-':
            if(Match('-')) AddToken(TokenType.MINUS_MINUS);
            else if(Match('=')) AddToken(TokenType.MINUS_EQUALS);
            else AddToken(TokenType.MINUS);
            break;
            case '!':
            AddToken(Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG); 
            break;
            case '=':
            if(Match('=')) AddToken(TokenType.EQUAL_EQUAL);
            else if(Match('>')) AddToken(TokenType.EQUAL_GREATER);
            else AddToken(TokenType.EQUAL);
            break;
            case '<':
            AddToken(Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
            break;
            case '>':
            AddToken(Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
            break;
            case '/':
            if(Match('/'))
            {
                while(Peek() != '\n' && !IsAtTheEnd())
                {
                    Advance();
                }
            }
            else AddToken(TokenType.SLASH);
            break;
            case'@':
            AddToken(Match('@') ? TokenType.ATSIGN_ATSIGN : TokenType.ATSIGN); 
            break;
            case'|':
            if(Match('|')) AddToken(TokenType.OR);
            break;
            case'&':
            if(Match('&')) AddToken(TokenType.AND);
            break;
            case'"': String(); break;
            case ' ':
            case '\r':
            case '\t':
            // Ignore whitespace.
            break;
            case '\n':
            line++;
            break;
            default:
            if(IsDigit(c)) Number();
            else if(IsAlpha(c)) Identifier();
            else Errors.Add(line +" " + "Unexepected character." + start + " " + current);
            break;
        }
    }
    bool IsAtTheEnd()
    {
        return current >= Input.Length;
    }
    char Advance()
    {
        current ++;
        return Input[current - 1];
    }
    bool Match(char expected)
    {
        if(IsAtTheEnd()) return false;
        if(Input[current] != expected) return false;
        current++;
        return true;
    }
    char Peek()
    {
        if(IsAtTheEnd()) return '\0';
        return Input[current];
    }
    char PeekNext()
    {
        if(current+1>= Input.Length) return '\0';
        return Input[current+1];
    }
    void String()
    {
        while(Peek() != '"' && !IsAtTheEnd())
        {
            if(Peek() == '\n') line++;
            Advance();
        }
        if(IsAtTheEnd())
        {
            Errors.Add($"{line}: Unfinished string.");
        }
        Advance();
        string value = Input.Substring(start+1,current-start-2);
        AddToken(TokenType.STRING, value);
    }
    bool IsDigit(char c)
    {
        return c >= '0' && c <= '9'; 
    }
    void Number()
    {
        while(IsDigit(Peek())) Advance();
        if(Peek() == '.' && IsDigit(PeekNext()))
        { 
            Advance();
            while(IsDigit(Peek())) Advance();
        }
        AddToken(TokenType.NUMBER,System.Double.Parse(Input.Substring(start,current-start)));
    }
    bool IsAlpha(char c)
    {
        return (c >= 'a' && c <= 'z')||(c >= 'A' && c <= 'Z') ||c == '_';
    }
    bool IsAlphaNummeric(char c)
    {
        return IsAlpha(c) || IsDigit(c);
    }
    void Identifier()
    {
        while(IsAlphaNummeric(Peek())) Advance();
        string text = Input.Substring(start,current-start);
        TokenType type = TokenType.IDENTIFIER;
        if(keywords.ContainsKey(text))
        {
            Token token = keywords[text];
            Tokens.Add(new Token(token.Type,token.Lexeme,token.Lexeme,line,current/line)); //Revisar
        }
        else AddToken(type);
    }
    void AddToken(TokenType type)
    {
        AddToken(type,""); //Revisar
    }
    void AddToken(TokenType type, System.Object literal)
    {
        string text = Input.Substring(start,current-start);
        Tokens.Add(new Token(type,text,literal,line,current/line));
    }
}

