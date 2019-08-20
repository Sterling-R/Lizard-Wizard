using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroSphere : MonoBehaviour {

	RaycastHit rayHit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.GetComponent<PlayerController>())
		{
			//if there are no walls between the enemy and the player, go into aggro state
			if(!Physics.Linecast(transform.parent.transform.position, other.gameObject.transform.position, LayerMask.GetMask("Wall")))
			{
				transform.parent.GetComponent<EnemyAI>().SetAggro();
			}
		}
	}
}
