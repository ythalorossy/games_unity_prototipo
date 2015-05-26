using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float health;
	
	void Start () {
		health = 100;
	}
	
	void Update () {
		
	}
	
	public void changeHealth(float amount) {
		
		health += amount;
	}
	
	public float getHealth()
	{
		return health;
	}
}
