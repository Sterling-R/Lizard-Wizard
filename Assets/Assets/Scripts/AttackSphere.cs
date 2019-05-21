using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSphere : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			transform.parent.GetComponent<EnemyAI>().SetInRange(true);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			//if there are no walls between the enemy and the player, attacking is possible
			if(!Physics.Linecast(transform.parent.transform.position, other.gameObject.transform.position, LayerMask.GetMask("Wall")))
			{
				transform.parent.GetComponent<EnemyAI>().setAttackPossible(true);
			}

			else
			{
				transform.parent.GetComponent<EnemyAI>().setAttackPossible(false);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			transform.parent.GetComponent<EnemyAI>().SetInRange(false);
			transform.parent.GetComponent<EnemyAI>().setAttackPossible(false);
		}
	}
}
