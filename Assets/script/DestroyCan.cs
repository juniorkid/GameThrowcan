using UnityEngine;
using System.Collections;

public class DestroyCan : MonoBehaviour {

	// Use this for initialization
	private bool m_stay = true;
	

	private float delay = 0f;

	void OnCollisionStay ()
	{
		m_stay = true;
		//Debug.Log (m_crash);
	}

	void OnCollisionEnter ()
	{
		m_stay = true;
		//Debug.Log (m_crash);
	}

	void OnCollisionExit(){
		if(m_stay){
			delay = 2.5f;
			m_stay = false;
		}
	}

	
	void Update(){
		if (!m_stay) {
		//	Debug.Log ("DELETE");
			delay -= Time.deltaTime;
			if (delay <= 0)
				Destroy (gameObject);
		}
	}
}
