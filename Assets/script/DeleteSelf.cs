using UnityEngine;
using System.Collections;

public class DeleteSelf : MonoBehaviour {

	// Use this for initialization
	public bool m_destroy = false;
	private float delay = 10f;
	
	void OnCollisionEnter (Collision col)
	{
		m_destroy = true;
		delay = 10f;
	}

	IEnumerator DestroyObj(){
		yield return new WaitForSeconds(4);
		Destroy (gameObject);
	}

	public void SetDestroy(bool destroy){
		m_destroy = destroy;
	}

	void Update(){
		if(m_destroy || delay <= 0)
			StartCoroutine (DestroyObj());	
	}
}
