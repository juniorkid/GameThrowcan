using UnityEngine;
using System.Collections;

public class ShowScore : MonoBehaviour {

	// Use this for initialization
	void OnGUI(){
		GUI.color = Color.red;
		GUIStyle myStyle = new GUIStyle();
		myStyle.fontSize = 100;
		GUI.Label(new Rect(130,310, 100, 30), "High Score", myStyle);
		GUI.Label(new Rect(350,450, 100, 30), "" + PlayerPrefs.GetInt("highscore") , myStyle);
	}
}
