using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<ParticleSystem>().Play();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameObject.GetComponent<ParticleSystem>().isPlaying)
		{
			Destroy(gameObject);
		}
	}
}
