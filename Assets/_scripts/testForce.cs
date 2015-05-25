using UnityEngine;
using System.Collections;

public class testForce : MonoBehaviour {

	public float myForce = 1.0f;
	public float t = 0.0f;
	public bool isFacingRight = true;
	public float timeToFlip = 3.0f;
	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().AddForce (Vector2.right * myForce);
	}
	

	void FixedUpdate () {

		Vector2 force = Vector2.right * myForce * ((isFacingRight) ? 1 : -1);
		Debug.Log (force);

		GetComponent<Rigidbody2D>().AddForce (force);

		t += Time.deltaTime;

		if (t >= timeToFlip) {

			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			isFacingRight = !isFacingRight;

			t = 0.0f;
		} 


	}
}
