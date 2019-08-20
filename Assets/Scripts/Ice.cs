using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour {
	void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.GetComponent<EnemyAI>())
			{
				other.GetComponent<EnemyAI>().SetFrozen();
			}
		}
}
