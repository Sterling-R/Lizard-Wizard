using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

	//index of the spell which this object provides mana for 
	[SerializeField] int spellIndex;
	[SerializeField] int manaAmount;
	[SerializeField] GameObject manaSound;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Instantiate(manaSound, transform.position, Quaternion.identity);
			other.gameObject.GetComponent<PlayerController>().AddMana(spellIndex, manaAmount);
			Destroy(gameObject);

			
		}
	}
}
