﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

	[SerializeField] GameObject player;
	[SerializeField] float attackCooldown;
	[SerializeField] GameObject attackObject;
	[SerializeField] int meleeDamage;

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
	
	bool inAttackRange;
	bool attackPossible;
	enum AIState
	{
		Patrol,
		Aggro
	}

	AIState currState;

	// Use this for initialization
	void Start () {
		currState = AIState.Patrol;
		cooldownTimer = attackCooldown;

		agent = gameObject.GetComponent<NavMeshAgent>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(currState == AIState.Patrol)
		{
			//patrol along given path
			
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

					projectile.GetComponent<Projectile>().Init(cam, targetDirection, true);
				}

				cooldownTimer = 0f;
			}
		}
		
	}

	public void SetAggro()
	{
		currState = AIState.Aggro;
	}

	public void SetInRange(bool inRange)
	{
		inAttackRange = inRange;
	}

	public void setAttackPossible(bool possible)
	{
		attackPossible = possible;
	}
}
