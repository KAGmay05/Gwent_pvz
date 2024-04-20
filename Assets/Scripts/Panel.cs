
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour 
{
   public GameObject panel;
   public Text winner;
   bool endGame;
   public GameManager gameManager;
	void Start () 
	{
		OnClick();
	}
	public void Rounds()
	{
		Vector3 pos = panel.transform.position;
		pos.z = 0;
		panel.transform.position = pos;
		gameManager = GameObject.Find("gamemanager").GetComponent<GameManager>();
		if(gameManager.plantWin == true)
		{
			panel.transform.rotation = Quaternion.Euler(0,0,0);
			winner.text = "Plantas ganan la ronda";
		}
		else if(gameManager.zombieWin == true)
		{
			panel.transform.rotation = Quaternion.Euler(0,0,0);
			winner.text = "Zombies ganan la ronda";
		}
		else 
		panel.transform.rotation = Quaternion.Euler(0,0,0);
		winner.text = "Empate";
	}
	public void Game()
	{
		Vector3 pos = panel.transform.position;
		pos.z = 0;
		panel.transform.position = pos;
		if(gameManager.plantWin == true)
		{
			winner.text = "Plantas ganan el juego";
			endGame = true;
		}
		else 
		{
			winner.text = "Zombies ganan el juego";
			endGame = true;
		}
	}
  public void OnClick()
  {
	Vector3 pos = panel.transform.position;;
	pos.z = -10;
    panel.transform.position = pos;
	if(endGame)
	{
		endGame = false;
		pos.z = 0;
	}
  }


	
    
}
