using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour {

	SpriteRenderer spriteRender;
	[SerializeField] float fadeRate;
	// Use this for initialization
	void Start () {
		spriteRender = gameObject.GetComponent<SpriteRenderer>();
		spriteRender.color = new Color(1,1,1,0);
	}
	
	// Update is called once per frame
	void Update () {

		if(spriteRender.color.a > 0)
		{
			spriteRender.color = new Color(spriteRender.color.r, spriteRender.color.g, spriteRender.color.b, spriteRender.color.a - fadeRate * Time.deltaTime);
		}
		
	}
}
