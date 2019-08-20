using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageText : MonoBehaviour {

	[SerializeField] float messageDuration;
	float timer;
	bool cleared;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<TextMesh>().text = "";
		timer = 0f;
		cleared = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(timer >= 0)
		{
			timer -= Time.deltaTime;
		}

		if(timer <= 0 && !cleared)
		{
			gameObject.GetComponent<TextMesh>().text = "";
			cleared = true;
			Debug.Log("oop");	
		}
		
	}

	public void DisplayMessage(string message)
	{
		gameObject.GetComponent<TextMesh>().text = message;
		timer = messageDuration;
		cleared = false;
	}
}
