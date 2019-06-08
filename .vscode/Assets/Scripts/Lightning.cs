using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour {
	[SerializeField] GameObject sparkGenerator;
	Camera cam;
	void Start()
	{
		cam = gameObject.transform.GetChild(0).GetComponent<Billboard>().GetCam();
	}

	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Enemy")
		{
			GameObject sparkGenrate = Instantiate(sparkGenerator, transform.position, Quaternion.identity);
			sparkGenrate.GetComponent<SparkGenerator>().Init(other.gameObject, cam);
		}

	}

}