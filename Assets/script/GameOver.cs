using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	private bool gameOver ;

	void Start(){
		gameOver = false;
	}
	// Update is called once per frame
	void Update () {
		if(GameObject.FindWithTag ("Can")== null)
			gameOver = true;
	}

	public bool getGameOver(){
		return gameOver;
	}
}
