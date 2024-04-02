using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

	public bool onField;

	public bool isOverDropZone;
	
	GameObject playerDropZone;
	
	public GameObject canvas;

	public Vector2 startPosition;
	public GameObject startParent;
	GameObject hand;

	void Start ()
	{
		canvas = GameObject.Find("Canvas");
		hand = gameObject.transform.parent.gameObject;
		playerDropZone = hand;
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("aaaaaaa");
		startPosition = transform.position;
		startParent = transform.parent.gameObject;
	}
	
	public void OnDrag (PointerEventData eventData)
	{
		if (!onField)
		{
			this.transform.position = eventData.position;
			transform.SetParent(canvas.transform);
		}
		
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Enter");
        isOverDropZone = true;
		playerDropZone = collision.gameObject;
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		Debug.Log("Exit");
        isOverDropZone = false;
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("EndDrag");
		if (isOverDropZone && Rodri())
		{
			
			Debug.Log("OverDropZone");

			transform.SetParent(playerDropZone.transform, false);

			onField = true;
		}
		else
		{
			Debug.Log("Restart");
			transform.position = startPosition;
			transform.SetParent(startParent.transform, false);
			
		}
	}	
	public bool Rodri ()
	{
		Zones conditions = playerDropZone.GetComponent<Zones>();
		string k = conditions.zoneNames;
		string l = gameObject.GetComponent<CardDisplay>().zone;
		string a = gameObject.GetComponent<CardDisplay>().zoneaux;
		
		if (k == l || k == a)
		{
		  return true;
		} 
		else
		{
			return false;
		}
	     
	}
}
