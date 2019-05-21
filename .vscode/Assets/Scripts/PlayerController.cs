using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] int maxHealth;
	[SerializeField] GameObject healthText;
	[SerializeField] GameObject manaText;
	[SerializeField] GameObject spellIcon;
	[SerializeField] GameObject launcher;
	int currHealth;

	Launcher.Spell currSpell;


	// Use this for initialization
	void Start () {
		currHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		currSpell = launcher.GetComponent<Launcher>().GetCurrSpell();

		//display health value
		if(currHealth >= 0)
		{
			healthText.GetComponent<TextMesh>().text = currHealth.ToString();
		}

		else
		{
			healthText.GetComponent<TextMesh>().text = "0";
		}

		//display mana value
		if(currSpell.mana >= 0)
		{
			manaText.GetComponent<TextMesh>().text = currSpell.mana.ToString();
		}

		else
		{
			manaText.GetComponent<TextMesh>().text = "0";
		}

		//display spell image
		spellIcon.GetComponent<SpriteRenderer>().sprite = currSpell.icon;
		
		if(currHealth <= 0)
		{
			//die
			Debug.Log("DEAD");
		}
	} 

	public void TakeDamage(int damage)
	{
		currHealth -= damage;
	}
}
