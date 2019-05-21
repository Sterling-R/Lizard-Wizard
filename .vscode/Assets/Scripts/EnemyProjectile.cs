using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] float lifespan;
	[SerializeField] int damage;
	
	// Update is called once per frame
	void Update () {

		lifespan -= Time.deltaTime;

		if(lifespan < 0f)
		{
			Destroy(gameObject);
		}

		transform.position += Time.deltaTime * transform.forward * speed;
		
	}

	public void Init(Camera cam, Vector3 direction)
	{
		transform.forward = direction;
		gameObject.transform.GetChild(0).GetComponent<Billboard>().SetCam(cam);
	}

	void OnTriggerEnter(Collider other)
	{
		
		if (other.tag == "Wall")
		{
			Destroy(gameObject);
		}

		if (other.tag == "Player")
		{
			//do damage
			other.gameObject.GetComponent<EnemyDamage>().TakeDamage(damage);

			Destroy(gameObject);

		}

	}
}
