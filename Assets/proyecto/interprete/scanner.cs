using System.Diagnostics;
using System.Xml;

class Scanner
{
   private string source {get; set;}
   private List<Token> tokens = new List<Token>();
   public Dictionary <string,Token> reservedKeys = new Dictionary<string,Token>();
   public int current = 0;
   public int start = 0;
   public int line = 1;
   
   Scanner(string Source)
   {
    source = Source;
    reservedKeys["while"] = new Token(TokenType.WHILE, "while", 0, 0);
    reservedKeys["for"] = new Token(TokenType.FOR, "for", 0, 0);
    reservedKeys["Effect"] = new Token(TokenType.FUN, "Effect", 0, 0);
    reservedKeys["effect"] = new Token(TokenType.EFFECT, "effect", 0, 0);
    reservedKeys["postaction"] = new Token(TokenType.POSTACTION, "postaction", 0, 0);
    reservedKeys["card"] = new Token(TokenType.CARD, "card", 0, 0);
    reservedKeys["Predicate"] = new Token(TokenType.PREDICATE, "Predicate", 0, 0);
    reservedKeys["false"] = new Token(TokenType.FALSE, "false", 0, 0);
    reservedKeys["true"] = new Token(TokenType.TRUE, "true", 0, 0);
    reservedKeys["Name"] = new Token(TokenType.NAME, "name", 0, 0);
    reservedKeys["in"] = new Token(TokenType.IN, "in", 0, 0);
    reservedKeys["Params"] = new Token(TokenType.PARAMS, "Params", 0, 0);
    reservedKeys["Action"] = new Token(TokenType.ACTION, "Action", 0, 0);
    reservedKeys["Type"] = new Token(TokenType.TYPE, "Type", 0, 0);
    reservedKeys["Faction"] = new Token(TokenType.FACTION, "faction", 0, 0);
    reservedKeys["Power"] = new Token(TokenType.POWER, "power", 0, 0);
    reservedKeys["Range"] = new Token(TokenType.RANGE, "range", 0, 0);
    reservedKeys["OnActivation"] = new Token(TokenType.ONACTIVATION, "OnActivation", 0, 0);
    reservedKeys["Fun"] = new Token(TokenType.FUN, "Fun", 0, 0);
    reservedKeys["Selector"] = new Token(TokenType.SELECTOR, "Selector", 0, 0);
    reservedKeys["Single"] = new Token(TokenType.SINGLE, "single", 0, 0);
    reservedKeys["Pointer"] = new Token(TokenType.POINTER, "pointer", 0, 0);
    reservedKeys["Method"] = new Token(TokenType.METHOD, "Method", 0, 0);
    reservedKeys["String"] = new Token(TokenType.STRINGTYPE, "String", 0, 0);
    reservedKeys["Number"] = new Token(TokenType.NUMBERTYPE, "Number", 0, 0);
    reservedKeys["Bool"] = new Token(TokenType.BOOLTYPE, "Bool", 0, 0);
    reservedKeys["Source"] = new Token(TokenType.SOURCE, "Source", 0, 0);
    reservedKeys["TriggerPlayer"] = new Token(TokenType.POINTER, "TriggerPlayer", 0, 0);
    reservedKeys["Board"] = new Token(TokenType.POINTER, "Board", 0, 0);
    reservedKeys["HandOfPlayer"] = new Token(TokenType.POINTER, "HandOfPlayer", 0, 0);
    reservedKeys["DeckOfPlayer"] = new Token(TokenType.POINTER, "DeckOfPlayer", 0, 0);
    reservedKeys["FieldOfPlayer"] = new Token(TokenType.POINTER, "FieldOfPlayer", 0, 0);
    reservedKeys["GraveyardOfPlayer"] = new Token(TokenType.POINTER, "GraveyardOfPlayer", 0, 0);
    reservedKeys["Find"] = new Token(TokenType.METHOD, "Find", 0, 0);
    reservedKeys["Push"] = new Token(TokenType.METHOD, "Push", 0, 0);
    reservedKeys["SendBottom"] = new Token(TokenType.METHOD, "SendBottom", 0, 0);
    reservedKeys["Pop"] = new Token(TokenType.METHOD, "Pop", 0, 0);
    reservedKeys["Remove"] = new Token(TokenType.METHOD, "Remove", 0, 0);
    reservedKeys["Shuffle"] = new Token(TokenType.METHOD, "Shuffle", 0, 0);
   }
   public char Advance ()
   {
    return source[current++];
   }
   private bool IsAtEnd()
   {
    return current >= source.Length;
   }
   private void AddToken(TokenType type)
   {
      string text = source.Substring(start,current);
      tokens.Add(new Token(type,text,line,current/line));
   }
   private void ScanTokens()
   {
      char c = Advance();
      switch (c) 
      {
         case '(': AddToken(TokenType.LEFT_PAREN);break;
         case ')': AddToken(TokenType.RIGHT_PAREN); break;
         case ']': AddToken(TokenType.RIGHT_BRACKET); break;
         case '[': AddToken(TokenType.LEFT_BRACKET); break;
         case '{': AddToken(TokenType.LEFT_BRACE); break;
         case '}': AddToken(TokenType.RIGHT_BRACE); break;
         case ',': AddToken(TokenType.COMMA); break;
         case '.': AddToken(TokenType.DOT); break;
         case ':': AddToken(TokenType.COLON); break;
         case ';': AddToken(TokenType.SEMICOLON); break;
         
         case '|': 
         if(Match ('|')) AddToken(TokenType.OR); break;
         
         case  '&':
         if(Match('&')) AddToken(TokenType.AND); break;
         case '/': 
          if(Match('/'))
          {
            while (Peek() != '\n' && !IsAtEnd()) Advance();
          }
          else AddToken(TokenType.SALSH);break;
         
         case '*': AddToken(TokenType.STAR); break;
      

         //ONE OR TWO CHARACTER TOKENS
         case '+':
         if(Match('+')) AddToken(TokenType.PLUS_PLUS);
         else if (Match('=')) AddToken(TokenType.PLUS_EQUAL);
         else AddToken(TokenType.PLUS); break;

         case '-' :
         if(Match('-')) AddToken(TokenType.MINUS_MINUS);
         else if (Match('=')) AddToken(TokenType.MINUS_EQUAL);
         else AddToken(TokenType.MINUS); break;

         case '=' :
         if(Match('>')) AddToken(TokenType.EQUAL_GREATER);
         else if (Match('=')) AddToken(TokenType.EQUAL_EQUAL);
         else AddToken(TokenType.EQUAL); break;

         case '!' : AddToken(Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);break;
         case '<' : AddToken(Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);break;
         case '>' : AddToken(Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);break;
         case '"' : String();break;
         case ' ':
         case '\r':
         case '\t': break;
         case '\n': line++; break;
         default :
          if(IsDigit(c)) Number();
          else if (IsAlpha(c)) Identifier();
          else Console.WriteLine("Error"); break;
        
      }
   }
   private bool IsDigit(char c)
   {
      return c >= 0 && c<= 9;
   }
   private void Number()
   {
      while(IsDigit(Peek())) Advance();
      if(Peek() == '.' && IsDigit(PeekNext()))
      {
          Advance();
          while (IsDigit(Peek())) Advance();
      }
      tokens.Add(new Token(TokenType.NUMBER,Double.Parse(source.Substring(start, current)),line,current));
   }
   
