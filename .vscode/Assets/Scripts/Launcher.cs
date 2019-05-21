﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Launcher : MonoBehaviour {

	[SerializeField] GameObject projectile;
	float coolDown;
	[SerializeField] Camera cam;
	private float coolDownTimer;

	[System.Serializable]
	public class Spell
	{
		public GameObject spellObject;
		public int mana = 0;
		public float coolDown;
		public Sprite icon;
	}

	[SerializeField] Spell[] spells;
	int currSpellIndex;


	// Use this for initialization
	void Start () 
	{
		coolDown = spells[0].coolDown;
		coolDownTimer = coolDown;
		SetCurrSpell(0);
	}
	
	// Update is called once per frame
	void Update () {

		if(coolDownTimer < coolDown)
		{
			coolDownTimer += Time.deltaTime;
		}

		if(Input.GetMouseButton(0) && coolDownTimer >= coolDown && (spells[currSpellIndex].mana > 0 || currSpellIndex == 0))
		{
			GameObject currProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
			currProjectile.GetComponent<Projectile>().Init(cam, cam.transform.forward, false);
			coolDownTimer = 0.0f;
			spells[currSpellIndex].mana -= 1;
		}

		//swap to spells using number keys
		
		//magic missle
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			//don't check mana for default spell
			SetCurrSpell(0);
		}

		//fireball
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			//check that player has mana for spell
			if(spells[1].mana > 0)
			{
				SetCurrSpell(1);
			}
		}
	}

	//function for swithing spells
	void SetCurrSpell(int SpellIndex)
	{
		projectile = spells[SpellIndex].spellObject;
		coolDown = spells[SpellIndex].coolDown;
		coolDownTimer = 0f;
		currSpellIndex = SpellIndex;
	}

 
	public Spell GetCurrSpell()
	{
		return spells[currSpellIndex];
	}
}