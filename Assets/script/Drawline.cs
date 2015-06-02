using UnityEngine;
using System.Collections;

public class Drawline : MonoBehaviour {

	private LineRenderer m_lineRenderer;
	private Vector3 m_origin;
	private Vector3 m_destination;

	private Color m_c1;
	private Color m_c2;

	private bool m_drawLine;

	// Use this for initialization
	void Start () {
		m_lineRenderer = GetComponent<LineRenderer> ();

		m_c1 = Color.white;
		m_c2 = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		m_lineRenderer.SetColors (m_c1, m_c2);
	}

	public void SetOrigin(Vector3 origin){
		m_origin = origin;

		m_lineRenderer.SetPosition(0, m_origin);
	}

	public void SetDestination(Vector3 destination){
		m_destination.y = m_origin.y;
		m_destination.x = (destination.x);
		m_destination.z = (Mathf.Abs(destination.y - m_origin.y) + m_origin.z) ;
//		Debug.Log(m_destination);
		m_lineRenderer.SetPosition (1, m_destination);
	//	Debug.Log ("TESTTT");

	}

	public bool GetEnable(){
		return m_drawLine;
	}

	public void SetEnable(bool enable){
		m_lineRenderer.enabled = enable;
		m_drawLine = enable;
	}

	public void SetColor(float c){
		m_c1 = new Color(1, 1f - c, 1f - c, 255);
		m_c2 = m_c1;
	}
}
