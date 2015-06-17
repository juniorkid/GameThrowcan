using UnityEngine;
using System.Collections;

public class DeleteSelf : MonoBehaviour {

	private bool m_destroy = false;

	// Use this for initialization
	void Update(){
		if (transform.position.y < -50 || transform.position.z > 150) {
			gameObject.GetComponent<DragBall>().SetShot(false, false);
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter() {
		if (!m_destroy) {
			Debug.Log ("DESTROY BALLLLL");
			StartCoroutine(DestroyBall());
		}
	}

	private IEnumerator DestroyBall(){
		m_destroy = true;
		yield return new WaitForSeconds(3f);
		Destroy (gameObject);
	}
}
