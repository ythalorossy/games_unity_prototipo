using UnityEngine;
using System.Collections;

public class MoveBridge : MonoBehaviour {
	/*
	public Transform startMarker;
	public Transform endMarker;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;

	void Start() {
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
	}

	void Update() {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
	}
	*/
	
	public void moveTo(float distance)
	{
		Vector2 target = new Vector2(transform.position.x + distance, transform.position.y);

		//Debug.Log(transform.position);
		//Debug.Log(target);

		transform.position = Vector2.Lerp(
								transform.position, 
								target,
								1);
	}
	
}
