using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide : MonoBehaviour 
{
 public GameObject almanaq;
 public void Hide()
  {
	CanvasGroup canvasGroup = almanaq.GetComponent<CanvasGroup>();
    canvasGroup.alpha = 0;
    canvasGroup.interactable = false;
    canvasGroup.blocksRaycasts = false;
  }
}


	
