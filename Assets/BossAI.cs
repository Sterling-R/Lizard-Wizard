using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {

	[SerializeField] GameObject[] teleportPoints;
	int totalPoints;
	
	[SerializeField] GameObject player;
	[SerializeField] GameObject fireball;
	[SerializeField] GameObject ice;
	[SerializeField] Camera cam;
	[SerializeField] GameObject teleportParticle;
	
	[SerializeField] GameObject iceParticle;

	public enum AttackState
	{
		Fire,
		Ice
	}

	public AttackState currState;

	public int projectilesFired;
	int teleportsMade;
	[SerializeField]float fireCooldown;
	[SerializeField]float iceCooldown;
	[SerializeField] float teleportCooldown;
	float fireTimer;
	float iceTimer;
	float teleportTimer;

	bool iceFired;
	bool iceParticleSpawned;

	[SerializeField] GameObject bossTrigger;

	
	// Use this for initialization
	void Start () {
		currState = AttackState.Fire;
		totalPoints = teleportPoints.Length;
		projectilesFired = 0;
		teleportsMade = 1;
		iceFired = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		fireTimer += Time.deltaTime;
		iceTimer += Time.deltaTime;
		teleportTimer += Time.deltaTime;

		if(currState == AttackState.Fire)
		{
			//fire 3 fireballs per teleport point
			if(projectilesFired >= 3)
			{
				//teleport 3 times per fire phase
				if(teleportsMade >= 3)
				{
					if(teleportTimer >= teleportCooldown)
					{
						teleportTimer = 0;

						//teleport to ground and go to ice phase
						int randIndex = Random.Range(3,6);
						GameObject particle = Instantiate(teleportParticle, transform.position, Quaternion.identity);
						transform.position = teleportPoints[randIndex].transform.position;
						currState = AttackState.Ice;
						iceFired = false;

						iceTimer = 0;
					}
					
				}

				else
				{
					//teleport to new point
					if(teleportTimer >= teleportCooldown)
					{
						teleportTimer = 0;
						projectilesFired = 0;

						int randIndex = Random.Range(0,3);

						Vector3 newPos = teleportPoints[randIndex].transform.position;

						if(newPos == transform.position)
						{
							if(randIndex - 1 < 0)
							{
								newPos = teleportPoints[randIndex + 1].transform.position;
							}

							else
							{
								newPos = teleportPoints[randIndex - 1].transform.position;
							}
						}

						GameObject particle = Instantiate(teleportParticle, transform.position, Quaternion.identity);
						transform.position = newPos;

						teleportsMade ++;
						projectilesFired = 0;
					}
				}
			}

			else
			{
				//fire a fireball
				if(fireTimer >= fireCooldown)
				{
					Vector3 launchPos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
					GameObject currFire = Instantiate(fireball, launchPos, Quaternion.identity);
					currFire.GetComponent<Projectile>().Init(cam, player.transform.position - launchPos, true, null);
					projectilesFired ++;
					fireTimer = 0;
				}
			}


		}

		else if (currState == AttackState.Ice)
		{
			if(!iceParticleSpawned)
			{
				GameObject particle = Instantiate(iceParticle, transform.position, Quaternion.identity);
				iceParticleSpawned = true;
			}

			if(iceTimer >= iceCooldown)
			{
				if(!iceFired)
				{
					Vector3 currDirection = transform.forward; 
					//fire a ring of ice projectiles
					for(int i = 0; i < 128; i++)
					{
						GameObject currIce = Instantiate(ice, transform.position, Quaternion.identity);
						currIce.GetComponent<Projectile>().Init(cam, currDirection, true, null);
						currDirection = Quaternion.AngleAxis(-2.8125f, Vector3.up) * currDirection; 
					}

					iceFired = true;
				}
			}

			if(teleportTimer >= teleportCooldown)
			{
				teleportTimer = 0;

				currState = AttackState.Fire;
				iceParticleSpawned = false;
				int randIndex = Random.Range(0,3);
				GameObject particle = Instantiate(teleportParticle, transform.position, Quaternion.identity);
				transform.position = teleportPoints[randIndex].transform.position;
				teleportsMade = 1;
				projectilesFired = 0;
				fireTimer = 0;
			}

			
		}
	}

	void OnDestroy()
	{
		bossTrigger.GetComponent<BossTrigger>().EndFight();
	}
}
