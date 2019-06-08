using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour {
	void OnTriggerEnter(Collider other)
		{
			if(other.tag == "Enemy")
			{
				other.GetComponent<EnemyAI>().SetFrozen();
			}
		}
}
