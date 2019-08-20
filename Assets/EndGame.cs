using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

	[SerializeField] GameObject music;
	[SerializeField] GameObject winScreen;
	

	float levelTimer;

	// Use this for initialization
	void Start () {
		levelTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		levelTimer += Time.deltaTime;
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.GetComponent<PlayerController>())
		{
			other.gameObject.GetComponent<PlayerController>().isDead = true;
			music.GetComponent<AudioSource>().Stop();
			winScreen.SetActive(true);

			float minutes = Mathf.Floor(levelTimer / 60);
     		float seconds = levelTimer % 60;

			if(seconds < 10)
			{	
				winScreen.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text += minutes + ":0" + (int)seconds;
			}

			else
			{	
				winScreen.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text += minutes + ":" + (int)seconds;
			}

			gameObject.GetComponent<AudioSource>().Play();

		}
	}
}
