using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkGenerator : MonoBehaviour {

	List<GameObject> enemies;
	[SerializeField] GameObject spark;

	GameObject originalEnemy;
	Camera cam;
	bool oneFramePassed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if(oneFramePassed)
		{
			for(int i = 0; i < enemies.Count; i++)
			{
				GameObject currSpark = Instantiate(spark, transform.position, Quaternion.identity);
				transform.LookAt(enemies[i].transform.position);
				currSpark.GetComponent<Projectile>().Init(cam, transform.forward, false, originalEnemy);
				currSpark.GetComponent<Spark>().target = enemies[i];

			}

			Destroy(gameObject);
		}	
		
		oneFramePassed = true;	
	}

	public void Init(GameObject ogEnemy, Camera ogCam)
	{
		originalEnemy = ogEnemy;
		enemies = new List<GameObject>();
		cam = ogCam;
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.GetComponent<EnemyDamage>() && other.gameObject != originalEnemy)
		{
			//if there are no walls between the generator and the enemy, shoot a spark that way
			if(!Physics.Linecast(transform.position, other.gameObject.transform.position, LayerMask.GetMask("Wall")))
			{
				if(!enemies.Contains(other.gameObject))
				{
					enemies.Add(other.gameObject);
				}
			}
		}
	}
}
