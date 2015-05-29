 using UnityEngine;
using System.Collections;

public class KeyPickable : MonoBehaviour {

	public ColoredKeyType ColoredKeyType;

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") 
		{
			//other.GetComponent<ItemManager>().setYellowKeyPicked(true);

			other.GetComponent<ItemManager>().pickKey(this.ColoredKeyType);

			Destroy(this.gameObject);
		}

	}
}
