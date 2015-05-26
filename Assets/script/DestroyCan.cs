using UnityEngine;
using System.Collections;

public class DestroyCan : MonoBehaviour {

	// Use this for initialization
	private bool m_stay = false;

	private bool m_destroy = false;

	private float delay = 0f;

	void OnCollisionStay ()
	{
		m_stay = true;
		m_destroy = true;
		//Debug.Log (m_crash);
	}

	void OnCollisionEnter ()
	{
		m_stay = true;
		m_destroy = false;
		//Debug.Log (m_crash);
	}

	void OnCollisionExit(){
		if(m_stay){
			delay = 4f;
			m_stay = false;
		}
	}
	
	void DestroyObj(){
		if (!m_stay && m_destroy) {
			Destroy (gameObject);
		}
	}
	
	void Update(){
		delay -= Time.deltaTime;
		if(delay <= 0 && !m_stay && m_destroy)
			DestroyObj();
	}
}
