using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CursorScript : MonoBehaviour {
	private string state;
	private string[] options;
	private int pointerIndex;
	public List<PlayerScript> hero;
	public List<EnemyScript> enemy;

	// Use this for initialization
	void Start () 
	{
		hero = new List<PlayerScript>();
		enemy = new List<EnemyScript>();

		PlayerScript playerSc = (PlayerScript) GameObject.Find("Player").GetComponent<PlayerScript>();
		EnemyScript enemySc = (EnemyScript) GameObject.Find("Enemy").GetComponent<EnemyScript>();

		hero.Add(playerSc);
		enemy.Add(enemySc);

		state = "MenuSelect";

		//Change the length of options later to accommodate more menu options
		options = new string[1];
		options[0] = "Attack";
		//Add more menu options here later as string elements of the options array

		pointerIndex = 0; //current menu option to point at
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (state == "MenuSelect" && Input.GetKeyDown(KeyCode.Return) && options[pointerIndex] == "Attack")
		{
			//Standby state is used while the interface is waiting for an attack to complete
			state = "Standby";

			PlayerScript commandedCharacter = hero[0];
			EnemyScript enemyToAttack = enemy[0];


			hero.RemoveAt (0);

			hero.Add (commandedCharacter);

			commandedCharacter.PerformAttack(enemyToAttack);
		}

		else if (state == "MenuSelect" && Input.GetAxis("Vertical") < 0)
		{
			//cycle downward in the options array
			pointerIndex = (pointerIndex + 1)%options.Length;
		}
		else if (state == "MenuSelect" && Input.GetAxis("Vertical") > 0)
		{
			//cycle upward in the options array
			pointerIndex = (options.Length + pointerIndex - 1)%options.Length;
		}
	}

	public void WakeUp()
	{
		//This is called by the attacking character's script to notify the cursor to return to default state
		state = "MenuSelect";
	}
}
