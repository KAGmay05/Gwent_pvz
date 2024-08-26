public class Error : Exception
{
    public string Mess { get; private set; }
    

    public Error(string message)
    {
        Mess = message;
    
    }

    public string Report()
    {
        return $" Error: {Message}";
    }
}
public class RunTimeError : Exception
{
    public Token token;
    public RunTimeError(Token token, string message) : base(message)
    {

        this.token = token;
    }

}
public enum ErrorType
{
    LEXICAL, SYNTAX, SEMANTIC
}