   private char PeekNext()
   {
      if(current + 1 >= source.Length) return '\0';
      return source[current +1];
   }
   private void String()
   {
      while(Peek() != '"' && !IsAtEnd())
      {
         if(Peek() == '\n') line++;
         Advance();
      }
      if(IsAtEnd())
      {Console.WriteLine("error");
      return;}
      Advance();
      string text = source.Substring(start +1,current -1);
      tokens.Add(new Token(TokenType.STRING,text,line,current));
      
   }
   List<Token> ScanToken()
   {
      while(!IsAtEnd	())
      {
         start = current;
         ScanToken();
      }
      tokens.Add(new Token(TokenType.EOF,"",line,current));
      return tokens;
   }
   private Boolean Match(char expected)
   {
      if (IsAtEnd()) return false;
      if(source[current] != expected) return false;

      current ++;
      return true;
   }
   private char Peek()
   {
      if(IsAtEnd()) return'\0';
      return source[current];
   }
   private bool IsAlpha(char c)
{
  return (c>= 'a' && c <= 'z' || c>= 'A' && c<= 'Z' || c == '_');
}
   private bool IsAlphaNumeric(char c)
   {
      return IsAlpha(c) || IsDigit(c);
   }
   private void Identifier()
   {
      while (IsAlphaNumeric(Peek())) Advance();
      string text = source.Substring(start,current);
      // TokenType type = reservedKeys;?//
      if(type ==null) type = TokenType.IDENTIFIER;
      AddToken(type);
   }

}