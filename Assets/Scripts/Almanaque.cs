using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almanaque : MonoBehaviour 
{

  public GameObject almanaq;

  public void OnClick()
  {
	CanvasGroup canvasGroup = almanaq.GetComponent<CanvasGroup>();
    canvasGroup.alpha = 1;
    canvasGroup.interactable = true;
    canvasGroup.blocksRaycasts = true;
  }
  public void Hide()
  {
	CanvasGroup canvasGroup = almanaq.GetComponent<CanvasGroup>();
    canvasGroup.alpha = 0;
    canvasGroup.interactable = false;
    canvasGroup.blocksRaycasts = false;
  }
}
