using UnityEngine;
using System.Collections;

public class PlayGame : MonoBehaviour {

	public void Startgame(){
		Application.LoadLevel ("Playgame");
	}

	public void ResetScore(){
		PlayerPrefs.SetInt("highscore", 999999);
	}
}
