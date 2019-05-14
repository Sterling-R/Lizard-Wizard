using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

	[SerializeField] GameObject projectile;
	[SerializeField] float coolDown;
	[SerializeField] Camera cam;
	private float coolDownTimer;

	// Use this for initialization
	void Start () {

		coolDownTimer = coolDown;
	}
	
	// Update is called once per frame
	void Update () {

		if(coolDownTimer < coolDown)
		{
			coolDownTimer += Time.deltaTime;
		}

		if(Input.GetMouseButton(0) && coolDownTimer >= coolDown)
		{
			GameObject currProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
			currProjectile.GetComponent<Fireball>().init(cam);
			coolDownTimer = 0.0f;
		}
		
	}
}
