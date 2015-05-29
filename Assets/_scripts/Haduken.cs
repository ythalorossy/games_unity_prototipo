using UnityEngine;
using System.Collections;

public class Haduken : MonoBehaviour {

	public float speed = 4f;

	private GameObject player;

	private float direction = 1;

	// Use this for initialization
	void Start () {

		// Retrieve the direction which the Player are looking at.
		player = GameObject.FindGameObjectWithTag("Player");

		// Calculate the direction
		direction = player.transform.localScale.x * speed;

		if (direction < 0) {
			Flip ();
		}

		Destroy(gameObject, 3f);
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log ("player-haduken: " + player.transform.localScale);
	
		GetComponent<Rigidbody2D>().velocity = new Vector2(direction, 0);

	}

	void Flip () {

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
