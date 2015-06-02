using UnityEngine;
using System.Collections;

public class ShowScore : MonoBehaviour {
	//Show Score in Main Menu

	// Use this for initialization
	void OnGUI(){
		GUI.color = Color.red;
		GUIStyle myStyle = new GUIStyle();
		myStyle.fontSize = 60;
		GUI.Label(new Rect( (Screen.width / 2) - 140,150, 100, 30), "Best Score", myStyle);
		if(PlayerPrefs.GetInt("highscore") == 999999)
			GUI.Label(new Rect((Screen.width / 2) - 20, 230, 100, 30), "" + 0 , myStyle);
		else if(PlayerPrefs.GetInt("highscore") < 10)
			GUI.Label(new Rect((Screen.width / 2) - 20, 230, 100, 30), "" + PlayerPrefs.GetInt("highscore") , myStyle);
		else if(PlayerPrefs.GetInt("highscore") < 100)
			    GUI.Label(new Rect( (Screen.width / 2) - 35, 230, 100, 30), "" + PlayerPrefs.GetInt("highscore"), myStyle);
		else if(PlayerPrefs.GetInt("highscore") < 1000)
				GUI.Label(new Rect( (Screen.width / 2) - 50, 230, 100, 30), "" + PlayerPrefs.GetInt("highscore"), myStyle);
	}
}
