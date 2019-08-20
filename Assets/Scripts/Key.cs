using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

	[SerializeField] GameObject[] doors;
	[SerializeField] string color;
	[SerializeField] GameObject keySound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.GetComponent<PlayerController>())
		{
			Instantiate(keySound, transform.position, Quaternion.identity);
			other.gameObject.GetComponent<PlayerController>().GetMessageText().GetComponent<MessageText>().DisplayMessage(color + " Doors Unlocked");
			
			for(int i = 0; i < doors.Length; i++)
			{
				Destroy(doors[i]);	
			}

			Destroy(gameObject);	
		}
	}
}
