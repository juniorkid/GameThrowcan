using UnityEngine;
using System.Collections;

public class ShowScore : MonoBehaviour {
	//Show Score in Main Menu

	// Use this for initialization
	void OnGUI(){
		GUI.color = Color.red;
		GUIStyle myStyle = new GUIStyle();
		myStyle.fontSize = 60;
		GUI.Label(new Rect( (Screen.width / 2) - 80,150, 100, 30), "Score", myStyle);
		if(PlayerPrefs.GetInt("Can") == 999999)
			GUI.Label(new Rect((Screen.width / 2) - 20, 230, 100, 30), "" + 0 , myStyle);
		else if(PlayerPrefs.GetInt("Can") < 10)
			GUI.Label(new Rect((Screen.width / 2) - 20, 230, 100, 30), "" + PlayerPrefs.GetInt("Can") , myStyle);
		else if(PlayerPrefs.GetInt("Can") < 100)
			GUI.Label(new Rect( (Screen.width / 2) - 35, 230, 100, 30), "" + PlayerPrefs.GetInt("Can"), myStyle);
	}
}
