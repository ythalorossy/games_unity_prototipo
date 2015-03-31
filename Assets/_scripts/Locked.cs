using UnityEngine;
using System.Collections;

public class Locked : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.tag == "Player") 
		{
			if (other.GetComponent<ItemManager>().isYellowKeyPicked())
			{
				Debug.Log("Player has picked yellow key");

				GameObject.Find("bridge").GetComponent<MoveBridge>().moveTo(3);

				Destroy(this.gameObject);
			}
		}
		
	}


}
