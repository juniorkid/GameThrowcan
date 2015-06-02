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

	void Start(){
		m_shotPos.rotation = Quaternion.Euler(345,-270,0);
		m_score = 0;
	}
	
	void Update ()
	{
		m_gameOver = gameObject.GetComponent<GameOver> ().GetGameOver ();
		if (!m_gameOver) {
			//Debug.Log ("TEST");
			Shot ();
		}
		else {
			StoreHighscore (m_score);
			StartCoroutine(GoMenu());
		}
	}

	void Shot(){
		// Check for mouse press
		if(Input.GetMouseButtonDown(0) && !m_hasClick){
			m_shotPos.rotation = Quaternion.Euler(330,-270,0); // set to default rotation
			m_hasClick = true ; 
			m_mouseStartPoint = Input.mousePosition;

			// Create object at mouse point
			Vector3 pos = new Vector3( m_mouseStartPoint.x, m_mouseStartPoint.y, 15);
			Vector3 objPos = Camera.main.ScreenToWorldPoint(pos);
			m_shot = Instantiate(m_projectile, objPos, m_shotPos.rotation) as Rigidbody;
			m_shot.useGravity = false; // use for give object don't fall down

			//Draw Line Start Position
			gameObject.GetComponent<Drawline>().SetOrigin(objPos);

		}

		if(m_hasClick){
			Vector3 pos = new Vector3( Input.mousePosition.x, Input.mousePosition.y, 15);
			Vector3 objPos = Camera.main.ScreenToWorldPoint(pos);

			//Draw Line End Position
			if(Input.mousePosition.y >= m_mouseStartPoint.y)
				m_distance = Vector3.Distance(m_mouseStartPoint, Input.mousePosition);
			else
				m_distance = 0;

			if(m_distance > 500f)
				m_distance = 500;

			//Check if position mouse < start mouse don't show line
			if(Input.mousePosition.y >= m_mouseStartPoint.y){
			//	Debug.Log("UPPP");
				gameObject.GetComponent<Drawline>().SetEnable(true);
				gameObject.GetComponent<Drawline>().SetColor(m_distance/500f);
				gameObject.GetComponent<Drawline>().SetDestination(objPos);
			}
			else 
				gameObject.GetComponent<Drawline>().SetEnable(false);
		}



		// Release mouse button for Throw
		if(m_hasClick && Input.GetMouseButtonUp(0)){
			m_shot.useGravity = true;
			m_hasClick = false;
			m_mouseEndPoint = Input.mousePosition;
			//If  Origin equal Destination Delete ball
			if(m_distance == 0){
				m_shot.GetComponent<DeleteSelf>().SetDestroy(true);
				m_shot.GetComponent<DeleteSelf>().SetDelay(0.1f);
			}

			else if(m_mouseEndPoint.y >= m_mouseStartPoint.y){	

				m_shot.GetComponent<DeleteSelf>().SetDestroy(true);
				m_shot.GetComponent<DeleteSelf>().SetDelay(5f);

					// Rotate to end point of mouse
				m_angle = Cal_angle(m_mouseStartPoint,m_mouseEndPoint);
				m_shotPos.rotation = Quaternion.Euler(330,-270-m_angle,0);

					// Throw ball
				m_shot.AddForce(m_shotPos.forward * m_distance * m_shotForce);
				//m_shot.GetComponent<DeleteSelf>().SetDestroy(true);

					//Count number ball that throw
				m_score++;

				// Close line
				gameObject.GetComponent<Drawline>().SetEnable(false);
				m_distance = 0;
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
		float x;
		float y;

		GUIStyle myStyle = new GUIStyle();
		myStyle.fontSize = 40;
		myStyle.normal.textColor=Color.white;
		GUI.Label (new Rect (10, 10, 100, 100), "Ball: " + m_score , myStyle);

		myStyle.normal.textColor=Color.red;
		myStyle.fontSize = 50;

		if (m_distance >= 500) {
			if (Screen.height - m_mouseStartPoint.y - 10 > Screen.height - 50)
				y = Screen.height - 50;
			else {
				y = Screen.height - m_mouseStartPoint.y - 10;
			}
			Debug.Log(y);
			x = m_mouseStartPoint.x - 110;
			GUI.Label (new Rect ( x, y, 100, 100), "Max Force" , myStyle);
		}

	}

	IEnumerator GoMenu(){
		yield return StartCoroutine (gameObject.GetComponent<GameOver> ().ShowScore ());
		Application.LoadLevel ("Mainmenu");
	}
}