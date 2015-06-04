using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	// Max health
	public float maxHealth = 100.0f;

	// Current Health
	public float currentHealth = 0f;

	public Transform healthBar;

	// Use this for initialization
	void Start () 
	{
		// Start health with max health
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		updateHealthBar ();

		if (currentHealth <= 0f)
		{
			Destroy (this.gameObject, 0f);
		}
	}
	
	public void TakeDamage(float amount) 
	{
		this.currentHealth -= amount;
	}

	private void updateHealthBar () 
	{
		float healthBarScale = Mathf.Clamp((currentHealth / maxHealth), 0, 1) ;

		Vector3 healthBarTemp = healthBar.transform.localScale;
		healthBarTemp.x = healthBarScale;
		
		healthBar.localScale = healthBarTemp;
	}
}