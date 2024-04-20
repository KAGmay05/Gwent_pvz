// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class zoom : MonoBehaviour
// {
//     public GameObject Canvas;
//     public GameObject zoomCard;
//     private Vector2 zoomScale = new Vector2(2, 2);
//     public void Awake()
//     {
//         Canvas = GameObject.Find("Canvas");
//     }
//     public void OnMouseEnter()
//     {
//         zoomCard = Instantiate(gameObject, new Vector2(40, 40), Quaternion.identity);
//         zoomCard.transform.SetParent(Canvas.transform, false);
//         zoomCard.transform.localScale = zoomScale;
//     }
//     public void OnMouseExit()
//     {
//         Destroy(zoomCard);
//     }
// }
