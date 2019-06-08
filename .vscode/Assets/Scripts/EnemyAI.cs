using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

	[SerializeField] GameObject player;
	[SerializeField] float attackCooldown;
	[SerializeField] float freezeTime;
	[SerializeField] GameObject attackObject;
	[SerializeField] int meleeDamage;
	[SerializeField] GameObject child;

	Color freezeColor;
	Color originalColor;

	[SerializeField] GameObject navObj;
	NavMeshAgent agent;

	enum AttackType
	{
		Melee,
		Ranged
	}
	
	[SerializeField] AttackType type;


	//for debug
	public Camera cam;

	float cooldownTimer;
	float freezeTimer;
	
	bool inAttackRange;
	bool attackPossible;
	enum AIState
	{
		Patrol,
		Aggro,
		Frozen
	}

	AIState currState;

	[SerializeField] Transform[] patrolPoints;
	int pointIndex; 

	// Use this for initialization
	void Start () {
		currState = AIState.Patrol;
		cooldownTimer = attackCooldown;

		agent = navObj.GetComponent<NavMeshAgent>();

		freezeColor = new Color (0,150,255,255);
		originalColor = child.GetComponent<SpriteRenderer>().color;

		pointIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(currState == AIState.Patrol)
		{
			//patrol along given path
			// Choose the next destination point when the agent gets
            // close to the current one.
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
			{
                GotoNextPoint();
			}
			
		}
		
		cooldownTimer += Time.deltaTime;

		if(currState == AIState.Aggro)
		{

			//move into attack range if not in range or attack isn't possible from here
			if(!inAttackRange || !attackPossible)
			{
				agent.SetDestination(player.transform.position);
				agent.isStopped = false;
			}
			
			//attack if in range and off cool down
			if(inAttackRange && cooldownTimer >= attackCooldown && attackPossible)
			{
				//face player
				transform.LookAt(player.transform.position, Vector3.up);

				agent.isStopped = true;
				
				//perform a melee attack
				if(type == AttackType.Melee)
				{
					player.GetComponent<PlayerController>().TakeDamage(meleeDamage);
				}

				//perform a ranged attack
				if(type == AttackType.Ranged)
				{
					//instatiate projectile object
					GameObject projectile = Instantiate(attackObject,transform.position,Quaternion.identity);
					
					//aim projectile at player
					Vector3 targetVertex = (cam.transform.position + player.transform.position)/2;
					Vector3 targetDirection = targetVertex - transform.position;
					targetDirection.Normalize();

					projectile.GetComponent<Projectile>().Init(cam, targetDirection, true, null);
				}

				cooldownTimer = 0f;
			}
		}

		if(currState == AIState.Frozen)
		{
			agent.isStopped = true;
			child.GetComponent<SpriteRenderer>().color = freezeColor;
			
			freezeTimer += Time.deltaTime;

			if(freezeTimer >= freezeTime)
			{
				currState = AIState.Patrol;
			}
		}

		else
		{
			child.GetComponent<SpriteRenderer>().color = originalColor;
		}
	}

	void GotoNextPoint() 
	{
            // Returns if no points have been set up
            if (patrolPoints.Length == 0)
			{
                return;
			}

            // Set the agent to go to the currently selected destination.
            agent.destination = patrolPoints[pointIndex].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            pointIndex = (pointIndex + 1) % patrolPoints.Length;
        }

	public void SetAggro()
	{
		if(currState!= AIState.Frozen)
		{
			currState = AIState.Aggro;
		}
	}

	public void SetInRange(bool inRange)
	{
		inAttackRange = inRange;
	}

	public void setAttackPossible(bool possible)
	{
		attackPossible = possible;
	}

	public void SetFrozen()
	{
		currState = AIState.Frozen;
		freezeTimer = 0.0f;
	}
}
