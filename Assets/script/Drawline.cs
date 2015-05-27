using UnityEngine;
using System.Collections;

public class Drawline : MonoBehaviour {

	private LineRenderer m_lineRenderer;
	private Vector3 m_origin;
	private Vector3 m_destination;
	private bool m_drawLine;
	// Use this for initialization
	void Start () {
		m_lineRenderer = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetOrigin(Vector3 origin){
		m_origin = origin;

		m_lineRenderer.SetPosition(0, m_origin);
	}

	public void SetDestination(Vector3 destination){
		m_destination = destination;
	//	Debug.Log(m_destination);
		m_lineRenderer.SetPosition (1, m_destination);
	//	Debug.Log ("TESTTT");

	}

	public void setDraw(bool drawLine){
		m_lineRenderer.enabled  = drawLine;
	}

	public void setEnable(bool enable){
		m_lineRenderer.enabled = enable;
	}
}
