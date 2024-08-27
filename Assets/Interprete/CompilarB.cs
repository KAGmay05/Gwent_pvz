using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CompilarB : MonoBehaviour
{
    public TMP_InputField input;
    public TextMeshProUGUI outputText;

    string output = "Tokens: \n";
    public GameObject errors;
    public void CompileAndRun()
    {
        List<string> Errors =new List<string>();
        // try
        // {
        string code = input.text;
        Lexer lexer = new Lexer(code,Errors);
        List<Token> tokens = lexer.ScanTokens();
        if(Errors.Count != 0)
        {
         foreach(var error in Errors)
         {
            outputText.text = error;
         }
        }
        // foreach(var token in tokens)
        // {
        //  output += $"{token.Type}: {token.Lexeme}\n";
        // }
        Parser parser = new Parser(tokens, Errors);
        Node effect = parser.Parse();
        if(Errors.Count != 0)
        {
            foreach(var error in Errors)
            {
                outputText.text = error;
            }
        }
        // Debug.Log("Parsed Effect:");
        // effect.Show();
        // Semantic semanticNew = new Semantic();
        // semanticNew.CheckNode(effect);
        // outputText.text = output;
        CanvasGroup canvaserrores = errors.GetComponent<CanvasGroup>();
        canvaserrores.alpha = 1;
        canvaserrores.interactable = true;
        canvaserrores.blocksRaycasts = true;        // ShowErrors();
        // }
        // catch (System.Exception ex)
        // {
            // outputText.text = "Error: " + ex.Message;
        }

        
        // Parser parser = new Parser(tokens);
        // Node effect = parser.Parse();
}

// void ShowErrors()
// {
//         CanvasGroup canvaserrores = errors.GetComponent<CanvasGroup>();
//         canvaserrores.alpha = 1;
//         canvaserrores.interactable = true;
//         canvaserrores.blocksRaycasts = true;
// }

