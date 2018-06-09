using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour {

	private Rigidbody rb;
	private Animation anim;

	public float maxHealth = 100f;		// max health of zombie
	public float currentHealth = 100f;	// current health of zombie
	public SimpleHealthBar healthBar;	// health bar for zombie

	public GameObject ammoObject; 		// used to spawn ammo after zombie death


	private float timeToChangeDirection = 3f; // used for random walk

	private Vector3 movement;			// where to move
	private float x, y;					// used for random range
	private bool dead = false; 			// used to make sure death animation and score update once
	private bool moveTowardsCenter = false;	// moving towards center if out of bounds

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animation> ();
		healthBar = GetComponentInChildren<SimpleHealthBar> ();
		anim.Play ("Zombie Idle");


		// select a random destination to walk to
		x = Random.Range(-1f, 1f);
		y = Random.Range(-1f, 1f);
		movement = new Vector3 (4*x, 0, 4*y);		
		if (x != 0 || y != 0) {
			anim.Play ("Walk In Place");
		} else {
			anim.Play ("Zombie Idle");
		}
	}
		
	// Update is called once per frame
	void Update () {

		if (!dead) // perform is zombie alive
		{
			timeToChangeDirection -= Time.deltaTime;
			if (timeToChangeDirection <= 0)  // change random direction every 3 seconds
			{
				x = Random.Range (-1f, 1f);
				y = Random.Range (-1f, 1f);

				movement = new Vector3 (4 * x, 0, 4 * y);
				timeToChangeDirection += 3f;
				moveTowardsCenter = false;
			}

			if (!moveTowardsCenter) // move towards random direction
			{
				Vector3 newDir = Vector3.RotateTowards (transform.forward, transform.position + movement, 2 * Time.deltaTime, 0.0f);
				transform.rotation = Quaternion.LookRotation (newDir);
				transform.position = Vector3.MoveTowards (transform.position, transform.position + movement, 2 * Time.deltaTime);
			}
			else 	// move towards center
			{
				Vector3 newDir = Vector3.RotateTowards (transform.forward, Vector3.zero, 2 * Time.deltaTime, 0.0f);
				transform.rotation = Quaternion.LookRotation (newDir);
				transform.position = Vector3.MoveTowards (transform.position, Vector3.zero, 2 * Time.deltaTime);
			}

			// if out of bounds, set bool to move towards center and add 5 more seconds to timer
			if (!moveTowardsCenter && (transform.position.x > 10 || transform.position.x < -10 || transform.position.z < -10 || transform.position.z > 10) ) 
			{
				moveTowardsCenter = true;
				timeToChangeDirection -= Time.deltaTime;
				timeToChangeDirection += 2f;
			}

			if (x != 0 || y != 0) {
				anim.Play ("Walk In Place");
			} else {
				anim.Play ("Zombie Idle");
			}
		}
	}

	public void TakeDamage(float amount)
	{
		currentHealth -= amount;						// subtract amount of damage taken
		healthBar.UpdateBar (currentHealth, maxHealth);	// update health bar
		if (currentHealth <= 0f) 						// check if dead
		{
			if (!dead) 		// only allow Die() to occur once.
			{
				Die ();
			}
		}
		else 
		{
			anim.Play ("Zombie Reaction Hit (1)");
		}
	}

	// kill the zombie
	void Die()
	{			
		dead = true;
		int ammoSpawnChance = Random.Range (0, 100);
		if (ammoSpawnChance > 80) 		// 20 % chance to drop ammo
		{
			// create an ammo object and spawn it where the zombie died
			Vector3 offset = new Vector3 (0.74f, 0f, 1.32f);
			Instantiate (ammoObject, transform.position + offset, transform.rotation); // spawn ammo to pick up
		}
		// play dying animation, add 100 to the score, and wait 3 seconds before destroying zombie object
		anim.Play ("Zombie Dying1");
		GameObject.Find("ScoreText").GetComponent<ScoreTracker>().addScore(100);
		Destroy (this.gameObject, 3f);
	}
}
