using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[HideInInspector] 
	public bool isFacingRight = true;

	public float maxSpeed = 7.0f;
	[SerializeField] private float m_JumpForce = 450f;  
	public Rigidbody2D haduken;
	
	public Transform targetInstatiatePower;

	[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
	private Transform m_GroundCheck;	// A position marking where to check if the player is grounded.
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private bool m_Flying;            // Whether or not the player is flying.

	private Rigidbody2D m_Rigidbody2D;

	private float movement = 0f;

	private void Awake()
	{
		m_GroundCheck = transform.Find("GroundCheck");

		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		checkGrounded();
	
		#if !UNITY_ANDROID && !UNITY_IPHONE

		movement = Input.GetAxis("Horizontal");

		#endif

		Move (movement);
	}

	void Update () 
	{

		#if UNITY_ANDROID
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			// Get the unity player activity
			AndroidJavaObject activity = 
				new AndroidJavaClass("com.unity3d.player.UnityPlayer")
					.GetStatic<AndroidJavaObject>("currentActivity");
			
			// call activity's boolean moveTaskToBack(boolean nonRoot) function
			// documentation: http://developer.android.com/reference/android/app/Activity.html#moveTaskToBack(boolean)
			activity.Call<bool>("moveTaskToBack", true);
		}
		
		#endif

		if (Input.GetKeyDown(KeyCode.Z))
		{
			GetComponent<Animator>().SetTrigger("hiting");
		} 
		else if (Input.GetKeyDown(KeyCode.X)) 
		{
			Attack("hadukenEvent");
		} 
		else if (Input.GetButtonDown("Jump") && m_Grounded)
		{
			Jump();
		}

		GetComponent<Animator>().SetBool("flying", m_Flying);
	}

	/*
	 * Check if the Player is in contact with other thing
	 */
	void checkGrounded()
	{		
		m_Grounded = false;
		
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject) {
				m_Grounded = true;
				m_Flying = false;
			}
		}
	}

	/**
	 * Moviment Event called by UI
	 */
	public void movimentEvent (float moviment)
	{
		movement = moviment;
	}

	/*
	 * Attack Event called by UI
	 */
	public void attackEvent (string attackName) 
	{
		Attack(attackName);
	}

	/*
	 * Handling Player's Jump Action
	 */
	public void Jump () {

		if (m_Grounded) 
		{
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			
			m_Grounded = false;
			m_Flying = true;
		}
	}

	/*
	 * Move the player and configure de speed in Animator
	 */
	public void Move(float moviment)
	{
		m_Rigidbody2D.velocity = new Vector2 (moviment * maxSpeed, m_Rigidbody2D.velocity.y);
		
		if(moviment > 0 && !isFacingRight)
		{
			Flip ();
		} 
		else if (moviment < 0 && isFacingRight) 
		{
			Flip ();
		}
		
		GetComponent<Animator>().SetFloat("speed", Mathf.Abs(moviment));
	}

	/*
	 * Flip Player's localScale.x 
	 */ 
	void Flip()
	{
		// Invert the value of facingRight
		isFacingRight = !isFacingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	/*
	 * Player's attack command
	 */
	public void Attack (string attackName) {

		// Instantiate prefab haduken.
		// This condition will be called by Animation 
		if (attackName == "haduken")
		{
			Instantiate(haduken, targetInstatiatePower.position, targetInstatiatePower.rotation);
			
			haduken.AddForce(new Vector2(10, 0));
		} 

		// Attack event called by UI button
		else if (attackName == "hadukenEvent")
		{
			GetComponent<Animator>().SetTrigger("haduken");
			// The method Attack("haduken") will be called inside animation 
		}
		else if (attackName == "hiting")
		{
			GetComponent<Animator>().SetTrigger("hiting");
		}
	}
}
