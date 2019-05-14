using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] float lifespan;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		lifespan -= Time.deltaTime;

		if(lifespan < 0f)
		{
			Destroy(gameObject);
		}

		transform.position += Time.deltaTime * transform.forward * speed;
		
	}

	public void init(Camera cam)
	{
		transform.forward = cam.transform.forward;
		gameObject.transform.GetChild(0).GetComponent<Billboard>().SetCam(cam);
	}
}
