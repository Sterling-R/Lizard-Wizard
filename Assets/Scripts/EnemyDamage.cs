using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

	[SerializeField] int maxHitPoints;
	[SerializeField] ParticleSystem deathParticle;
	public int currHitPoints;
	// Use this for initialization
	void Start () {
		currHitPoints = maxHitPoints;
	}
	
	// Update is called once per frame
	void Update () {
		if(currHitPoints <= 0)
		{
			//play death animation or do a death particle effect
			deathParticle.transform.position = transform.position;
			deathParticle.Play();
			//

			Destroy(gameObject);

		}
	}

	public void TakeDamage(int damage)
	{
		currHitPoints -= damage;
	}
}
