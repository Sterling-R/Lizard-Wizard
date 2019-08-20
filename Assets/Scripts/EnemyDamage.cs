using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

	[SerializeField] int maxHitPoints;
	[SerializeField] GameObject deathParticle;
	[SerializeField] GameObject deathSound;
	public int currHitPoints;

	// Use this for initialization
	void Start () {
		currHitPoints = maxHitPoints;
	}
	
	// Update is called once per frame
	void Update () {
		if(currHitPoints <= 0)
		{
			//play death particle effect and sound
			Quaternion rot = Quaternion.identity;
			rot.eulerAngles = new Vector3(-90f,0f,0f);
			GameObject particle = Instantiate(deathParticle, transform.position, rot);
			//particle.GetComponent<ParticleSystem>().Play();

			Instantiate(deathSound,transform.position, Quaternion.identity);

			Destroy(gameObject);

		}
	}

	public void TakeDamage(int damage)
	{
		currHitPoints -= damage;
	}
}
