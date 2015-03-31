﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[HideInInspector] 
	public bool isFacingRight = true;

	// Field of view
	public float lookupTo;

	// Power
	public Rigidbody2D power;

	// Position to start power
	public Transform targetInstatiatePower;

	// Time to recharge 
	public float chargeTime;

	private float rechargingTime = 0;

	private bool fired = false;

	void Start() {
	}

	void FixedUpdate() {

		rechargingTime += Time.deltaTime;
	
		//Debug.Log (rechargingTime);

		if (rechargingTime >= chargeTime)
		{
			fired = false;

			rechargingTime = 0;
		}

		Vector2 rayCastPosition = new Vector2(transform.position.x, transform.position.y + .5f);

		// Raycast looking...
		RaycastHit2D hit = Physics2D.Raycast(
									rayCastPosition, 
									(isFacingRight) ? Vector2.right : -Vector2.right, 
									lookupTo);

		Debug.DrawRay(rayCastPosition, (isFacingRight) ? Vector2.right : -Vector2.right, Color.red);

		if (hit.collider != null) {

			//Debug.Log("Raycast have hit anybody");

			if (hit.collider.gameObject.tag == "Player")
			{
				//Debug.Log("Raycast have hit the Player");

				// Distance from my position and hit position 
				float distanceHit = Mathf.Abs(hit.point.y - transform.position.y);

				// Distante of hit is less or equal to distance which i see
				if (distanceHit <= lookupTo)
				{
					// If not yet fired or recharging
					if (!fired)
					{
						// Set direction of power
						power.GetComponent<HadukenEnemy>().direction = targetInstatiatePower.transform.localScale.x;

						Instantiate(power, targetInstatiatePower.position, targetInstatiatePower.rotation);
					
						//Debug.Log("Raycast have hit the Player");

						fired = true;

						Flip ();
					}
				}
			}
		}
	}

	void Flip()
	{
		// Invert the value of facingRight
		isFacingRight = !isFacingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

		Transform targetPower = transform.FindChild("TargetInstantiatePower");
		Vector3 theScaleTarget = targetPower.transform.localScale;
		theScaleTarget.x *= -1;
		targetPower.localScale = theScale;

	}
}
