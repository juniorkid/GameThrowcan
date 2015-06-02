using UnityEngine;
using System.Collections;

public class DeleteSelf : MonoBehaviour {

	// Use this for initialization
	private bool m_destroy = false;
	public float m_delay;
	
	void OnCollisionEnter (Collision col)
	{
		m_destroy = true;
		m_delay = 4f;
	}

	public IEnumerator DestroyObj(){
		yield return new WaitForSeconds(m_delay);
		Destroy (gameObject);
	}	

	public void SetDestroy(bool destroy){
		m_destroy = destroy;
	}

	public void SetDelay(float delay){
		m_delay = delay;
	}

	void Update(){
		if (m_destroy) {
			StartCoroutine (DestroyObj ());
		}

		if(transform.position.y < -50 || transform.position.z > 150)
			Destroy (gameObject);
	}
}
