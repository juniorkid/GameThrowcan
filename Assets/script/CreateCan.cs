using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateCan : MonoBehaviour {

	public GameObject[] m_levelCan;
	public int m_level;
	private Vector3[] m_posLevelCan;

	private bool m_checkCan;

	// Use this for initialization
	void Start () {
		m_posLevelCan = new Vector3[5];
		
		m_posLevelCan [0] = new Vector3 (0f, 0.28f, 49.53f);
		m_posLevelCan [1] = new Vector3 (-1.25f, 0.28f, 49.53f);
		m_posLevelCan [2] = new Vector3 (-1.49f, 0.28f, 49.53f);
		m_posLevelCan [3] = new Vector3 (-1.25f, 0.28f, 49.53f);
		m_posLevelCan [4] = new Vector3 (-2.52f, 0.28f, 49.53f);
		Instantiate (m_levelCan[m_level], m_posLevelCan[m_level], Quaternion.identity);
		m_level ++;
		m_checkCan = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindWithTag ("Can") == null) {
			if(m_level < 5){
				Instantiate (m_levelCan [m_level], m_posLevelCan [m_level], Quaternion.identity);
				m_level ++;
				Debug.Log (m_level);
			}
			else{
				gameObject.GetComponent<GameOver>().SetGameover(true);
			}
		} 
	}

	public void SetCheckCan(bool checkCan){
		m_checkCan = checkCan;
	}


}
