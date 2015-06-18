using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
	public Rigidbody m_projectile; // Prefab for throw
	public Transform m_shotPos;
	[Range(1.0f, 100.0f)]
	public float m_shotForce = 2f;

	private bool m_hasClick = false;
	private float m_distance ;
	private Vector3 m_mouseStartPoint;
	private Vector3 m_mouseEndPoint;

	private Rigidbody m_shot; // object use to throw
	
	private float m_angle;
	private bool m_gameOver;
	private int m_score ;
	private int m_lastscore;

	private float m_timeStart;
	private float m_timeStop;

	private bool m_hasCreate;
	
	void Start(){

		Time.timeScale = 1.15f;

		m_shot = Instantiate(m_projectile, new Vector3(0f, -5.5f, 15), m_shotPos.rotation) as Rigidbody;
		m_shot.useGravity = false;

		m_shotPos.rotation = Quaternion.Euler(345,-270,0);
		m_score = 0;

		m_hasCreate = false;

		PlayerPrefs.SetInt ("Can", 0);
	}
	
	void Update ()
	{
		m_gameOver = gameObject.GetComponent<GameOver> ().GetGameOver ();

			if (!m_gameOver) {
				if(m_shot != null){
					//Debug.Log ("TEST");
					Shot ();
				}
				else if(!m_hasCreate){
					Debug.Log ("NEW BALL");
					StartCoroutine(CraeteBall());
					m_hasCreate = true;
					m_hasClick = false;
				}
			} else {
			//	StoreHighscore (m_score);
				StartCoroutine (GoMenu ());
			}
	}

	private IEnumerator CraeteBall(){
		m_score++;
		yield return new WaitForSeconds (0.5f);

		// Limit Ball

		if (m_score == 5) {
			gameObject.GetComponent<GameOver> ().SetGameover (true);
		} else {
			m_shot = Instantiate (m_projectile, new Vector3 (0f, -5.5f, 15), m_shotPos.rotation) as Rigidbody;
			m_shot.useGravity = false;
			m_hasCreate = false;
		}
	}

	void Shot(){

		if (m_shot.GetComponent<DragBall> ().GetShot () && !m_hasClick) {

			m_hasClick = true;

			m_mouseStartPoint = m_shot.GetComponent<DragBall> ().GetMousePosition ();
			m_timeStart = Time.realtimeSinceStartup;
			Vector3 objPos = Camera.main.ScreenToWorldPoint (m_mouseStartPoint);

			objPos.y = -5.5f;

			gameObject.GetComponent<Drawline> ().SetOrigin (objPos);

		}
		if (!m_shot.GetComponent<DragBall> ().GetShot ()) {
			m_hasClick = false;
			//	Debug.Log("AAA " + m_hasClick);
		}

		if (m_hasClick) {

			float time;
			float force;
			float angleByTime;

			//	Debug.Log("BBB " + m_hasClick);

			//	Debug.Log ("AIMMING");

			Vector3 pos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 15);
			Vector3 objPos = Camera.main.ScreenToWorldPoint (pos);
			
			//Draw Line End Position
			if (pos.y >= m_mouseStartPoint.y)
				m_distance = Vector3.Distance (m_mouseStartPoint, pos);
			else {
				m_distance = 0;
			}

			m_timeStop = Time.realtimeSinceStartup;
			
			time = m_timeStop - m_timeStart;
			
			force = m_distance / time;

			//Check if position mouse < start mouse don't show line

			if (Input.mousePosition.y >= m_mouseStartPoint.y) {
				//	Debug.Log("UPPP");
				gameObject.GetComponent<Drawline> ().SetEnable (true);
				gameObject.GetComponent<Drawline> ().SetColor (force / 6000f);
				gameObject.GetComponent<Drawline> ().SetDestination (objPos);
			} else 
				gameObject.GetComponent<Drawline> ().SetEnable (false);

			if (Input.GetMouseButtonUp (0)) {

			
				m_hasClick = false;
				m_mouseEndPoint = Input.mousePosition;

				//If  Origin equal Destination Delete ball
				if (m_mouseEndPoint.y >= m_mouseStartPoint.y && m_distance != 0) {	
				
					// Rotate to end point of mouse
					m_angle = Cal_angle (m_mouseStartPoint, m_mouseEndPoint);
				
					//		Debug.Log(Time.realtimeSinceStartup);


					angleByTime = 346.5f - (time * 50);
				
				m_shotPos.rotation = Quaternion.Euler (angleByTime, -270 - m_angle, 0);
				
					//Debug.Log (m_shotPos.rotation);

					// Maximum force and angle
					if (force > 6000f){
						angleByTime = 345;
						force = 6000f;
					}

					// Minumum force and angle
					else if (force < 3100){
						force = 1800f;
							angleByTime = 325;
					}

					Debug.Log (force);
					Debug.Log (angleByTime);

					m_shotPos.rotation = Quaternion.Euler (angleByTime, -270 - m_angle, 0);

					m_shot.useGravity = true;
					// Throw ball
					m_shot.AddForce (m_shotPos.forward * force * m_shotForce);
				
					//	Debug.Log ("FORCE:  "  + ((m_distance/time)).ToString());
					//m_shot.GetComponent<DeleteSelf>().SetDestroy(true);
				
					//Count number ball that throw
				
					// Close line
					//	gameObject.GetComponent<Drawline>().SetEnable(false);
					//	m_distance = 0;
				
					m_shot.GetComponent<DragBall> ().SetShot (true, false);


				}
			}
		}
	}

	//Calculate angle to rotate
	float Cal_angle(Vector3 origin, Vector3 dest){
		Vector3 origin_temp = new Vector3( origin.x, origin.y, 15);
		Vector3 origin_worldspace = Camera.main.ScreenToWorldPoint(origin_temp);
		Vector3 dest_temp = new Vector3( dest.x, dest.y, 15);
		Vector3 dest_worldspace = Camera.main.ScreenToWorldPoint(dest_temp);

		float x = Mathf.Atan2(dest_worldspace.y - origin_worldspace.y,dest_worldspace.x - origin_worldspace.x) * 180f / 3.14f;
		//Debug.Log (x);

		return x;

	}

	//Check for high score
	void StoreHighscore(int newHighscore)
	{
		int oldHighscore = PlayerPrefs.GetInt("highscore", 10000);    
		if (newHighscore < oldHighscore) {
			PlayerPrefs.SetInt ("highscore", newHighscore);
			gameObject.GetComponent<GameOver> ().SetHighscore ();
		}
	}

	//Create Label show scores
	void OnGUI(){
		GUIStyle myStyle = new GUIStyle();
		myStyle.fontSize = 40;
		myStyle.normal.textColor=Color.white;
		GUI.Label (new Rect (10, 10, 100, 100), "Ball: " + m_score , myStyle);

		myStyle.fontSize = 40;
		myStyle.normal.textColor=Color.white;
		GUI.Label (new Rect (10, 50, 100, 100), "Can : " + PlayerPrefs.GetInt("Can") , myStyle);

	}

	IEnumerator GoMenu(){
		yield return StartCoroutine (gameObject.GetComponent<GameOver> ().ShowScore ());
		Application.LoadLevel ("Mainmenu");
	}
}