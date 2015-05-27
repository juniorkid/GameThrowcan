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

	public Camera m_camera;

	private float m_angle;
	private bool m_gameOver;
	private int m_score ;
	private int m_lastscore;

	private float delay; // delay for end games

	void Start(){
		m_shotPos.rotation = Quaternion.Euler(345,-270,0);
		m_score = 0;
	}
	
	void Update ()
	{
	//	Debug.Log (gameObject.GetComponent<GameOver> ().getGameOver ());
		m_gameOver = gameObject.GetComponent<GameOver> ().getGameOver ();
		if (!m_gameOver) {
			//Debug.Log ("TEST");
			Shot ();
			delay = 2f;
		}
		else {
			delay -= Time.deltaTime;
			if(delay <= 0){
				StoreHighscore (m_score);
				Application.LoadLevel ("Mainmenu");
			}
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
		}
		// Check for mouse don't press
		if(m_hasClick && Input.GetMouseButtonUp(0)){
			m_shot.useGravity = true;
			m_hasClick = false;
			m_mouseEndPoint = Input.mousePosition;
			m_distance = Vector3.Distance(m_mouseStartPoint, m_mouseEndPoint);
			
			if(m_mouseEndPoint.y >= m_mouseStartPoint.y){	
				
				// Rotate to end point of mouse
				m_angle = Cal_angle(m_mouseStartPoint,m_mouseEndPoint);
				m_shotPos.rotation = Quaternion.Euler(330,-270-m_angle,0);

				// Throw ball
				m_shot.AddForce(m_shotPos.forward * m_distance * m_shotForce);
				m_shot.GetComponent<DeleteSelf>().SetDestroy(true);

				//Count number ball that throw
				m_score++;
				gameObject.GetComponent<Drawline>().setDraw(false);
			}
		}
	}

	float Cal_angle(Vector3 origin, Vector3 dest){

		float x = Mathf.Atan2(dest.y - origin.y,dest.x - origin.x) * 180f / 3.14f;
		Debug.Log (x);

		return x;

	}

	//Check for high score
	void StoreHighscore(int newHighscore)
	{
		int oldHighscore = PlayerPrefs.GetInt("highscore", 10000);    
		if(newHighscore < oldHighscore)
			PlayerPrefs.SetInt("highscore", newHighscore);
	}

	//Create Label show scores
	void OnGUI(){
		GUIStyle myStyle = new GUIStyle();
		myStyle.fontSize = 30;
		GUI.color = Color.red;
		GUI.Label (new Rect (10, 0, 100, 100), "Score: " + m_score , myStyle);
	}
}