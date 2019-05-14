using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

	[SerializeField] Camera cam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(cam)
		{
			transform.LookAt(cam.transform.position, Vector3.up);
		}

	}

	public void SetCam(Camera newCam)
	{
		cam = newCam; 
	}
}
