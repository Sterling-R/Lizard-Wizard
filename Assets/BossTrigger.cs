using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

	[SerializeField] AudioSource musicPlayer;
	[SerializeField] AudioClip musicFile;
	[SerializeField] GameObject boss;
	[SerializeField] GameObject door;
	[SerializeField] GameObject key;

	bool bossFightStarted;
	// Use this for initialization
	void Start () {
		bossFightStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.GetComponent<PlayerController>() && !bossFightStarted)
		{
			boss.SetActive(true);
			door.SetActive(true);
			//musicPlayer.clip = musicFile;
			musicPlayer.Play();
			key.SetActive(false);

			bossFightStarted = true;
		}
	}

	public void EndFight()
	{
		musicPlayer.Stop();
		key.SetActive(true);
	}

}
