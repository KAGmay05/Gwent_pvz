using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Error : System.Exception
    {
    public string Mess { get; private set; }
    

    public Error(string message)
    {
        Mess = message;
    
    }

    public string Report()
    {
        return $" Error: {Mess}";
    }
}
public class RunTimeError : System.Exception
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

