using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeTextureTiling : MonoBehaviour {
	// Use this for initialization
	void Start () {
		//gameObject.GetComponent<MeshRenderer>().material = new Material(gameObject.GetComponent<MeshRenderer>().material);
		
		if(transform.localScale.x > transform.localScale.z)
		{
			gameObject.GetComponent<MeshRenderer>().material.SetTextureScale("_MainTex", new Vector2 (transform.localScale.x/4.0f, 1.25f));
		}

		else
		{
			gameObject.GetComponent<MeshRenderer>().material.SetTextureScale("_MainTex", new Vector2 (transform.localScale.z/4.0f, 1.25f));
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
