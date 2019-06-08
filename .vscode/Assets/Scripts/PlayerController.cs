using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] int maxHealth;
	[SerializeField] GameObject healthText;
	[SerializeField] GameObject manaText;
	[SerializeField] GameObject messageText;
	[SerializeField] GameObject spellIcon;
	[SerializeField] GameObject launcher;
	[SerializeField] GameObject damageFlash;
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
			//Debug.Log("DEAD");
		}
	} 

	public void TakeDamage(int damage)
	{
		currHealth -= damage;
		
		//do damage flash effect
		damageFlash.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);

	}

	public void AddMana(int spellIndex, int manaAmount)
	{
		launcher.GetComponent<Launcher>().GetSpells()[spellIndex].mana += manaAmount;

		string message = "+" + manaAmount.ToString() + " ";
		//fire mana added
		if(spellIndex == 1)
		{
			message += "Fire ";
		}

		//ice mana added
		if(spellIndex == 2)
		{
			message += "Ice ";
		}

		//lightning mana added
		if(spellIndex == 3)
		{
			message += "Lightning ";
		}

		message += "Mana";

		Debug.Log(message);
		messageText.GetComponent<MessageText>().DisplayMessage(message);
	}
}
