using UnityEngine;
using System.Collections;

public class ShowScore : MonoBehaviour {

	// Use this for initialization
	void OnGUI(){
		GUI.color = Color.red;
		GUIStyle myStyle = new GUIStyle();
		myStyle.fontSize = 60;
		GUI.Label(new Rect( 55,150, 100, 30), "High Score", myStyle);
		GUI.Label(new Rect(180,230, 100, 30), "" + PlayerPrefs.GetInt("highscore") , myStyle);
	}
}
