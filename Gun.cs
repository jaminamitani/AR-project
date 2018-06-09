using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

	public float damage = 25f;

	public int magAmmo = 30;
	public int reserveAmmo = 90;

	public Text uiAmmoText; 

	public Camera fpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;
	public AmmoTracker ammoTracker;


	public AudioClip shootSound;
	public AudioClip dryFireSound;
	public AudioClip reloadSound;
	public AudioSource soundSource;

	public void Start()
	{
		ammoTracker = transform.parent.GetChild(2).GetComponent<AmmoTracker>();
	}


	public void Shoot()
	{
		
		if (ammoTracker.shootUpdate ())	// if enough ammo to shoot
		{ 

			soundSource.clip = shootSound;
			soundSource.Play();
		
			RaycastHit hit;
			if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit)) { // see if raycast hit an object
				Debug.Log (hit.transform.name);

				ZombieBehavior target = hit.transform.GetComponent<ZombieBehavior> ();
				if (target != null) {  // check if object hit was a zombie
					target.TakeDamage (damage);
				}
				ZombieBehaviorGroundPlane target2 = hit.transform.GetComponent<ZombieBehaviorGroundPlane> ();
				if (target2 != null) {
					target2.TakeDamage (damage);
				}

				GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal)); // for bullet impact effect
				Destroy (impactGO, 2f);
			}
		}
		else // no ammo
		{
			soundSource.clip = dryFireSound;
			soundSource.Play ();
		}
	}

	public void Reload()
	{
		if (ammoTracker.reload ()) // if reload can occur
		{ 
			// play reload sound effect
			soundSource.clip = reloadSound;
			soundSource.Play ();
		} 
	}

}
