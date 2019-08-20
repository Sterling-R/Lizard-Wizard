using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] float lifespan;
	[SerializeField] int damage;

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

	public void init(Camera cam, Vector3 direction)
	{
		transform.forward = direction;
		gameObject.transform.GetChild(0).GetComponent<Billboard>().SetCam(cam);
	}

	void OnTriggerEnter(Collider other)
	{
		
		if (other.gameObject.GetComponent<ComputeTextureTiling>())
		{
			Destroy(gameObject);
		}

		if (other.gameObject.GetComponent<EnemyDamage>())
		{
			//do damage
			other.gameObject.GetComponent<EnemyDamage>().TakeDamage(damage);

			Destroy(gameObject);
		}

	}
}
