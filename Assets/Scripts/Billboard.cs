using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

	[SerializeField] Camera cam;
	[SerializeField] Sprite[] sprites = new Sprite[4];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Vector3.Dot(transform.parent.forward, cam.transform.parent.forward));

		transform.LookAt(cam.transform.position, Vector3.up);

		
		float dot = Vector3.Dot(transform.parent.forward, cam.transform.parent.forward);
		Vector3 cross = Vector3.Cross(transform.parent.forward, cam.transform.parent.forward);

		//choose sprite depending on what "side" player is looking at
		if(-1f <= dot && dot <= -0.7f)
		{
			//front sprite
			SetSprite(0);
		}

		else if(0.7f <= dot && dot <= 1f)
		{
			//back sprite
			SetSprite(1);
		}

		else if(-0.7f <= dot && dot <= 0.7f)
		{
			//right spite
			if(cross.y > 0)
			{
				SetSprite(2);
			}
			
			//left sprite
			else if(cross.y < 0)
			{
				SetSprite(3);
			}
		}



	}

	public void SetCam(Camera newCam)
	{
		cam = newCam; 
	}

	void SetSprite(int index)
	{
		if(gameObject.GetComponent<SpriteRenderer>().sprite != sprites[index])
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index];
			}
	}
}
