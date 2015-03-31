 using UnityEngine;
using System.Collections;

public class KeyPick : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {

		Debug.Log (other.tag);
		if (other.tag == "Player") 
		{
			other.GetComponent<ItemManager>().setYellowKeyPicked(true);

			Destroy(this.gameObject);
		}

	}
}
