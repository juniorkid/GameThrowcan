using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	private bool m_gameOver ;
	private bool m_highscore;

	void Start(){
		m_gameOver = false;
		m_highscore = false;
	}
	// Update is called once per frame
	void Update () {

	}

	//Set for game over
	public bool GetGameOver(){
		return m_gameOver;
	}

	public void SetGameover(bool gameover){
		m_gameOver = gameover;
	}

	// Set to show highscore
	public void SetHighscore(){
		m_highscore = true;
	}

	public IEnumerator ShowScore(){
		if (m_highscore) {
			Destroy (GameObject.FindWithTag ("Floor"));
			Destroy (GameObject.FindWithTag ("Ball"));
			Destroy (GameObject.FindWithTag ("Camera"));
			Destroy (GameObject.FindWithTag ("UI"));
			yield return new WaitForSeconds (2f);
		} else {
			yield return new WaitForSeconds (2f);
		}

	}

	void OnGUI(){
		//Show Highscore when end game
/*
		GUIStyle myStyle = new GUIStyle ();
		myStyle.fontSize = 50;
		myStyle.normal.textColor = Color.black;
		if (m_gameOver && m_highscore){
			GUI.Label (new Rect (Screen.width / 2 - 187, Screen.height/2 - 120, 100, 100), "New Best Score !! ", myStyle);
			if(PlayerPrefs.GetInt("highscore") == 999999)
				GUI.Label(new Rect((Screen.width / 2) - 20, Screen.height/2 -50, 100, 30), "" + 0 , myStyle);
			else if(PlayerPrefs.GetInt("highscore") < 10)
				GUI.Label(new Rect((Screen.width / 2) - 20, Screen.height/2 -50, 100, 30), "" + PlayerPrefs.GetInt("highscore") , myStyle);
			else if(PlayerPrefs.GetInt("highscore") < 100)
				GUI.Label(new Rect( (Screen.width / 2) - 35, Screen.height/2 -50, 100, 30), "" + PlayerPrefs.GetInt("highscore"), myStyle);
			else if(PlayerPrefs.GetInt("highscore") < 1000)
				GUI.Label(new Rect( (Screen.width / 2) - 50, Screen.height/2 -50, 100, 30), "" + PlayerPrefs.GetInt("highscore"), myStyle);
			//GUI.Label (new Rect (Screen.width / 2 - 10, Screen.height/2 -50, 100, 100), "" + PlayerPrefs.GetInt("highscore"), myStyle);
		}*/
	}
}
