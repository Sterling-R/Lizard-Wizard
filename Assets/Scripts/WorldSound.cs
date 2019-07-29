using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<AudioSource>().Play();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!gameObject.GetComponent<AudioSource>().isPlaying)
		{
			Destroy(gameObject);
		}
	}
}
