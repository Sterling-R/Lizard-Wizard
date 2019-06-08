using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] float lifespan;
	[SerializeField] int damage;
	bool isEnemyProjectile;
	
	//use only if the projectile is a bouncing lightning projectile
	GameObject originalEmeny;

	// Update is called once per frame
	void Update () {

		lifespan -= Time.deltaTime;

		if(lifespan < 0f)
		{
			Destroy(gameObject);
		}

		transform.position += Time.deltaTime * transform.forward * speed;
		
	}

	public void Init(Camera cam, Vector3 direction, bool isEnemy, GameObject enemy)
	{
		isEnemyProjectile = isEnemy;
		transform.forward = direction;
		gameObject.transform.GetChild(0).GetComponent<Billboard>().SetCam(cam);
		originalEmeny = enemy;
	}

	void OnTriggerEnter(Collider other)
	{
		
		if (other.tag == "Wall")
		{
			Destroy(gameObject);
		}

		if (other.tag == "Player" && isEnemyProjectile)
		{
			//do damage to player
			other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);

			Destroy(gameObject);

		}

		if(other.tag == "Enemy" && !isEnemyProjectile && other.gameObject != originalEmeny)
		{
			//do damage to enemy
			other.gameObject.GetComponent<EnemyDamage>().TakeDamage(damage);

			Destroy(gameObject);
		}

	}
}
