using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIce : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PlayerController>())
		{
			other.GetComponent<PlayerController>().SetFrozen(true);
		}
	}
}
