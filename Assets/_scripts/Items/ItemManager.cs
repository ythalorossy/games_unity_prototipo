using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {

	public List<ColoredKeyType> keys;

	public Transform groupItemsHUD;

	public void updateHUD(ColoredKeyType coloredKeyType)
	{
		if (groupItemsHUD)
		{
			groupItemsHUD.FindChild(coloredKeyType.ToString()).gameObject.SetActive(true);
		}
	}

	public void pickKey(ColoredKeyType coloredKeyType)
	{
		this.keys.Add (coloredKeyType);

		updateHUD (coloredKeyType);
	}

	public bool containsKey(ColoredKeyType coloredKeyType) 
	{
		return this.keys.Contains (coloredKeyType);
	}
}
