using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor1 : MonoBehaviour
{
     public GameObject editor;
     public GameObject errores;
    void Start()
    {
        CanvasGroup canvasEditor = editor.GetComponent<CanvasGroup>();
        canvasEditor.alpha = 0;
        canvasEditor.interactable = false;
        canvasEditor.blocksRaycasts = false;
        CanvasGroup canvaserrores = errores.GetComponent<CanvasGroup>();
        canvaserrores.alpha = 0;
        canvaserrores.interactable = false;
        canvaserrores.blocksRaycasts = false;
    }

    
}
