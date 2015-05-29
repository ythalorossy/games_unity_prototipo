using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private Camera myCam;
	public Transform target;

	public float m_speed = 0.1f;
	public Vector3 m_addCoord;
	public float percentualSize = 1.5f;


	// Use this for initialization
	void Start () {
	
		myCam = GetComponent<Camera> ();

	}
	
	// Update is called once per frame
	void Update () {

		myCam.orthographicSize = (Screen.height / 100f) * percentualSize;

		if (target) {

			transform.position = Vector3.Lerp(transform.position, target.position + m_addCoord, m_speed) ;

		}

	}
}
