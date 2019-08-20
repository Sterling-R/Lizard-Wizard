using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	[SerializeField] int maxHealth;
	[SerializeField] GameObject healthText;
	[SerializeField] GameObject manaText;
	[SerializeField] GameObject messageText;
	[SerializeField] GameObject spellIcon;
	[SerializeField] GameObject launcher;
	[SerializeField] GameObject damageFlash;
	[SerializeField] GameObject deathText;
	[SerializeField] GameObject DamageSound;
	[SerializeField] GameObject[] hands;
	int currHealth;

	Launcher.Spell currSpell;
	public bool isDead;
	public bool isFrozen;
	Color freezeColor;
	Color originalColor;
	[SerializeField] float freezeCooldown;
	float freezeTimer;


	// Use this for initialization
	void Start () {
		currHealth = maxHealth;
		isDead = false;

		freezeColor = new Color (0,150,255,255);
		originalColor = hands[0].GetComponent<SpriteRenderer>().color;
		freezeTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(isFrozen)
		{
			freezeTimer += Time.deltaTime;
			
			if(freezeTimer >= freezeCooldown)
			{
				SetFrozen(false);
				freezeTimer = 0;
				hands[0].transform.parent.gameObject.GetComponent<HeadBob>().enabled = true;
			}
		}

		if(!isDead)
		{
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
				isDead = true;
				damageFlash.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
				damageFlash.GetComponent<DamageFlash>().enabled = false;
				deathText.GetComponent<MeshRenderer>().enabled = true;

			}
		}

		else
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				SceneManager.LoadScene(0);
	
			}
		}
		
	} 

	public void TakeDamage(int damage)
	{
		currHealth -= damage;
		
		//do damage flash effect and play damage sound
		damageFlash.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);

		if(!isDead && GameObject.Find("DamageSound") == null)
		{
			if(damage > 0)
			{
				Instantiate(DamageSound, transform.position, Quaternion.identity);
			}
		}

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

	public GameObject GetMessageText()
	{
		return messageText;
	}

	public void SetFrozen(bool frozen)
	{
		isFrozen = frozen;

		hands[0].transform.parent.gameObject.GetComponent<HeadBob>().enabled = false;

		if(isFrozen)
		{
			hands[0].GetComponent<SpriteRenderer>().color = freezeColor;
			hands[1].GetComponent<SpriteRenderer>().color = freezeColor;
		}

		else
		{
			hands[0].GetComponent<SpriteRenderer>().color = originalColor;
			hands[1].GetComponent<SpriteRenderer>().color = originalColor;
		}
	}
}
