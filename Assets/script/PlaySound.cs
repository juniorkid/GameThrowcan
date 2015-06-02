using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	AudioSource m_audio1;
	AudioClip m_sound1;
	// Use this for initialization

	void Start(){
		m_audio1 = gameObject.GetComponent<AudioSource>();
	}

	void OnCollisionEnter ()
	{
		m_audio1.PlayOneShot (m_audio1.clip,1f);
	}
}
