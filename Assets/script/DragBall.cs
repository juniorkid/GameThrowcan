using UnityEngine;
using System.Collections;

public class DragBall : MonoBehaviour {

	private Vector3 m_mousePosition;
	private bool m_canShot = false;
	private bool m_hasShot = false;
	private bool m_overBall = false;
	private bool m_onDrag = false;

	void OnMouseUp(){
		if (m_onDrag) {
			Debug.Log ("UPPP");
			m_canShot = false;
			m_onDrag = false;
		}
	}

	// Use this for initialization
	void OnMouseDrag(){
		if (!m_hasShot && m_overBall) {
			Debug.Log("DRAG BALL");

			m_onDrag = true;
			m_canShot = false;

			m_mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 15);
			Vector3 objPos = Camera.main.ScreenToWorldPoint (m_mousePosition);
			objPos.y = -5.5f;

			if (objPos.x < -2.57f)
				objPos.x = -2.57f;
			else if (objPos.x > 2.57f)
				objPos.x = 2.57f;

			transform.position = objPos;
		}
	}

	public Vector3 GetMousePosition(){
		return m_mousePosition;
	}

	void OnMouseOver(){
		m_overBall = true;
	}

	void OnMouseExit(){
		Debug.Log ("EXITTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTt");
		m_overBall = false;
		if (m_onDrag) {
			m_onDrag = false;
			m_canShot = true;
			Debug.Log ("EXIT");
		}
	}

	public bool GetShot(){
		return m_canShot;
	}

	public void SetShot(bool hasShot, bool canShot){
		m_hasShot = hasShot;
		m_canShot = canShot;
	}
}
