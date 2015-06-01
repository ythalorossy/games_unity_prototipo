using UnityEngine;
using System.Collections;

public class UnlockBridge : MoveDefault, IUnlockable {

	public void unlock () {

		this.GetComponent<MoveDefault> ().move ();
	}

}
