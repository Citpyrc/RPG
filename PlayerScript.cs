using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	private int attackTimer;
	public CharacterController heroControl;
	private Vector3 prevLocation;
	private string state;
	private bool successfullyAttacked;
	private Vector3 attackDestination;
	private EnemyScript attackTarget;
	private CursorScript uiScript;
	public float speed;
	
	// Use this for initialization
	void Start () {
		uiScript = (CursorScript) GameObject.Find("Menu Cursor").GetComponent<CursorScript>();
		state = "Standby";
		prevLocation = transform.position;
		speed = 15.0f;
		successfullyAttacked = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (state == "Attacking")
		{

			if (successfullyAttacked)
			{
				transform.position = Vector3.MoveTowards (transform.position, prevLocation, speed*Time.deltaTime);
				if (Vector3.Distance (transform.position, prevLocation) <= 0)
				{
					//made it back to starting position
					uiScript.WakeUp ();
					state = "Standby";
				}
			}
			else
			{



				if (Vector3.Distance (transform.position, attackDestination) < 1)
				{
					//attack animation stuff happens here

					successfullyAttacked = true;

					//damage stuff happens here
				}
				else
				{
					transform.position = Vector3.MoveTowards (transform.position, attackDestination, speed*Time.deltaTime);
				}
			}
		}
	}
	
	public void PerformAttack(EnemyScript target)
	{


		successfullyAttacked = false;
		state = "Attacking";
		prevLocation = transform.position;
		attackTarget = target;
		attackDestination = target.transform.position;
	}
}