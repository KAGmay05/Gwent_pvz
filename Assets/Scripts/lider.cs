using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lider : MonoBehaviour
{
	public Effects efectos;
	public void OnClick()
	{
		efectos = GameObject.Find("efectos").GetComponent<Effects>();
		efectos.DeleteRow();
	}
}
