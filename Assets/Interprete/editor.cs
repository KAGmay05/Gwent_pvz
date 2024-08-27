using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class editor : MonoBehaviour
{
    public TextMeshProUGUI logtext;
    public ScrollRect scrollRect;
    public void AddLogMessage(string message)
    {
        logtext.text += message + "\n";
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
